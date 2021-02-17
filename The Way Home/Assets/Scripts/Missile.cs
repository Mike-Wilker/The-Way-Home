using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    const float ACCELERATION = 2.0f;
    const float DRAG = 0.05f;
    const float TIME_REWARD = 0.05f;
    Player player;
    float speed = 10.0f;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        speed += ACCELERATION * Time.deltaTime;
        speed *= 1.0f - DRAG * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        transform.LookAt(player.transform);
    }
    public void takeDamage()
    {
        Time.timeScale += TIME_REWARD;
        Instantiate(Resources.Load(@"Prefabs\Effects\Explosion"), transform.position, transform.rotation);
        Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\Effects\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX\Explosion"), 0.25f, Time.timeScale);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().takeDamage();
            Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\Effects\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX\Explosion"), 0.25f, Time.timeScale);
            Destroy(gameObject);
        }
    }
}
