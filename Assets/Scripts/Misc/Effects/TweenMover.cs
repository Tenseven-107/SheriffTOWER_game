using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMover : MonoBehaviour
{
    // Tween used for moving objects, like sprites, for small animations

    [SerializeField] Vector2 startPosition = Vector2.zero; // Starting position
    [SerializeField] Vector2 endPosition = Vector2.zero; // Final position

    [SerializeField] float duration = 1f; // Time of the tween
    [SerializeField] Ease ease = Ease.Linear; // Easing of the tween. Like for example bobbing up and down in a bouncy manor or moving in a linear fashion

    [SerializeField] bool looping = false; // If the tween should loop
    [SerializeField] bool atStart = true; // If the tween should be played at start


    private void Start()
    {
        if (atStart == true) { StartTween(); }
    }


    public void StartTween()
    {
        transform.localPosition = startPosition;

        if (looping == true)
        {
            transform.DOLocalMove(endPosition, duration).SetEase(ease).SetLoops(-1, LoopType.Yoyo).SetId(gameObject);
        }
        else
        {
            transform.DOLocalMove(endPosition, duration).SetEase(ease).SetId(gameObject);
        }
    }

    // Kills tween when changing scenes or getting destroyed prematurely to avoid errors
    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
