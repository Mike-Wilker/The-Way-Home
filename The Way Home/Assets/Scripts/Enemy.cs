using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float MAX_SPEED = 10.0f;
    const float SHOT_COOLDOWN = 1.0f;
    const float SHOT_RANDOMNESS = 0.5f;
    const float TIME_REWARD = 0.1f;
    float shotTimer = 0.0f;
    Vector3 speed;
    void Start()
    {
        speed = new Vector3(Random.Range(-MAX_SPEED, MAX_SPEED), Random.Range(-0.5f * MAX_SPEED, -MAX_SPEED), 0.0f);
    }
    void Update()
    {
        shotTimer += Time.deltaTime;
        transform.Translate(speed * Time.deltaTime, Space.World);
        if (shotTimer >= 0.0f)
        {
            shotTimer = -(SHOT_COOLDOWN + SHOT_RANDOMNESS);
            Instantiate(Resources.Load(@"Prefabs\Projectiles\Bullet"), transform.position, transform.rotation);
            Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\Effects\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX\Gun"), 0.25f, Time.timeScale);
        }
    }
    public void takeDamage()
    {
        Time.timeScale += TIME_REWARD;
        Instantiate(Resources.Load(@"Prefabs\Effects\Explosion"), transform.position, transform.rotation);
        Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\Effects\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX\Explosion"), 0.25f, Time.timeScale);
        Destroy(gameObject);
    }
}
