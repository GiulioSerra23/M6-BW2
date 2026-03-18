using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    public static string SelectedCharacter;

    [Header("Animator References")]
    [SerializeField] private Animator narutoAnim;
    [SerializeField] private Animator sasukeAnim;

    [Header("Settings")]
    [SerializeField] private string SceneGame = "Ivan2";
    [SerializeField] private float awaitBeforeStarts = 2f;

    public void Naruto()
    {
        SelectedCharacter = "Naruto";
        StartCoroutine(Selection(narutoAnim));
    }

    public void Sasuke()
    {
        SelectedCharacter = "Sasuke";
        StartCoroutine(Selection(sasukeAnim));
    }

    IEnumerator Selection(Animator anim)
    {
        if (anim != null && anim.runtimeAnimatorController != null)
        {
            anim.SetTrigger("IsChoosed");
        }
        else
        {
            Debug.LogWarning("Manca l'animator controller");
        }

        yield return new WaitForSeconds(awaitBeforeStarts);
        GameState.Instance.SetupRun();
        SceneManager.LoadScene(SceneGame);
    }
}
