using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("REF")]
    [SerializeField] private Ball ball;
    [SerializeField] bool GoalIsPlayer1;
    public bool isHighlighted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && isHighlighted)
        {
            GameManager.GetInstance().ScoreUpdate(ball.score * 2, GoalIsPlayer1);
            return;
        }
        else if (other.gameObject.CompareTag("Ball"))
        {
            GameManager.GetInstance().ScoreUpdate(ball.score, GoalIsPlayer1);
        }
    }
}
