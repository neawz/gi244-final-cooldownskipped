using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField] private string zoneTag = "HighlightZone";
    [SerializeField] private float switchInterval = 5f;
    private readonly List<GameObject> zonePool = new();

    private GameObject[] zone;
    private GameObject currentActiveZone;

    private void Start()
    {
        SpawnGoals();
        StartCoroutine(SwitchZoneRoutine());
    }

    private void SpawnGoals()
    {
        GameObject[] zoneGO = GameObject.FindGameObjectsWithTag(zoneTag);
        zone = zoneGO;

        foreach (var goal in zone)
        {
            SetZoneVisible(goal, false);
            zonePool.Add(goal);
        }

        if (zonePool.Count > 0)
        {
            int randomIndex = Random.Range(0, zonePool.Count);
            currentActiveZone = zonePool[randomIndex];
            zonePool.RemoveAt(randomIndex);
            SetZoneVisible(currentActiveZone, true);
        }
    }

    private IEnumerator SwitchZoneRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);
            
            if (currentActiveZone != null)
            {
                SetZoneVisible(currentActiveZone, false);
                zonePool.Add(currentActiveZone);
            }
            
            if (zonePool.Count > 0)
            {
                int randomIndex = Random.Range(0, zonePool.Count);
                currentActiveZone = zonePool[randomIndex];
                zonePool.RemoveAt(randomIndex);
                SetZoneVisible(currentActiveZone, true);
            }
        }
    }

    private void SetZoneVisible(GameObject zoneObject, bool highlighted)
    {
        Renderer[] renderers = zoneObject.GetComponentsInChildren<Renderer>(true);
        foreach (var renderer in renderers)
        {
            renderer.enabled = highlighted;
        }

        Goal goal = zoneObject.GetComponent<Goal>();
        if (goal != null)
        {
            goal.isHighlighted = highlighted;
        }
    }
}
