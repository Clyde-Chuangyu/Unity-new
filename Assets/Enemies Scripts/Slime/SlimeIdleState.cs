using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeGroundState
{
    private Slime slime;

    public SlimeIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime slime) : base(_enemyBase, _stateMachine, _animBoolName, slime)
    {
        this.slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = slime.idleTime;
        slime.SetVelocity(0, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
            stateMachine.ChangeState(slime.jumpState);
    }
}
