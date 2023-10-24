using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] UnityEvent onClick;
    [SerializeField] TrailRenderer trail;

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            onClick.Invoke();
        }

        if (Time.timeScale != 0)
        {
            trail.enabled = true;
        }
        else { trail.enabled = false; }
    }
}
