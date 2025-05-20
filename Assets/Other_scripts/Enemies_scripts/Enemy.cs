using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask PlayerMask;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] public float playerCheckDistance;
    [SerializeField] protected Transform attackCheck;
    [SerializeField] public float attackCheckDistance;
    [SerializeField] Player player;

    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Header("Attack info")]
    [SerializeField] public float attackCD;
    public float lastTimeAttack;

    [Header("Stunned info")]
    public float stunnedDuration;
    public Vector2 stunnedDirection;
    private bool canBeStunned;
    [SerializeField] protected GameObject currentImage;

    public EnemyStateMachine stateMachine { get; private set; }
    public string lastAnimBoolName {  get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }
    protected override void Start()
    {
        base.Start();
        player = PlayerManager.instance.player;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        currentImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        currentImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if(canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public virtual void AnimationFinishTrigger()=> stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D isPlayerDetected() => 
        Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerCheckDistance, PlayerMask);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + playerCheckDistance * facingDir, playerCheck.position.y));
        Gizmos.DrawLine(attackCheck.position, new Vector3(attackCheck.position.x + attackCheckDistance * facingDir, attackCheck.position.y));
    }
    public virtual void AssignLastAnimName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }
    public override void Die()
    {
        base.Die();
        if (player.sword)
            Destroy(player.sword);      
    }
}
