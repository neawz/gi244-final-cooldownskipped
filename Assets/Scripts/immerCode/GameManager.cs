using UnityEngine;

public class GameManager : MonoBehaviour
{


    [Header("REF")]
    [SerializeField]Ball ball;


    public static GameManager Instance { get; private set; }

    [Header("Player Score")]
    public int player1Score = 0 ;
    public int player2Score = 0 ;


    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }
    public void ScoreUpdate(int ballScore,bool GoalIsPlayer1)
    {
        if(GoalIsPlayer1)
        {
            player2Score += ballScore;
        }
        else
        {
            player1Score += ballScore;  
        }
        Debug.Log($"Player1: {player1Score} | Player2: {player2Score}");

        ball.ResetBall();
    }
}
