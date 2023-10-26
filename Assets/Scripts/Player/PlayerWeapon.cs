using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerWeapon : MonoBehaviour
{
    // A weapon a player can use to attack with. Has ammo to limit usage

    [SerializeField] float reloadTime = 1f; // Time for reloading
    [SerializeField] int bullets = 6; // How many ammo the player has
    bool reloading = false; // If the player is currently reloading
    int currentBullets = 6; // Current ammo of the player

    Transform spriteTrans; // Transform of the weapon
    SpriteRenderer sprite; // Weapon sprite

    BulletShooter shooter; // Object that shoots projectiles
    Camera cam; // Game camera

    // Set up
    private void Start()
    {
        currentBullets = 6;

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

    // If the player is pressing the fire button, are not reloading and have ammo; they fire a projectile
    void FireLoop()
    {
        if (Input.GetMouseButtonDown(0) && currentBullets > 0 && reloading == false)
        {
            currentBullets--;
            shooter.Fire();

            if (currentBullets <= 0)
            {
                spriteTrans.DOLocalRotate(new Vector3(0, 0, 360), reloadTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.OutElastic); // Cool speen animation
                StartCoroutine(ReloadLoop());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            spriteTrans.DOLocalRotate(new Vector3(0, 0, 360), reloadTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.OutElastic); // Cool speen animation
            StartCoroutine(ReloadLoop());
        }
    }

    // Reloading the player weapon. After the reloadtime, the player has ammo again
    IEnumerator ReloadLoop()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentBullets = bullets;
        reloading = false;
        yield break;
    }

    // Logic for making the weapon point to the mouse
    private void PointToMouse()
    {
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 startingScreenPos = cam.WorldToScreenPoint(transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;

        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 0.1f);
        
        if (reloading == false)
        {
            spriteTrans.rotation = Quaternion.Lerp(spriteTrans.rotation, Quaternion.Euler(0, 0, angle), 0.5f);
        }

        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            sprite.flipY = true;
        }
        else { sprite.flipY = false; }
    }
}
