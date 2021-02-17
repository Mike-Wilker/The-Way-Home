using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float SPEED = 25.0f;
    const float MIN_HEIGHT = -100.0f;
    void Update()
    {
        transform.Translate(new Vector3(0.0f, -SPEED * Time.deltaTime, 0.0f), Space.World);
        if (transform.position.y <= MIN_HEIGHT)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().takeDamage();
            Destroy(gameObject);
        }
    }
}
