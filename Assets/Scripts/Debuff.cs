using System.Collections;
using UnityEngine;

public class Debuff : PowerUp
{
    public float debuffDuration = 5f;
    public override void EnablePowerUp(PlayerStatus player, PlayerStatus opponent)
    {
        opponent.ApplyDebuff(debuffDuration);
        PowerUpObjectPool.GetInstance().ReturnObject(gameObject);
    }
}
