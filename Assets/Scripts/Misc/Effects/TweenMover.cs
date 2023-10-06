using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMover : MonoBehaviour
{
    [SerializeField] Vector2 startPosition = Vector2.zero;
    [SerializeField] Vector2 endPosition = Vector2.zero;

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


    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
