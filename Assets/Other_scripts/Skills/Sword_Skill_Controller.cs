using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    [SerializeField] private float returnSpeed;

    public Transform attackOccur;
    public float attackOccurRedius;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate=true;
    private bool isReturning;
    private bool canReturnSword=false;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();        
    }
    public void SetUpSword(Vector2 _direction,float _gravityScale,Player _player)
    {
        player = _player;
        rb.velocity = _direction;
        rb.gravityScale = _gravityScale;
        anim.SetBool("Rotating", true);
    }

    public void ReturnSword()
    {
        if (canReturnSword)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //rb.isKinematic = false;
            isReturning = true;
            transform.parent = null;
            canReturnSword = false;
        }
    }
    private void Update()
    {            
        if (canRotate)        
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position=Vector2.MoveTowards(transform.position,player.transform.position,returnSpeed*Time.deltaTime);
            if(Vector2.Distance(transform.position, player.transform.position)<1)
                player.CatchTheSword();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;
        if(collision.GetComponent<Enemy>() != null)
        {
            EnemyStats enemyStas = collision.GetComponent<EnemyStats>();
            player.stats.DoDamage(enemyStas);
        }

        anim.SetBool("Rotating", false);
        canReturnSword = true;
        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent= collision.transform;
    }
    
    
}
