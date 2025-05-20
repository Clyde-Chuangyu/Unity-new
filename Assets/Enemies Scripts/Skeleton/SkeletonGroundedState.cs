using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Skeleton skeleton;
    protected Transform player;
    public SkeletonGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Skeleton skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (skeleton.isPlayerDetected() || Vector2.Distance(skeleton.transform.position, player.transform.position) < skeleton.attackCheckDistance)
        {
            stateMachine.ChangeState(skeleton.battleState);
        }
    
    }

}
