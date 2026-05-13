using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    private Vector3 originalScale;
    private Coroutine buffCoroutine;
    private Coroutine debuffCoroutine;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void ApplySizeBuff(float sizeMultiplier, float duration)
    {
        if (buffCoroutine != null)
        {
            StopCoroutine(buffCoroutine);
        }
        buffCoroutine = StartCoroutine(SizeBuffRoutine(sizeMultiplier, duration));
    }

    public void ApplyDebuff(float duration)
    {
        if (debuffCoroutine != null)
        {
            StopCoroutine(debuffCoroutine);
        }
        else
        {
            ReverseControls();
        }
        debuffCoroutine = StartCoroutine(ReverseDebuffRoutine(duration));
    }

    private IEnumerator SizeBuffRoutine(float sizeMultiplier, float duration)
    {
        transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y,
            originalScale.z * sizeMultiplier
        );

        yield return new WaitForSeconds(duration);

        transform.localScale = originalScale;
        buffCoroutine = null;
    }

    private IEnumerator ReverseDebuffRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        ReverseControls();
        debuffCoroutine = null;
    }

    private void ReverseControls()
    {
        GetComponent<PlayerController>().ReverseControls();
    }
}
