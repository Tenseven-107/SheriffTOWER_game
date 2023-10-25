using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public int hp = 3; // Entity hp
    public int maxHP = 3; // Entities max hp
    public int team = 0; // Entities team

    public bool godmode = false; // Godmode
    [HideInInspector] public bool invincible = false; // Invincible

    public float iFrameTime = 1; // I-frame time
    float last;

    public bool deactivate = true; // If entity should deactivate on death

    bool flash = true; // If enemy can flash
    WaitForSeconds flashTimer = new WaitForSeconds(0.05f);
    WaitForSeconds flashTimerShort = new WaitForSeconds(0.025f);
    public SpriteRenderer sprite;
    public GameObject deathEffect;
    public Animator anims;

    public bool juice = false; // If enemy has screen fx
    [Range(0, 0.1f)] public float hitstopTime = 0;
    [Range(0, 1)] public float screenshakeTime = 0;
    [Range(0, 10)] public float screenshakeIntensity = 0;

    [SerializeField] UnityEvent onHit;
    [SerializeField] UnityEvent onDeath;

    public RandomAudio audio; // Hit audio


    // Set up
    private void Start()
    {
        flash = true;
    }


    // Handle damage
    public void HandleHit(int damage)
    {
        if (hp > 0 && !invincible)
        {
            if (Time.time - last < iFrameTime)
            {
                return;
            }
            last = Time.time;

            if (!godmode) hp -= damage;
            if (onHit != null) onHit.Invoke();

            // Juice -
            StartCoroutine(Flash());

            if (juice)
            {
                GameCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();
                camera.hitstop(hitstopTime);
                camera.screenshake(screenshakeTime, screenshakeIntensity);
            }

            if (audio != null) audio.PlaySound();

            if (anims != null)
            {
                anims.ResetTrigger("Idle");
                anims.SetTrigger("Hit");
            }
            // -

            if (hp <= 0)
            {
                flash = false;
                Die();
            }
        }
    }


    // Play hit flash animation
    IEnumerator Flash()
    {
        Color color = sprite.color;

        yield return flashTimer;

        for (float n = 0; n < iFrameTime; n += 0.1f)
        {
            if (flash)
            {
                sprite.color = Color.red;
                yield return flashTimerShort;
                sprite.color = color;
                yield return flashTimerShort;
            }
            else break;
        }
    }


    // Die
    public void Die()
    {
        if (onDeath != null) onDeath.Invoke();

        if (deathEffect != null)
        {
            Transform parent = transform.parent;
            Instantiate(deathEffect, transform.position, Quaternion.Euler(0, 0, 0), parent);
        }

        if (deactivate) gameObject.SetActive(false);
        else Destroy(gameObject);
    }
}
