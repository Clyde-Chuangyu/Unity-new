using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box4 : Chest
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
        stats.maxHP.AddModifier(100);
        stats.currentHP += 100;
        Timer = 2f;
        tip.SetActive(true);
    }
}
