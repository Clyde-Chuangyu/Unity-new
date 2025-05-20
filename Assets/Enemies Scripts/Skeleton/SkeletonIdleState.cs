using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{
    public SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Skeleton skeleton) : base(_enemyBase, _stateMachine, _animBoolName, skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.idleTime;
        skeleton.SetVelocity(0, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer<0)
        {
            stateMachine.ChangeState(skeleton.moveState);
        }
    }
}
