using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box3 : Chest
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
        Timer = 3f;
        player.secondJump = true;
        tip.SetActive(true);
    }
}
