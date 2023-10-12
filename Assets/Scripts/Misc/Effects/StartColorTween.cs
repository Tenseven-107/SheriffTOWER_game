using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartColorTween : MonoBehaviour
{
    [SerializeField] float time = 0.1f;
    SpriteRenderer sprite;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        sprite.color = new Color(1,1,1, 0);
        DOTweenModuleSprite.DOColor(sprite, Color.white, time);
    }
}
