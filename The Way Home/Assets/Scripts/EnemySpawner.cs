using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    const float SPAWN_COOLDOWN = 3.0f;
    const float SPAWN_RANDOMNESS = 2.0f;
    [SerializeField]
    float activationTimer = 0.0f;
    float spawnTimer = 0.0f;
    void Update()
    {
        activationTimer = activationTimer - Time.deltaTime > 0.0f ? activationTimer - Time.deltaTime : 0.0f;
        if (activationTimer <= 0.0f)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= 0.0f)
            {
                spawnTimer = -(SPAWN_COOLDOWN + Random.Range(0.0f, SPAWN_RANDOMNESS));
                if (Random.Range(0.0f, 1.0f) >= 0.75)
                {
                    Instantiate(Resources.Load(@"Prefabs\Characters\Missile"), transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(Resources.Load(@"Prefabs\Characters\Drone"), transform.position, transform.rotation);
                }
            }
        }
    }
}
