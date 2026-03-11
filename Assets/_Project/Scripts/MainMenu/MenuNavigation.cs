using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject characterSelectionCanvas;
    [SerializeField] Animator npcAnim;

    public void OpenCharacterSelection()
    {
        if (npcAnim != null)
        {
            npcAnim.SetTrigger("ButtonPressed");
        }

        mainCanvas.SetActive(false);

        characterSelectionCanvas.SetActive(true);
    }
}
