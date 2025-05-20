using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    Skeleton skeleton;
    public SkeletonStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Skeleton skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.skeleton = skeleton;
    }
    public override void Enter()
    {
        base.Enter();
        skeleton.fx.InvokeRepeating("RedColorBlink",0,0.1f);
        stateTimer =skeleton.stunnedDuration;
        rb.velocity=new Vector2(-skeleton.facingDir*skeleton.stunnedDirection.x,skeleton.stunnedDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.fx.Invoke("CancelRedBlink",0);
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)       
            stateMachine.ChangeState(skeleton.idleState);       
    }
}
