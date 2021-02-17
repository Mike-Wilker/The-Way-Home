using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    const float SPEED = 50.0f;
    const float MAX_HEIGHT = 100.0f;
    void Update()
    {
        transform.Translate(new Vector3(0.0f, SPEED * Time.deltaTime, 0.0f), Space.World);
        if (transform.position.y >= MAX_HEIGHT)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().takeDamage();
            Destroy(gameObject);
        }
        else if (other.GetComponent<Missile>())
        {
            other.GetComponent<Missile>().takeDamage();
            Destroy(gameObject);
        }
    }
}
