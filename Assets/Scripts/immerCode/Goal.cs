using UnityEngine;

public class Goal : MonoBehaviour
{

    private Ball ball;
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Goal!!");
        }
    }
}
