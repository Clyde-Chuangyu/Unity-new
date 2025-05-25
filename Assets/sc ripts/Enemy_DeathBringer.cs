using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeathBringer : DeathBringerAdapter
{
    #region States
    public DeathBringerBattleState battleState { get; private set; }
    public DeathBringerAttackState attackState { get; private set; }
    public DeathBringerIdleState idleState { get; private set; }
    public DeathBringerDeadState deadState { get; private set; }
    public DeathBringerSpellCastState spellCastState { get; private set; }
    public DeathBringerTeleportState teleportState { get; private set; }
    #endregion

    public bool bossFightBegun;

    [Header("Spell cast details")]
    [SerializeField] private GameObject spellPrefab;
    public int amountOfSpells;
    public float spellCooldown;
    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;
    [SerializeField] private Vector2 spellOffset;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;

    protected override void Awake()
    {
        base.Awake();

        SetupDefailtFacingDir(-1);

        // 初始化状态
        idleState = new DeathBringerIdleState(this, stateMachine, "Idle", this);
        battleState = new DeathBringerBattleState(this, stateMachine, "Move", this);
        attackState = new DeathBringerAttackState(this, stateMachine, "Attack", this);
        deadState = new DeathBringerDeadState(this, stateMachine, "Idle", this);
        spellCastState = new DeathBringerSpellCastState(this, stateMachine, "SpellCast", this);
        teleportState = new DeathBringerTeleportState(this, stateMachine, "Teleport", this);
    }

    protected override void Start()
    {
        base.Start();
        
        // 确保必要的引用存在
        ValidateReferences();
        
        stateMachine.Initialized(idleState); // 注意：使用Initialized而不是Initialize
    }

    private void ValidateReferences()
    {
        // 验证必要的引用
        if (groundCheck == null)
            Debug.LogWarning("DeathBringer: GroundCheck is not assigned!");
        
        if (wallCheck == null)
            Debug.LogWarning("DeathBringer: WallCheck is not assigned!");
        
        if (attackOccur == null)
            Debug.LogWarning("DeathBringer: AttackOccur is not assigned!");
        
        if (arena == null)
            Debug.LogError("DeathBringer: Arena is not assigned! Teleport will not work properly!");
        
        if (spellPrefab == null)
            Debug.LogError("DeathBringer: SpellPrefab is not assigned! Spell casting will not work!");
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
        // 移除了销毁玩家剑的逻辑
    }

    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;

        float xOffset = 0;

        if (player.rb.velocity.x != 0)
            xOffset = player.facingDir * spellOffset.x;

        Vector3 spellPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + spellOffset.y);

        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<DeathBringerSpell_Controller>().SetupSpell(stats);
    }

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (capsule.size.y / 2)); // 使用capsule代替cd

        if (!GroundBelow() || SomethingIsAround())
        {
            FindPosition();
        }
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, groundMask); // 使用groundMask代替whatIsGround
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, groundMask);

    protected override void OnDrawGizmos()
    {
        // 不调用base.OnDrawGizmos()以避免playerCheck错误
        
        // 绘制基础的碰撞检测线
        if (groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        
        if (wallCheck != null)
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        
        if (attackOccur != null)
            Gizmos.DrawWireSphere(attackOccur.position, attackOccurRedius);

        // DeathBringer特有的Gizmos
        if (Application.isPlaying)
        {
            var groundHit = GroundBelow();
            if (groundHit)
                Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundHit.distance));
            Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
        }
        
        // 绘制攻击距离
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackCheckDistance * facingDir, transform.position.y));
    }

    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }
        return false;
    }

    public bool CanDoSpellCast()
    {
        return Time.time >= lastTimeCast + spellStateCooldown;
    }

    #region Animation Event Methods
    // 这些方法将被动画事件直接调用
    public void AnimationTriggerRelocate()
    {
        FindPosition();
    }

    public void AnimationTriggerMakeInvisible()
    {
        MakeTransparent(true);
    }

    public void AnimationTriggerMakeVisible()
    {
        MakeTransparent(false);
    }
    #endregion
}