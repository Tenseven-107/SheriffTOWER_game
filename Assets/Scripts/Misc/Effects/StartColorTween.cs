using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartColorTween : MonoBehaviour
{
    // Tween that plays effect of going from one color to another

    [SerializeField] float time = 0.1f; // Time of tween
    [SerializeField] Color startColor = Color.clear; // First color
    [SerializeField] Color endColor = Color.white; // Last color
    [SerializeField] bool atStart = true; // If should be played at start

    SpriteRenderer sprite; // Sprite that will play color effect


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (atStart == true) { StartTween(); }
    }

    public void StartTween()
    {
        sprite.color = startColor;
        DOTweenModuleSprite.DOColor(sprite, endColor, time).SetId(gameObject);
    }

    // Kills tween when changing scenes or getting destroyed prematurely to avoid errors
    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }
}
