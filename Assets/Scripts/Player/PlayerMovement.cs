using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float speed = 0;
    [SerializeField] float maxSpeed = 5.5f;
    [SerializeField] float acceleration = 1.25f;
    Vector2 velocity = Vector2.zero;
    Rigidbody2D rb;

    [SerializeField] GameObject torsoObject;
    [SerializeField] GameObject feetObject;
    public enum EquipState { UNARMED, ARMED, ITEM };
    [SerializeField] public EquipState equip;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        SetAnimations();
    }


    void Movement()
    {
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
