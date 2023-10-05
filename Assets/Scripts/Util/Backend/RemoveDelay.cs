using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemoveDelay : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    WaitForSeconds timer;


    private void Start()
    {
        timer = new WaitForSeconds(delay);

        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return timer;
        Destroy(gameObject);
        yield break;
    }

    private void OnDestroy()
    {
        StopCoroutine(Remove());
    }
}
