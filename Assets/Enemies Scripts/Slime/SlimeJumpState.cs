using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJumpState : SlimeGroundState
{
    private Slime slime;
    private float jumpTime=0.1f;

    public SlimeJumpState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Slime slime) : base(_enemyBase, _stateMachine, _animBoolName, slime)
    {
        this.slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = jumpTime;
        slime.SetVelocity(slime.moveSpeed*slime.facingDir,slime.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (slime.IsWallDetected())
            slime.Flip();
        if (stateTimer < 0 && slime.IsGroundDetected())                    
            stateMachine.ChangeState(slime.idleState);        
    }
}
