using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    ObstacleSpawner spawner;

    void OnEnable()
    {
        
        spawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    void OnCollisionEnter(Collision col)
    {
        SoundManager.GetInstance().PlaySound2D("BreakBlock");
        if (!col.gameObject.CompareTag("Ball")) return;
 
        spawner?.ReturnBreakable(gameObject);
    }
}
