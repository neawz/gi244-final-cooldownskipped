using UnityEngine;

public class Goal : MonoBehaviour
{   
    [Header("REF")]
    [SerializeField]private Ball ball;
    [SerializeField]private GameManager gameManager;

    [SerializeField] bool GoalIsPlayer1;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            gameManager.ScoreUpdate(ball.score,GoalIsPlayer1);
        }
    }
}
