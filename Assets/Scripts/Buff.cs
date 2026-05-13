using System.Collections;
using UnityEngine;

public class Buff : PowerUp
{
    public float sizeMultiplier = 2f;
    public float buffDuration = 5f;
    public override void EnablePowerUp(PlayerStatus player, PlayerStatus opponent)
    {
        player.ApplySizeBuff(sizeMultiplier, buffDuration);
        PowerUpObjectPool.GetInstance().ReturnObject(gameObject);
    }
}
