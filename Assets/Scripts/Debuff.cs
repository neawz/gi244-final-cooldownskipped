using System.Collections;
using UnityEngine;

public class Debuff : PowerUp
{
    public float debuffDuration = 5f;
    public override void EnablePowerUp(PlayerStatus player, PlayerStatus opponent)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(ApplyReverseDebuff(opponent));
    }

    private IEnumerator ApplyReverseDebuff(PlayerController player)
    {
        player.ReverseControls();
        yield return new WaitForSeconds(debuffDuration);
        player.ReverseControls();
        PowerUpObjectPool.GetInstance().ReturnObject(gameObject);
    }
}
