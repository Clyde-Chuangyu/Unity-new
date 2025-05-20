
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        if (player.IsGroundDetected())
        {
            player.jumpTimes = 1;
            player.haveDashed = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space)&&player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpState);
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.AirState);
        if(Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.PrimaryAttack);
        if(Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.CounterAttack);
        if (player.canUseSkill)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
                stateMachine.ChangeState(player.aimSword);
        }       
    }
    private bool HasNoSword()
    {
        if(!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
