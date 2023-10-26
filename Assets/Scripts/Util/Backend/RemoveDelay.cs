using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RemoveDelay : MonoBehaviour
{
    [SerializeField] bool atStart = true;
    [SerializeField] bool destruct = true;

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
        gameObject.SetActive(true);
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return timer;

        afterDelayEvent.Invoke();
        if (destruct == true)
        {
            Destroy(gameObject);
        }
        else { gameObject.SetActive(false);  }

        yield break;
    }

    private void OnDestroy()
    {
        StopCoroutine(Remove());
    }
}
