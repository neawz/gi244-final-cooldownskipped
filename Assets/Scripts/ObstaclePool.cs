using UnityEngine;
using System.Collections.Generic;

public class ObstaclePool : MonoBehaviour
{
    private static ObstaclePool instance;
    public static ObstaclePool Getinstance()
    {
        return instance;
    }

    [Header("Breakable")]
    [SerializeField] GameObject breakablePrefab;
    [SerializeField] int breakablePoolSize = 10;

    [Header("Permanent")]
    [SerializeField] GameObject permanentPrefab;
    [SerializeField] int permanentPoolSize = 5;

    

    readonly List<GameObject> breakablePool = new();
    readonly List<GameObject> permanentPool = new();
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
 
        FillPool(breakablePrefab, breakablePoolSize, breakablePool);
        FillPool(permanentPrefab, permanentPoolSize, permanentPool);
    }

    // ─── Public API for obstacleSpawner or other script
    public GameObject GetBreakable() => Get(breakablePool, breakablePrefab);
    public GameObject GetPermanent() => Get(permanentPool, permanentPrefab);
 
    public void ReturnBreakable(GameObject go) => Return(go, breakablePool);
    public void ReturnPermanent(GameObject go) => Return(go, permanentPool);
    void FillPool(GameObject prefab, int size, List<GameObject> pool)
    {
        for (int i = 0; i < size; i++)
        {
            var go = Instantiate(prefab, transform);
            go.SetActive(false);
            pool.Add(go);
        }
    }
    GameObject Get(List<GameObject> pool, GameObject prefab)
    {
        // หา object ที่ inactive อยู่ใน List
        GameObject obj = pool.Find(go => !go.activeSelf);
 
        if (obj == null)
        {
            Debug.LogWarning("[ObstaclePool] Pool หมด — สร้างเพิ่ม");
            obj = Instantiate(prefab, transform);
            pool.Add(obj);
        }
 
        obj.SetActive(true);
        return obj;
    }
    void Return(GameObject go, List<GameObject> pool)
    {
        go.SetActive(false);
        go.transform.SetParent(transform);
 
        if (!pool.Contains(go))
            pool.Add(go);
    }
}
