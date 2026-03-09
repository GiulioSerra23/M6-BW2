using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

// [CustomEditor] collega questo Editor al tipo Readme.
// Ogni volta che Unity seleziona un asset Readme, usa questa classe per renderizzarlo.
// [InitializeOnLoad] fa sì che il costruttore statico venga eseguito all'avvio dell'Editor.
[CustomEditor(typeof(Readme))]
[InitializeOnLoad]
public class ReadmeEditor : Editor
{
    #region Constants & Static State
    const float k_Space = 16f;
    const string k_SessionKey = "ReadmeEditor.showedReadme";
    const string k_SourceDirectory = "Assets/TutorialInfo";
    #endregion

    #region Static Constructor
    // Eseguito una volta all'avvio dell'Editor.
    // delayCall garantisce che AssetDatabase sia pronto prima di cercare asset.
    static ReadmeEditor()
    {
        EditorApplication.delayCall += SelectReadmeAutomatically;
    }
    #endregion

    #region Static Methods
    static void SelectReadmeAutomatically()
    {
        // SessionState persiste per tutta la sessione dell'Editor (non tra riavvii).
        // Evita che il Readme venga selezionato ogni volta che si ricompila.
        if (SessionState.GetBool(k_SessionKey, false)) return;

        SessionState.SetBool(k_SessionKey, true);

        var readme = SelectReadme();
        if (readme == null || readme.loadedLayout) return;

        LoadLayout();
        readme.loadedLayout = true;

        // Marca dirty: loadedLayout è cambiato, Unity deve salvare l'asset.
        EditorUtility.SetDirty(readme);
    }

    static void LoadLayout()
    {
        // Reflection necessaria: WindowLayout è una classe interna di Unity,
        // non esposta nelle API pubbliche. Approccio fragile ma è l'unico modo.
        var assembly   = typeof(EditorApplication).Assembly;
        var layoutType = assembly.GetType("UnityEditor.WindowLayout", throwOnError: true);
        var method     = layoutType.GetMethod("LoadWindowLayout", BindingFlags.Public | BindingFlags.Static);

        if (method == null)
        {
            Debug.LogWarning("[ReadmeEditor] LoadWindowLayout non trovato. Versione Unity non supportata.");
            return;
        }

        method.Invoke(null, new object[] { Path.Combine(Application.dataPath, "TutorialInfo/Layout.wlt"), false });
    }

    static Readme SelectReadme()
    {
        var ids = AssetDatabase.FindAssets("Readme t:Readme");

        if (ids.Length == 0)
        {
            Debug.LogWarning("[ReadmeEditor] Nessun asset Readme trovato nel progetto.");
            return null;
        }

        // Se ci sono più Readme, prendi il primo e avvisa.
        if (ids.Length > 1)
            Debug.LogWarning($"[ReadmeEditor] Trovati {ids.Length} asset Readme. Viene usato il primo.");

        var path         = AssetDatabase.GUIDToAssetPath(ids[0]);
        var readmeObject = AssetDatabase.LoadMainAssetAtPath(path);

        Selection.objects = new Object[] { readmeObject };
        return readmeObject as Readme;
    }

    static void RemoveTutorial()
    {
        bool confirmed = EditorUtility.DisplayDialog(
            "Remove Readme Assets",
            $"Tutti i contenuti in '{k_SourceDirectory}' verranno rimossi. Continuare?",
            "Rimuovi",
            "Annulla"
        );

        if (!confirmed) return;

        DeleteIfExists(k_SourceDirectory);
        DeleteIfExists(k_SourceDirectory + ".meta");

        var readme = SelectReadme();
        if (readme != null)
        {
            var path = AssetDatabase.GetAssetPath(readme);
            DeleteIfExists(path);
            DeleteIfExists(path + ".meta");
        }

        AssetDatabase.Refresh();
    }

    // Metodo estratto per evitare duplicazione: DRY (Don't Repeat Yourself).
    static void DeleteIfExists(string path)
    {
        if (Directory.Exists(path) || File.Exists(path))
            FileUtil.DeleteFileOrDirectory(path);
        else
            Debug.LogWarning($"[ReadmeEditor] Percorso non trovato: {path}");
    }
    #endregion

    #region GUI Styles
    bool m_Initialized;

    // Le properties lazy-init qui sotto sono un pattern comune negli Editor Unity:
    // i GUIStyle non possono essere creati fuori dal contesto OnGUI,
    // quindi li inizializziamo al primo accesso tramite Init().
    GUIStyle m_LinkStyle;
    GUIStyle m_TitleStyle;
    GUIStyle m_HeadingStyle;
    GUIStyle m_BodyStyle;
    GUIStyle m_ButtonStyle;

    void Init()
    {
        if (m_Initialized) return;

        m_BodyStyle           = new GUIStyle(EditorStyles.label) { wordWrap = true, fontSize = 14, richText = true };
        m_TitleStyle          = new GUIStyle(m_BodyStyle) { fontSize = 26 };
        m_HeadingStyle        = new GUIStyle(m_BodyStyle) { fontStyle = FontStyle.Bold, fontSize = 18 };
        m_LinkStyle           = new GUIStyle(m_BodyStyle) { wordWrap = false, stretchWidth = false };
        m_LinkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
        m_ButtonStyle         = new GUIStyle(EditorStyles.miniButton) { fontStyle = FontStyle.Bold };

        m_Initialized = true;
    }
    #endregion

    #region Inspector GUI
    protected override void OnHeaderGUI()
    {
        var readme    = (Readme)target;
        Init();

        var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 128f);

        GUILayout.BeginHorizontal("In BigTitle");
        {
            if (readme.icon != null)
            {
                GUILayout.Space(k_Space);
                GUILayout.Label(readme.icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
            }
            GUILayout.Space(k_Space);
            GUILayout.BeginVertical();
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label(readme.title, m_TitleStyle);
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        var readme = (Readme)target;
        Init();

        if (readme.sections == null || readme.sections.Length == 0)
        {
            EditorGUILayout.HelpBox("Nessuna sezione configurata in questo Readme.", MessageType.Info);
            return;
        }

        foreach (var section in readme.sections)
        {
            if (!string.IsNullOrEmpty(section.heading))
                GUILayout.Label(section.heading, m_HeadingStyle);

            if (!string.IsNullOrEmpty(section.text))
                GUILayout.Label(section.text, m_BodyStyle);

            if (!string.IsNullOrEmpty(section.linkText) && LinkLabel(new GUIContent(section.linkText)))
                Application.OpenURL(section.url);

            GUILayout.Space(k_Space);
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Remove Readme Assets", m_ButtonStyle))
            RemoveTutorial();
    }
    #endregion

    #region Utility
    bool LinkLabel(GUIContent label, params GUILayoutOption[] options)
    {
        var position = GUILayoutUtility.GetRect(label, m_LinkStyle, options);

        // Disegna la sottolineatura manualmente con Handles (non disponibile nei GUIStyle standard).
        Handles.BeginGUI();
        Handles.color = m_LinkStyle.normal.textColor;
        Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
        Handles.color = Color.white;
        Handles.EndGUI();

        EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);
        return GUI.Button(position, label, m_LinkStyle);
    }
    #endregion
}