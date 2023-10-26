using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamJuiceEmitter : MonoBehaviour
{
    // Object that plays camera effects

    [SerializeField] float hitstopTime = 1f; // Time of hitstop effect
    [SerializeField] float screenshakeTime = 1f; // Time of screenshake
    [SerializeField] float screenshakeIntensity = 1f; // Intensity oif screenshake 
    GameCamera camera; // the game's camera

    // Getting the camera object
    void Start()
    {
        GameObject camObject = GameObject.FindGameObjectWithTag("MainCamera");
        camera = camObject.GetComponent<GameCamera>();
    }

    // Plays camera effects
    public void Emit()
    {
        camera.hitstop(hitstopTime);
        camera.screenshake(screenshakeTime, screenshakeIntensity);
    }
}
