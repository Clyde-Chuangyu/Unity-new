using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Collison
    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundMask;
    public Transform attackOccur;
    public float attackOccurRedius;
    #endregion

    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D capsule { get; private set; }
    #endregion

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    [SerializeField] public int facingDir { get; private set; } = 1;
    [SerializeField] protected bool facingRight = true;

    public System.Action onFlipped;

    protected virtual void Awake()
    {
        if (facingRight == false)
            facingDir = -1;
    }
    protected virtual void Start()
    {
        fx=GetComponent<EntityFX>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats =GetComponent<CharacterStats>();
        capsule = GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Update()
    {

    }

    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundMask);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundMask);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackOccur.position,attackOccurRedius);
    }
    #endregion

    #region Velocity
    public void SetVelocity(float _xVelocity, float _yVelocity)//设置速度
    {
        if(isKnocked) return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    public void ZeroVelocity() 
    { 
        if(isKnocked) return;
        rb.velocity = new Vector2(0, 0);
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        if(onFlipped!=null)
            onFlipped();
    }
    public virtual void FlipController(float _X)
    {
        if (_X > 0 && !facingRight)
            Flip();
        else if (_X < 0 && facingRight)
            Flip();
    }
    #endregion

    #region Attack
    public void DamageEffect() 
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }
    public virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity= new Vector2(knockbackDirection.x*-facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration); 
        isKnocked=false;
    }
    #endregion
    public virtual void Die()
    {

    }
}
