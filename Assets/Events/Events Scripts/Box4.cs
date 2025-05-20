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
        stats.maxHP.AddModifier(100);
        stats.currentHP += 100;
        Timer = 2f;
        tip.SetActive(true);
    }
}
