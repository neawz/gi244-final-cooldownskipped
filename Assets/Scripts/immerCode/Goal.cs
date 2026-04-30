using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("REF")]
    [SerializeField] private Ball ball;
    [SerializeField] bool GoalIsPlayer1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameManager.GetInstance().ScoreUpdate(ball.score, GoalIsPlayer1);
        }
    }
}
