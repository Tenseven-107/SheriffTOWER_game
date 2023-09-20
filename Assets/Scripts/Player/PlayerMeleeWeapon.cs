using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMeleeWeapon : MonoBehaviour
{
    [SerializeField] float tweenSize = 1.5f;

    Transform spriteTrans;
    SpriteRenderer sprite;

    BulletShooter shooter;
    Camera cam;


    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        shooter = GetComponent<BulletShooter>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        spriteTrans = sprite.gameObject.transform;
    }


    private void FixedUpdate()
    {
        PointToMouse();
    }


    private void Update()
    {
        FireLoop();
    }


    void FireLoop()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shooter.Fire();

            spriteTrans.localScale = Vector2.one * tweenSize;
            spriteTrans.DOScale(Vector2.one, shooter.cooldown);
        }
    }


    private void PointToMouse()
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 startingScreenPos = cam.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;

        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 0.1f);

        spriteTrans.rotation = Quaternion.Lerp(spriteTrans.rotation, Quaternion.Euler(0, 0, angle), 0.5f);

        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            sprite.flipY = true;
        }
        else { sprite.flipY = false; }
    }
}
