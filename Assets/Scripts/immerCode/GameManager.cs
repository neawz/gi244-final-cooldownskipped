using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("REF")]
    [SerializeField] Ball ball;


    [Header("Player Score")]
    public int player1Score = 0;
    public int player2Score = 0;

    private static GameManager instance;
    public static GameManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void ScoreUpdate(int ballScore, bool GoalIsPlayer1)
    {
        if (GoalIsPlayer1)
        {
            player2Score += ballScore;
        }
        else
        {
            player1Score += ballScore;
        }
        Debug.Log($"Player1: {player1Score} | Player2: {player2Score}");

        StartCoroutine(ResetBallAfterDelay());
    }

    private IEnumerator ResetBallAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        ball.ResetBall();
    }
}
