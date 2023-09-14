using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] float reloadTime = 1f;
    [SerializeField] int bullets = 6;
    bool reloading = false;
    int currentBullets = 6;

    Transform spriteTrans;
    SpriteRenderer sprite;

    BulletShooter shooter;
    Camera cam;


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


    void FireLoop()
    {
        if (Input.GetMouseButtonDown(0) && currentBullets > 0 && reloading == false)
        {
            currentBullets--;
            shooter.Fire();

            if (currentBullets <= 0)
            {
                spriteTrans.DOLocalRotate(new Vector3(0, 0, 360), reloadTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.OutElastic);
                StartCoroutine(ReloadLoop());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            spriteTrans.DOLocalRotate(new Vector3(0, 0, 360), reloadTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.OutElastic);
            StartCoroutine(ReloadLoop());
        }
    }

    IEnumerator ReloadLoop()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentBullets = bullets;
        reloading = false;
        yield break;
    }


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
