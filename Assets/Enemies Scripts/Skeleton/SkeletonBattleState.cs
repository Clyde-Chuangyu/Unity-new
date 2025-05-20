using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Skeleton skeleton;
    private Transform player;
    private int moveDir;

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Skeleton skeleton) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        player =PlayerManager.instance.player.transform;
    }
    public override void Update()
    {
        base.Update();

        if (skeleton.isPlayerDetected())
        {
            stateTimer = skeleton.battleTime;
            if (skeleton.isPlayerDetected().distance < skeleton.attackCheckDistance)
            {
                if(AbleToAttack())
                    stateMachine.ChangeState(skeleton.attackState);
            }
        }
        else
        {
            if(stateTimer<0||Vector2.Distance(player.transform.position,skeleton.transform.position)>skeleton.playerCheckDistance)
                stateMachine.ChangeState(skeleton.moveState);
        }

        if (player.position.x>skeleton.transform.position.x)        
            moveDir = 1;     
        else        
            moveDir = -1;       
        skeleton.SetVelocity(skeleton.moveSpeed * moveDir,rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public bool AbleToAttack()
    {
        if (Time.time > skeleton.lastTimeAttack + skeleton.attackCD)
        {
            skeleton.lastTimeAttack = Time.time;
            return true;
        }
        else return false;
    }
}
