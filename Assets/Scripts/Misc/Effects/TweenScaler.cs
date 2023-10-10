using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScaler : MonoBehaviour
{
    [SerializeField] Vector2 startScale = Vector2.one;
    [SerializeField] Vector2 endScale = Vector2.zero;

    [SerializeField] float duration = 1f;
    [SerializeField] Ease ease = Ease.Linear;

    [SerializeField] bool looping = false;
    [SerializeField] bool atStart = true;


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


    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}