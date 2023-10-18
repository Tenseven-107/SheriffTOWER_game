using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float speed = 0;
    [SerializeField] float maxSpeed = 7.5f;
    [SerializeField] float acceleration = 0.5f;
    Vector2 velocity = Vector2.zero;
    Rigidbody2D rb;

    [SerializeField] float dashCooldown = 0.45f;
    [SerializeField] float dashTime = 0.12f;
    [SerializeField] float dashSpeed = 25.5f;
    float normalSpeed;
    float lastDash;

    [SerializeField] GameObject torsoObject;
    [SerializeField] GameObject feetObject;
    [SerializeField] GameObject hitbox;
    public enum EquipState { UNARMED, ARMED, ITEM };
    [SerializeField] public EquipState equip;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalSpeed = maxSpeed;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        SetAnimations();
    }

    private void Update()
    {
        Dashing();
    }



    void Movement()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(inputX, inputY);
        velocity = inputVector * speed * Time.deltaTime;

        if (inputVector != Vector2.zero)
        {
            speed += acceleration;
            speed = Mathf.Clamp(speed, 0, maxSpeed);
        }
        else
        {
            speed = 0;
        }

        rb.MovePosition(rb.position + velocity);
    }



    void Dashing()
    {
        if (Time.time - lastDash < dashCooldown)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastDash = Time.time;

            maxSpeed = dashSpeed;
            speed = dashSpeed;

            hitbox.SetActive(false);
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashTime);
        maxSpeed = normalSpeed;
        hitbox.SetActive(true);
        yield break;
    }



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
