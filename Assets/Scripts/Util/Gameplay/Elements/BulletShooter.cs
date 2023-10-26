using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BulletShooter : MonoBehaviour
{
    public float cooldown = 0.1f; // Cooldown of firing
    float last;

    public int bullets = 1; // Amount of bullets per shot

    [SerializeField] Transform fireTrans; // Fire position
    GameObject bulletHolder; // Holder object of bullets

    public GameObject bullet; // Shot bullet
    ParticleSystem particle; // Fire particle

    public bool juice = false; // If has screen fx
    [Range(0, 1)] public float screenshakeTime = 0; // Screenshake time
    [Range(0, 10)] public float screenshakeIntensity = 0; // Screenshake intensity

    [SerializeField] UnityEvent atFire; // Invoked when firing
    


    // Set up
    private void Start()
    {
        bulletHolder = GameObject.FindGameObjectWithTag("BulletHolder");
        if (bulletHolder == null) Debug.LogWarning("No Bullet Holder found. Add one into your scene!");
        if (GetComponent<ParticleSystem>() != null) particle = GetComponent<ParticleSystem>();
    }


    // Fire bullets normally
    public void Fire()
    {
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        for (int i = 0; i < bullets; i++)
        {
            Instantiate(bullet, fireTrans.position, fireTrans.rotation, bulletHolder.transform);

            if (particle != null) particle.Emit(1);
        }

        if (juice)
        {
            GameCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();
            camera.screenshake(screenshakeTime, screenshakeIntensity);
        }

        atFire.Invoke();
    }


    // Fire bullets in a circular pattern
    public void FireCircle()
    {
        if (Time.time - last < cooldown)
        {
            return;
        }
        last = Time.time;

        float rot = 360 / bullets;
        float current_rot;

        for (int i = 0; i < bullets; i++)
        {
            current_rot = fireTrans.eulerAngles.z;
            float new_rot = current_rot + rot;
            fireTrans.transform.eulerAngles = new Vector3(0, 0, new_rot);

            Instantiate(bullet, fireTrans.position, fireTrans.rotation, bulletHolder.transform);
        }
        
        if (particle != null) particle.Emit(1);

        if (juice)
        {
            GameCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameCamera>();
            camera.screenshake(screenshakeTime, screenshakeIntensity);
        }

        atFire.Invoke();
    }
}
