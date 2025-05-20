using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundState : EnemyState
{
    private Slime slime;
    private Transform player;
    private int moveDir;
    public SlimeGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime slime) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.slime = slime;
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
        if (slime.isPlayerDetected())
        {
            if (player.position.x > slime.transform.position.x)
                moveDir = 1;
            else
                moveDir = -1;
            slime.SetVelocity(slime.moveSpeed * moveDir, rb.velocity.y);
        }
    }
}
