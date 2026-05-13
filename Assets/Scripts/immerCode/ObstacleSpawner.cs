using UnityEngine;
using System.Collections.Generic;


public class ObstacleSpawner : MonoBehaviour
{
    private static ObstacleSpawner instance;
    public static ObstacleSpawner Getinstance()
    {
        return instance;
    }

    [Header("Spawn Points")]
    [SerializeField] List<Transform> spawnPoints = new();

    [Header("Spawn Count")]
    [SerializeField] int breakableCount = 3;
    [SerializeField] int permanentCount = 2;

    readonly List<GameObject> activeBreakable = new();
    readonly List<GameObject> activePermanent = new();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Start() => SpawnAll();
    public void RespawnAll()
    {
        ReturnAll();
        SpawnAll();
    }

    public void ReturnBreakable(GameObject go)
    {
        activeBreakable.Remove(go);
        ObstaclePool.Getinstance().ReturnBreakable(go);
    }

    void SpawnAll()
    {
        int total = breakableCount + permanentCount;
 
        if (spawnPoints.Count < total)
        {
            Debug.LogWarning($"[ObstacleSpawner] SpawnPoint มีแค่ {spawnPoints.Count} จุด แต่ต้องการ {total} จุด");
            total = spawnPoints.Count;
        }
 
        // Shuffle แล้วแจกตามลำดับ — ไม่ซ้ำแน่นอน
        List<Transform> shuffled = Shuffle(spawnPoints);
 
        int index = 0;
 
        for (int i = 0; i < breakableCount && index < total; i++, index++)
            Place(ObstaclePool.Getinstance().GetBreakable(), shuffled[index].position, isBreakable: true);
 
        for (int i = 0; i < permanentCount && index < total; i++, index++)
            Place(ObstaclePool.Getinstance().GetPermanent(), shuffled[index].position, isBreakable: false);
    }

    void Place(GameObject go, Vector3 pos, bool isBreakable)
    {
        go.transform.position = pos;
        go.transform.SetParent(null);
 
        if (isBreakable) activeBreakable.Add(go);
        else             activePermanent.Add(go);
    }
    void ReturnAll()
    {
        foreach (var go in activeBreakable)
        {
            ObstaclePool.Getinstance().ReturnBreakable(go);
        }
        foreach (var go in activePermanent)
        {
            ObstaclePool.Getinstance().ReturnPermanent(go);
        }
        activeBreakable.Clear();
        activePermanent.Clear();
    }

    List<Transform> Shuffle(List<Transform> source)
    {
        List<Transform> copy = new(source);
        for (int i = copy.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (copy[i], copy[j]) = (copy[j], copy[i]);
        }
        return copy;
    }
}
