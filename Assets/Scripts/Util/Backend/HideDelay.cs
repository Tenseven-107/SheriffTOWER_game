using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HideDelay : MonoBehaviour
{
    [SerializeField] bool atStart = true;

    [SerializeField] float delay = 1f;
    WaitForSeconds timer;

    [SerializeField] SpriteRenderer sprite;


    private void Start()
    {
        timer = new WaitForSeconds(delay);

        if (atStart == true) { StartDelay(); }
    }

    public void StartDelay()
    {
        sprite.enabled = true;
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        yield return timer;
        sprite.enabled = false;
        yield break;
    }

    private void OnDestroy()
    {
        StopCoroutine(Hide());
    }
}
