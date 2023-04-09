using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemey;
    public float height = 50;
    public float rate = 10;
    float lastSpawned = 0;
    public LayerMask ground;

    public float range = 100;
    public float maxExistingEnemies = 10;

    void Start()
    {

    }

    void Update()
    {
        lastSpawned += Time.deltaTime;
        if (lastSpawned > rate && SkeletonAI.instances < maxExistingEnemies)
        {
            Vector3 rayOrigin = new Vector3(Random.Range(-range, range), height, Random.Range(-range, range));

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 100, ground))
            {
                Instantiate(Enemey, hit.point, Quaternion.identity);
                lastSpawned = 0;
            }
        }
    }
}
