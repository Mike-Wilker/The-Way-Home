using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    const float MOVE_SPEED = 50.0f;
    const float SIZE = 100.0f;
    const float SIDEWAYS_VARIATION = 50.0f;
    void Update()
    {
        transform.Translate(new Vector3(0, -MOVE_SPEED * Time.deltaTime, 0), Space.World);
        if (transform.position.y < -SIZE * 2.0f)
        {
            Instantiate(Resources.Load(@"Prefabs\Background\City" + Random.Range(1, 4).ToString()), new Vector3(Random.Range(-SIDEWAYS_VARIATION, SIDEWAYS_VARIATION), SIZE * 2, 100.0f), transform.rotation);
            Destroy(gameObject);
        }
    }
}
