using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotater : MonoBehaviour
{
    [SerializeField] float initialRotation = 0;
    [SerializeField] float finalRotation = 0;

    [SerializeField] float duration = 1f;
    [SerializeField] Ease ease = Ease.Linear;
    [SerializeField] LoopType loop = LoopType.Yoyo;

    [SerializeField] bool looping = false;
    [SerializeField] bool atStart = true;


    private void Start()
    {
        if (atStart == true) { StartTween(); }
    }


    public void StartTween()
    {
        transform.rotation = Quaternion.Euler(0, 0, initialRotation);

        if (looping == true)
        {
            transform.DOLocalRotate(new Vector3(0, 0, finalRotation), duration, RotateMode.FastBeyond360).SetRelative(true).SetEase(ease).SetLoops(-1, loop).SetId(gameObject);
        }
        else
        {
            transform.DOLocalRotate(new Vector3(0, 0, finalRotation), duration, RotateMode.FastBeyond360).SetRelative(true).SetEase(ease).SetId(gameObject);
        }
    }



    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
