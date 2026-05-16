using System.Collections.Generic;
using UnityEngine;

public class VfxPool : MonoBehaviour
{
    private static VfxPool instance;
    public static VfxPool Getinstance()
    {
        return instance;
    }

    [Header("PongHit Vfx")]
    [SerializeField] GameObject pongHitPrefab;
    [SerializeField] int PongHitPoolSize = 10;

    [Header("PowerUp Vfx")]
    [SerializeField] GameObject PowerUpPrefab;
    [SerializeField] int PowerUpPoolSize = 5;

    [Header("GoalIn Vfx")]
    [SerializeField] GameObject GoalInPrefab;
    [SerializeField] int GoalInPoolSize = 5;

    readonly List<GameObject> PongHitPool = new();
    readonly List<GameObject> PowerUpPool = new();
    readonly List<GameObject> GoalInPool = new();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        FillPool(pongHitPrefab,PongHitPoolSize,PongHitPool);
        FillPool(PowerUpPrefab,PowerUpPoolSize,PowerUpPool);
        FillPool(GoalInPrefab,GoalInPoolSize,GoalInPool);
    }

    public GameObject GetPongHit() => Get(PongHitPool, pongHitPrefab);
    public GameObject GetPowerUp() => Get(PowerUpPool, PowerUpPrefab);
    public GameObject GetGoalIn() => Get(GoalInPool, GoalInPrefab);

    public void ReturnPongHit(GameObject go) => Return(go, PongHitPool);
    public void ReturnGetPowerUp(GameObject go) => Return(go, PowerUpPool);
    public void ReturnGoalIn(GameObject go) => Return(go, GoalInPool);

    private GameObject activePongHit;
    private GameObject activePowerUp;
    private GameObject activeGoalIn;

    void FillPool(GameObject prefab, int size, List<GameObject> pool)
    {
        for (int i = 0; i < size; i++)
        {
            var go = Instantiate(prefab, transform);
            go.SetActive(false);
            pool.Add(go);
        }
    }
    public void PlacePongHit(GameObject go, Vector3 pos)
    {
        go.transform.position = pos;
        go.transform.SetParent(null);

        if(activePongHit != null)
        {
            ReturnPongHit(activePongHit);
        }
        activePongHit = go;
    }
    public void PlaceGetPowerUp(GameObject go, Vector3 pos)
    {
        go.transform.position = pos;
        go.transform.SetParent(null);

        if(activePowerUp != null)
        {
            ReturnGetPowerUp(activePowerUp);
        }
        activePowerUp = go;
    }
    public void PlaceGoalIn(GameObject go, Vector3 pos)
    {
        go.transform.position = pos;
        go.transform.SetParent(null);

        if(activeGoalIn != null)
        {
            ReturnGoalIn(activeGoalIn);
        }
        activeGoalIn = go;
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