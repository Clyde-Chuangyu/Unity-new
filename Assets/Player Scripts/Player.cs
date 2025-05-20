using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    #region Values
    public bool isBusy {  get; private set; }
    public bool canUseSkill=false;
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration=0.2f;

    [Header("Move info")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;
    [SerializeField] public bool secondJump = false;
    public int jumpTimes=1;

    [Header("Dash info")]
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashTimer;
    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashCD;
    public float dashDir { get; private set; }
    public bool haveDashed=false;
    #endregion

    public SkillManager skill {  get; private set; }
    public GameObject sword; //{  get; private set; }

    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    //玩家状态列表
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerSecondJumpState SecondJumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState {  get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttack { get; private set; }
    public PlayerCounterAttackState CounterAttack { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerAimSwordState aimSword { get; private set; }
    public PlayerCatchSwordState catchSword { get;private set; }
    #endregion

    protected override void Awake()//Animation controller
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this,StateMachine,"Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        SecondJumpState = new PlayerSecondJumpState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        PrimaryAttack = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
        CounterAttack = new PlayerCounterAttackState(this, StateMachine, "CounterAttack");
        DeadState = new PlayerDeadState(this, StateMachine,"Die");
        aimSword = new PlayerAimSwordState(this, StateMachine, "AimSword");
        catchSword = new PlayerCatchSwordState(this, StateMachine,"CatchSword");
    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
        skill = SkillManager.instance;

    }
    protected override void Update()
    {
        base.Update();
        //运用当前状态的Update函数
        StateMachine.currentState.Update();
        CheckForDashInput();
    }

    #region Functions
    public IEnumerator BusyFor(float _second)
    {
        isBusy= true;
        yield return new WaitForSeconds(_second);
        isBusy = false;
    }   

    public void AnimationTrigger()=>StateMachine.currentState.AnimationFinishTrigger();
    

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(DeadState);
    }
    #region Dash
    private void CheckForDashInput()
    {
        if (IsWallDetected())        
            return;
        
        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer<0)
        {            
            
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;

            if (!IsGroundDetected()){
                if (!haveDashed){
                haveDashed = true;
                StateMachine.ChangeState(DashState);
                }
            }
            else
            {
                StateMachine.ChangeState(DashState);
            }
        }
    }
    public void AssignSword(GameObject _newSword)
    {
        sword = _newSword;
    }
    public void CatchTheSword()
    {
        StateMachine.ChangeState(catchSword);
        Destroy(sword);
    }
    #endregion
    #endregion
}
