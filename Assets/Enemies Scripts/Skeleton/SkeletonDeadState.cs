using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    private Skeleton skeleton;
    public SkeletonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Skeleton skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        skeleton.animator.SetBool(skeleton.lastAnimBoolName, true);
        skeleton.animator.speed = 0;
        skeleton.capsule.enabled = false;
        stateTimer = 0.15f;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0) 
        {
            rb.velocity = new Vector2(0,10);
        }
    }
}
