using System.Collections;
using UnityEngine;

public class SpawnerTower : MonoBehaviour
{
    [SerializeField] GameObject spawnable;
    [SerializeField] float spawnDuration = 3;
    [SerializeField] int spawnCount = 10;

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine() 
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Spawn();
            yield return new WaitForSeconds(spawnDuration);
        }
    }

    void Spawn()
    {
        Instantiate(spawnable, transform.position, transform.rotation);
    }
}
