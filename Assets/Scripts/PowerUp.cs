using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private PlayerStatus player1;
    private PlayerStatus player2;
    void Awake()
    {
        player1 = GameObject.FindWithTag("Player").GetComponent<PlayerStatus>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<PlayerStatus>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (other.GetComponent<Ball>().player1)
            {
                EnablePowerUp(player1, player2);
            }
            else if (other.GetComponent<Ball>().player2)
            {
                EnablePowerUp(player2, player1);
            }
        }
    }
    public abstract void EnablePowerUp(PlayerStatus player, PlayerStatus opponent);
}
