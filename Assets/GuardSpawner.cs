using System.Collections;
using UnityEngine;

public class GuardSpawner : MonoBehaviour
{
    public GameObject guardPrefab;
    public Transform[] spawnPoints;
    public int maxGuards = 3;
    public float respawnDelay = 5f;

    private int currentGuards = 0;

    void Start() { SpawnAll(); }

    void SpawnAll()
    {
        for (int i = 0; i < maxGuards; i++)
            SpawnGuard(i);
    }

    void SpawnGuard(int index)
    {
        if (index >= spawnPoints.Length) return;
        GameObject g = Instantiate(guardPrefab, spawnPoints[index].position, Quaternion.identity);
        currentGuards++;

        Health h = g.GetComponent<Health>();
        h.OnDeath += () => StartCoroutine(RespawnAfterDelay(index));
    }

    IEnumerator RespawnAfterDelay(int index)
    {
        currentGuards--;
        yield return new WaitForSeconds(respawnDelay);
        SpawnGuard(index);
    }
}