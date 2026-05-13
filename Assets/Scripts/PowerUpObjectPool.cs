using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpObjectPool : MonoBehaviour
{
    private static PowerUpObjectPool instance;
    public static PowerUpObjectPool GetInstance()
    {
        return instance;
    }

    [Header("PowerUp Prefabs")]
    public GameObject buffPrefab;
    public GameObject debuffPrefab;

    [Header("Settings")]
    public Transform[] spawnPoints;
    public int poolSize = 10;
    public float spawnInterval = 7f;

    private List<GameObject> pool = new List<GameObject>();
    private GameObject currentActivePowerUp;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        InitializePool();
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize / 2; i++)
        {
            CreateAndAddToPool(buffPrefab);
            CreateAndAddToPool(debuffPrefab);
        }
    }

    void CreateAndAddToPool(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (currentActivePowerUp != null)
            {
                ReturnObject(currentActivePowerUp);
            }
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp()
    {
        GameObject go = GetRandomFromPool();
        if (go == null) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        go.transform.position = spawnPoint.position;
        go.SetActive(true);

        go.GetComponent<MeshRenderer>().enabled = true;
        go.GetComponent<Collider>().enabled = true;

        currentActivePowerUp = go;
    }

    GameObject GetRandomFromPool()
    {
        List<GameObject> inactive = pool.FindAll(obj => !obj.activeSelf);
        if (inactive.Count == 0) return null;
        return inactive[Random.Range(0, inactive.Count)];
    }

    public void ReturnObject(GameObject go)
    {
        go.GetComponent<MeshRenderer>().enabled = false;
        go.GetComponent<Collider>().enabled = false;
        go.SetActive(false);
        if (currentActivePowerUp == go)
        {
            currentActivePowerUp = null;
        }
    }
}