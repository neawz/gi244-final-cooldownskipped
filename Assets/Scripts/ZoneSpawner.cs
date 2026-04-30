using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoneSpawner : MonoBehaviour
{
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
        GameObject[] zoneGO = GameObject.FindGameObjectsWithTag("HighlightZone");
        zone = zoneGO;

        foreach (var goal in zone)
        {
            goal.SetActive(false);
            zonePool.Add(goal);
        }

        if (zonePool.Count > 0)
        {
            int randomIndex = Random.Range(0, zonePool.Count);
            currentActiveZone = zonePool[randomIndex];
            zonePool.RemoveAt(randomIndex);
            currentActiveZone.SetActive(true);
        }
    }

    private IEnumerator SwitchZoneRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);
            
            if (currentActiveZone != null)
            {
                currentActiveZone.SetActive(false);
                zonePool.Add(currentActiveZone);
            }
            
            if (zonePool.Count > 0)
            {
                int randomIndex = Random.Range(0, zonePool.Count);
                currentActiveZone = zonePool[randomIndex];
                zonePool.RemoveAt(randomIndex);
                currentActiveZone.SetActive(true);
            }
        }
    }
}
