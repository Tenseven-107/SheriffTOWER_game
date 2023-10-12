using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamJuiceEmitter : MonoBehaviour
{
    [SerializeField] float hitstopTime = 1f;
    [SerializeField] float screenshakeTime = 1f;
    [SerializeField] float screenshakeIntensity = 1f;
    GameCamera camera;


    void Start()
    {
        GameObject camObject = GameObject.FindGameObjectWithTag("MainCamera");
        camera = camObject.GetComponent<GameCamera>();
    }

    public void Emit()
    {
        camera.hitstop(hitstopTime);
        camera.screenshake(screenshakeTime, screenshakeIntensity);
    }
}
