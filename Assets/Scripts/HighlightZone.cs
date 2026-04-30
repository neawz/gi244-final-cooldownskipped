using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightZone : MonoBehaviour
{
    [SerializeField] private GameObject highlightZone;

    private readonly List<GameObject> highlightZonePool = new();

    private static HighlightZone instance;
    public HighlightZone GetInstance()
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
    private void Start()
    {
        var allHighlightZones = GameObject.FindWithTag("HighlightZone").GetComponents<GameObject>();
        foreach (var zone in allHighlightZones)
        {
            if (zone.name == highlightZone.name && zone != highlightZone)
            {
                zone.SetActive(false);
                highlightZonePool.Add(zone);
            }
        }
    }

    public GameObject Acquire()
    {
        if (highlightZonePool.Count == 0)
        {
            Debug.LogWarning("HighlightZone pool is empty!");
            return null;
        }
        var go = highlightZonePool[0];
        highlightZonePool.RemoveAt(0);
        go.SetActive(true);
        return go;
    }

    public void Release(GameObject go)
    {
        highlightZonePool.Add(go);
        go.SetActive(false);
    }
}
