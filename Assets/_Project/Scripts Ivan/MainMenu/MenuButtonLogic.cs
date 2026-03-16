using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;

public class MenuButtonLogic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("NPC References")]
    [SerializeField] private Animator npcAnim;

    [Header("Popup References")]
    [SerializeField] private GameObject speechBubble;

    private int layerIndex = 1;

    private Coroutine currentRoutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (npcAnim != null)
        {
        npcAnim.SetBool("IsPointing", true);
        StartWeightTransition(1f);
        }
        if (speechBubble != null)
        {
            speechBubble.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (npcAnim != null)
        {
        npcAnim.SetBool("IsPointing", false);
        StartWeightTransition(0f);
        }

        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
    }

    private void StartWeightTransition(float target)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(LerpWeight(target));
    }

    IEnumerator LerpWeight(float target)
    {
        float startWeight = npcAnim.GetLayerWeight(layerIndex);
        float time = 0f;
        float duration = 0.2f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            npcAnim.SetLayerWeight(layerIndex, Mathf.Lerp(startWeight, target, t));
            yield return null;
        }
        npcAnim.SetLayerWeight(layerIndex, target);
    }
}
