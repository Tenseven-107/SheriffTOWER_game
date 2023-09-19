using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameCamera : MonoBehaviour
{
    Vector3 cam_offset = new Vector3(0,0,-10); // Offset of camera
    public float smoothing = 25; // if camera can smooth

    public float X_confiner = 0; // Confined on the X axis
    public float Y_confiner = 0; // Confined on the Y axis
    public float minusX_confiner = 0; // Confined on the -X axis
    public float minusY_confiner = 0; // Confined on the -Y axis

    public GameObject target; // Target object

    float hitstopTime = 0;

    float screenshakeTime = 0;
    float screenshakeIntensity = 0;
    public float screenshakeMod = 1;


    // Set up
    void Start()
    {
        Time.timeScale = 1;

        if (target != null) transform.position = target.transform.position + cam_offset;
        if (gameObject.tag != "MainCamera") gameObject.tag = "MainCamera";
    }


    // Set camera to target pos
    void FixedUpdate()
    {
        if (target != null)
        {
            SetCamPos();
        }
    }


    void SetCamPos()
    {
        Vector3 current_cam_pos = transform.position;
        Vector3 new_cam_pos = target.transform.position + cam_offset;
        Vector3 pos = Vector3.Lerp(current_cam_pos, new_cam_pos, smoothing * Time.fixedDeltaTime);

        pos.x = Mathf.Clamp(pos.x, -minusX_confiner, X_confiner);
        pos.y = Mathf.Clamp(pos.y, -minusY_confiner, Y_confiner);

        transform.position = pos;
    }

    Vector3 GetClampedPos()
    {
        Vector3 pos = target.transform.position;

        pos.x = Mathf.Clamp(pos.x, -minusX_confiner, X_confiner);
        pos.y = Mathf.Clamp(pos.y, -minusY_confiner, Y_confiner);

        return pos;
    }



    // Hitstop
    public void hitstop(float hitstop_time)
    { 
        this.hitstopTime = hitstop_time;
        StartCoroutine(hitstopLoop());
    }

    IEnumerator hitstopLoop()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(hitstopTime);
        Time.timeScale = 1;
        yield break;
    }



    // Screenshake
    public void screenshake(float screenshake_time, float screenshake_intensity)
    {
        if (target != null && CheckIfBordered() == false) transform.position = GetClampedPos() + cam_offset;

        this.screenshakeTime = screenshake_time;
        this.screenshakeIntensity = screenshake_intensity / screenshakeMod;
        if (screenshake_intensity != 0 && !(screenshakeMod <= -100) /*&& !CheckIfBordered()*/) StartCoroutine(ScreenshakeLoop());
    }

    IEnumerator ScreenshakeLoop() 
    {
        Vector3 original_pos = new Vector3(transform.position.x, transform.position.y, cam_offset.z);

        for (float n = 0; n < screenshakeTime; n += 0.01f)
        {
            float X = Random.Range(-screenshakeIntensity, screenshakeIntensity);
            float Y = Random.Range(-screenshakeIntensity, screenshakeIntensity);
            Vector3 shake = original_pos + new Vector3(X, Y);

            transform.position = Vector3.Lerp(original_pos, shake, 1);

            yield return null;
        }
        transform.position = original_pos;
        yield break;
    }


    // Check if camera is not touching the border
    bool CheckIfBordered()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (x >= X_confiner || x <= -X_confiner || y >= Y_confiner || y <= -Y_confiner)
        {
            return true;
        }
        else return false;
    }
}
