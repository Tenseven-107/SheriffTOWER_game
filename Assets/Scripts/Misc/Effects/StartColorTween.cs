using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartColorTween : MonoBehaviour
{
    [SerializeField] float time = 0.1f;
    [SerializeField] Color startColor = Color.clear;
    [SerializeField] Color endColor = Color.white;
    [SerializeField] bool atStart = true;

    SpriteRenderer sprite;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (atStart == true) { StartTween(); }
    }

    public void StartTween()
    {
        sprite.color = startColor;
        DOTweenModuleSprite.DOColor(sprite, endColor, time);
    }
}
