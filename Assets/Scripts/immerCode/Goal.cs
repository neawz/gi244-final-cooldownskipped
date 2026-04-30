using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("REF")]
    [SerializeField] private Ball ball;
    [SerializeField] bool GoalIsPlayer1;
    public bool isHighlighted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && !ball.hasScored)
        {
            ball.hasScored = true;
            if (isHighlighted)
            {
                GameManager.GetInstance().ScoreUpdate(ball.score * 2, GoalIsPlayer1);
            }
            else
            {
                GameManager.GetInstance().ScoreUpdate(ball.score, GoalIsPlayer1);
            }
        }
    }
}
