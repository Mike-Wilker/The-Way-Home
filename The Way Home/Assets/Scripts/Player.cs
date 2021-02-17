using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    const float SPEED = 25.0f;
    const float FOCUS_SPEED = 15.0f;
    const int MAX_LIVES = 3;
    const float HORIZONTAL_CONSTRAINT = 55.0f;
    const float VERTICAL_CONSTRAINT = 30.0f;
    const float DEATH_INVINCIBILITY = 2.5f;
    const float LASER_COOLDOWN = 0.1f;
    const float BOMB_COOLDOWN = 10.0f;
    const float TIME_DECAY_RATE = 0.05f;
    float invincibleTime = 0.0f;
    int lives = MAX_LIVES;
    float laserTimer = 0.0f;
    float bombTimer = 0.0f;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        laserTimer = laserTimer - Time.deltaTime > 0 ? laserTimer - Time.deltaTime : 0;
        bombTimer = bombTimer - Time.deltaTime > 0 ? bombTimer - Time.deltaTime : 0;
        invincibleTime = invincibleTime - Time.deltaTime > 0 ? invincibleTime - Time.deltaTime : 0;
        animator.SetFloat("X", Input.GetAxis("Horizontal") * (Input.GetButton("Focus") ? FOCUS_SPEED / SPEED : 1.0f));
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * (Input.GetButton("Focus") ? FOCUS_SPEED : SPEED), Input.GetAxis("Vertical") * (Input.GetButton("Focus") ? FOCUS_SPEED : SPEED), 0) * Time.deltaTime, Space.World);
        transform.SetPositionAndRotation(new Vector3(Mathf.Clamp(transform.position.x, -HORIZONTAL_CONSTRAINT, HORIZONTAL_CONSTRAINT), Mathf.Clamp(transform.position.y, -VERTICAL_CONSTRAINT, VERTICAL_CONSTRAINT), transform.position.z), transform.rotation);
        if (Input.GetButton("Fire1") && laserTimer <= 0.0f)
        {
            Instantiate(Resources.Load(@"Prefabs\Projectiles\Laser"), transform.position, transform.rotation);
            Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\Effects\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX\Laser"), 0.25f, Time.timeScale);
            laserTimer = LASER_COOLDOWN;
        }
        if (Input.GetButton("Fire2") && bombTimer <= 0.0f)
        {
            Instantiate(Resources.Load(@"Prefabs\Effects\Bomb"), transform.position, transform.rotation);
            Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\Effects\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX\Bomb"), 0.25f, Time.timeScale);
            bombTimer = BOMB_COOLDOWN;
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.takeDamage();
            }
            foreach (Bullet bullet in FindObjectsOfType<Bullet>())
            {
                Destroy(bullet.gameObject);
            }
            foreach (Missile missile in FindObjectsOfType<Missile>())
            {
                missile.takeDamage();
            }
        }
        Time.timeScale = Mathf.Pow(Time.timeScale, 1.0f - Time.deltaTime * TIME_DECAY_RATE);
    }
    public void takeDamage()
    {
        if (invincibleTime <= 0.0f)
        {
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                Destroy(enemy.gameObject);
            }
            foreach (Bullet bullet in FindObjectsOfType<Bullet>())
            {
                Destroy(bullet.gameObject);
            }
            foreach (Missile missile in FindObjectsOfType<Missile>())
            {
                Destroy(missile.gameObject);
            }
            invincibleTime = DEATH_INVINCIBILITY;
            Instantiate(Resources.Load(@"Prefabs\Effects\Explosion"), transform.position, transform.rotation);
            Instantiate(Resources.Load<OneShotAudioSource>(@"Prefabs\Effects\One Shot Audio Source"), transform.position, transform.rotation).setup(Resources.Load<AudioClip>(@"SFX\Death"), 0.25f, Time.timeScale);
            transform.SetPositionAndRotation(new Vector3(0.0f, 0.0f, 20.0f), transform.rotation);
            lives--;
            Time.timeScale = Mathf.Pow(Time.timeScale, 1.0f - TIME_DECAY_RATE * (MAX_LIVES - lives));
            if (lives <= 0)
            {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene("GameOver");
            }
            else if (lives <= 1)
            {
                GameObject.Find("Life2").SetActive(false);
            }
            else if (lives <= 2)
            {
                GameObject.Find("Life3").SetActive(false);
            }
        }
    }
}