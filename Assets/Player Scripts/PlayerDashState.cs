using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        
        // 播放冲刺音效
        AudioManager.Instance?.PlayDashSound();
    }

    public override void Exit()
    {
        base.Exit();
        player.dashTimer = player.dashCD;
        player.SetVelocity(xInput * player.moveSpeed * player.facingDir, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (stateTimer < 0)
            stateMachine.ChangeState(player.IdleState);
    }
}
