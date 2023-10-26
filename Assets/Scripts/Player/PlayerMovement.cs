using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    // Movement script of the player

    float speed = 0; // Current speed of the player
    [SerializeField] float maxSpeed = 7.5f; // Maximum speed of the player
    [SerializeField] float acceleration = 0.5f; // Acceleration of the player
    Vector2 velocity = Vector2.zero; // The current velocity of the player, used for moving the player around without directly changing the position 
    Rigidbody2D rb;// Player object's rigidbody

    [SerializeField] float dashCooldown = 0.45f; // Cooldown of the dash
    [SerializeField] float dashTime = 0.12f; // Time of the player dash
    [SerializeField] float dashSpeed = 25.5f; // Speed of the player dash
    float normalSpeed; // Normal speed of the player
    float lastDash; // Last time the player dashed

    [SerializeField] GameObject torsoObject; // The torso of the player
    [SerializeField] GameObject feetObject; // The legs of the sprite
    [SerializeField] GameObject hitbox; // Player objects hitbox, collider that recieves hits
    public enum EquipState { UNARMED, ARMED, ITEM }; // Animation states of the player
    [SerializeField] public EquipState equip;

    [SerializeField] UnityEvent onDash; // Invoked when the player dashes



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalSpeed = maxSpeed;
    }


    // Runs functions in their correspondent update functions
    void FixedUpdate()
    {
        Movement();
        SetAnimations();
    }

    private void Update()
    {
        Dashing();
    }


    // Movement of the player
    void Movement()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        // Getting the movement input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(inputX, inputY);

        // Velocity is the input vector which is multiplied byt the current speed of the player
        velocity = inputVector * speed * Time.deltaTime;

        // Adds speed when the player is pressing the movement keys. returns to 0 when none are pressed
        if (inputVector != Vector2.zero)
        {
            speed += acceleration;
            speed = Mathf.Clamp(speed, 0, maxSpeed);
        }
        else
        {
            speed = 0;
        }

        rb.MovePosition(rb.position + velocity); // Moves the player
    }


    // Player dashing
    void Dashing()
    {
        // Checks if the dashing cooldown is over
        if (Time.time - lastDash < dashCooldown)
        {
            return;
        }

        // When dashing key is pressed, the player dashes, increasing speed and deactivating the hitbox
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastDash = Time.time;

            maxSpeed = dashSpeed;
            speed = dashSpeed;

            hitbox.SetActive(false);
            StartCoroutine(Dash());

            onDash.Invoke();
        }
    }
    
    // At the end of the dash the player returns to normal speed. Hitbox gets enabled again
    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashTime);
        maxSpeed = normalSpeed;
        hitbox.SetActive(true);
        yield break;
    }


    // Playing animations
    void SetAnimations()
    {
        SpriteRenderer torso = torsoObject.GetComponent<SpriteRenderer>();
        SpriteRenderer feet = feetObject.GetComponent<SpriteRenderer>();

        if (velocity.x < 0)
        {
            torso.flipX = true;
            feet.flipX = true;
        }
        else if (velocity.x > 0)
        {
            torso.flipX = false;
            feet.flipX = false;
        }


        Animator torsoAnimator = torsoObject.GetComponent<Animator>();
        Animator feetAnimator = feetObject.GetComponent<Animator>();

        if (velocity != Vector2.zero)
        {
            if (equip == EquipState.UNARMED)
            {
                torsoAnimator.Play("Walk");
            }
            if (equip == EquipState.ARMED)
            {
                torsoAnimator.Play("Armed");
            }
            if (equip == EquipState.ITEM)
            {
                torsoAnimator.Play("Item");
            }

            feetAnimator.Play("Walk");
        }
        else
        {
            if (equip == EquipState.UNARMED)
            {
                torsoAnimator.Play("Idle");
            }
            if (equip == EquipState.ARMED)
            {
                torsoAnimator.Play("Armed");
            }
            if (equip == EquipState.ITEM)
            {
                torsoAnimator.Play("Item");
            }

            feetAnimator.Play("Idle");
        }
    }
}
