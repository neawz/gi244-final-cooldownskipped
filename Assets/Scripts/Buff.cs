using System.Collections;
using UnityEngine;

public class Buff : PowerUp
{
    public float sizeMultiplier = 2f;
    public float buffDuration = 5f;
    public override void EnablePowerUp(PlayerController player)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(ApplySizeBuff(player));
    }
    private IEnumerator ApplySizeBuff(PlayerController player)
    {
        Transform platform = player.transform;
        Vector3 originalScale = platform.localScale;

        platform.localScale = new Vector3(
            originalScale.x,
            originalScale.y,
            originalScale.z * sizeMultiplier
        );

        yield return new WaitForSeconds(buffDuration);

        platform.localScale = originalScale;
        PowerUpObjectPool.GetInstance().ReturnObject(gameObject);
    }
}
