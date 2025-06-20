using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
        if (Input.GetKeyDown(KeyCode.Space) && player.secondJump)
        {
            if (player.jumpTimes != 0)
                stateMachine.ChangeState(player.SecondJumpState);
        }
        if (player.IsWallDetected()&& xInput==player.facingDir)
            stateMachine.ChangeState(player.WallSlideState);
    }
}
