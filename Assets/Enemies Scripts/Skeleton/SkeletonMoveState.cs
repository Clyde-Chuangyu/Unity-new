using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Skeleton skeleton) : base(_enemyBase, _stateMachine, _animBoolName, skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        skeleton.SetVelocity(skeleton.moveSpeed*skeleton.facingDir,rb.velocity.y);
        if(skeleton.IsWallDetected()||!skeleton.IsGroundDetected() ) {
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.idleState);
        }
    }
}
