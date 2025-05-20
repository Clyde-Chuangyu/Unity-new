using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private Entity entity;
    private RectTransform myTransform;
    private Slider slider;
    private CharacterStats myStats;

    private void Start()
    {
        myTransform=GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        myStats = GetComponentInParent<CharacterStats>();
        entity.onFlipped += FlipUI;
        myStats.OnHealthChanged += UpdateHP;
        UpdateHP();
    }
    private void Update()
    {
        slider.value = myStats.currentHP;
    }
    private void UpdateHP()
    {
        slider.maxValue = myStats.GetMaxHP();
        slider.value = myStats.currentHP;
    }
    private void FlipUI()
    {
        myTransform.Rotate(0,180,0);
    }
    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        myStats.OnHealthChanged -= UpdateHP;
    }
}
