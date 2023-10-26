using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScaler : MonoBehaviour
{
    // Tween used for a scaling effect

    [SerializeField] Vector2 startScale = Vector2.one; // Start scale
    [SerializeField] Vector2 endScale = Vector2.zero; // Final scale value

    [SerializeField] float duration = 1f; // Time of the tween
    [SerializeField] Ease ease = Ease.Linear; // Easing of the tween

    [SerializeField] bool looping = false; // If the tween should loop
    [SerializeField] bool atStart = true; // If the tween should be played at start


    private void Start()
    {
        if (atStart == true) { StartTween(); }
    }


    public void StartTween()
    {
        transform.localScale = startScale;

        if (looping == true)
        {
            transform.DOScale(endScale, duration).SetEase(ease).SetLoops(-1, LoopType.Yoyo).SetId(gameObject);
        }
        else
        {
            transform.DOScale(endScale, duration).SetEase(ease).SetId(gameObject);
        }
    }

    // Kills tween when changing scenes or getting destroyed prematurely to avoid errors
    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
