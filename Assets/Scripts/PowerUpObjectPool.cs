using System.Collections.Generic;
using UnityEngine;

public class PowerUpObjectPool : MonoBehaviour
{
    public List<GameObject> powerUpPrefabs;
    private static PowerUpObjectPool instance;
    public static PowerUpObjectPool GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
