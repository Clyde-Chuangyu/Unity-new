using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeadState : EnemyState
{
    private Slime slime;
    public SlimeDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime slime) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
        slime.animator.SetBool(slime.lastAnimBoolName, true);
        slime.animator.speed = 0;
        slime.capsule.enabled = false;
        stateTimer = 0.15f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
        {
            rb.velocity = new Vector2(0, 10);
        }
    }
}
