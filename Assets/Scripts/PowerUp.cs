using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private PlayerController player1;
    private PlayerController player2;
    void Awake()
    {
        player1 = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (other.GetComponent<Ball>().player1)
            {
                EnablePowerUp(player1);
            }
            else if (other.GetComponent<Ball>().player2)
            {
                EnablePowerUp(player2);
            }
        }
    }
    public abstract void EnablePowerUp(PlayerController player);
}
