using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotater : MonoBehaviour
{
    // Tween for a rotating effect

    [SerializeField] float initialRotation = 0; // First point of rotation
    [SerializeField] float finalRotation = 0; // Final point of rotation

    [SerializeField] float duration = 1f; // Time of the tween
    [SerializeField] Ease ease = Ease.Linear; // Easing of the rotation
    [SerializeField] LoopType loop = LoopType.Yoyo; // How the effect should loop, like resetting when reaching the final rotation or moving back and forth

    [SerializeField] bool looping = false; // If the tween should loop
    [SerializeField] bool atStart = true; // If the tween should be looped


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


    // Kills tween when changing scenes or getting destroyed prematurely to avoid errors
    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
