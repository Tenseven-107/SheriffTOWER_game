using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RemoveDelay : MonoBehaviour
{
    [SerializeField] bool atStart = true;

    [SerializeField] float delay = 1f;
    WaitForSeconds timer;

    [SerializeField] UnityEvent afterDelayEvent;


    private void Start()
    {
        timer = new WaitForSeconds(delay);

        if (atStart == true) { StartDelay(); }
    }

    public void StartDelay()
    {
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return timer;

        afterDelayEvent.Invoke();
        Destroy(gameObject);

        yield break;
    }

    private void OnDestroy()
    {
        StopCoroutine(Remove());
    }
}
