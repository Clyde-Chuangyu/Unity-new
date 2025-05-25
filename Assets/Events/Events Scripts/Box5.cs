using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box5 : Chest
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void Effect()
    {
        base.Effect();
         // 在箱子被打开时播放音效
        AudioManager.Instance?.PlayboxSound();
        stats.defense.AddModifier(10);       
        Timer = 2f;
        tip.SetActive(true);
    }
}
