using UnityEngine;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints = new();
    public GameObject blockprerfab;
    void Start()
    {   
        for(int i = 0; i < spawnPoints.Count; i++ )
        {
            
        }
        
    }
}
