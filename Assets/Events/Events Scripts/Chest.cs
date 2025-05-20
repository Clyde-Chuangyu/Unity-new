using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]protected GameObject tip;
    public Animator animator;
    protected string animBoolName="Open";
    protected bool canBeOpened=false;
    protected bool isOpened=false;
    private GameObject go;
    public PlayerStats stats;
    public Player player;
    protected float Timer;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        go = GameObject.FindGameObjectWithTag("Player");
        player = go.GetComponent<Player>();
        stats = go.GetComponent<PlayerStats>();
        tip.SetActive(false);
    }
    protected virtual void Update()
    {
        CheckOpen();
        Timer -= Time.deltaTime;
        if (Timer < 0)
            tip.SetActive(false);
    }
    protected void CheckOpen()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (canBeOpened && !isOpened)
            {
                animator.SetBool(animBoolName,true);
                Effect();
                isOpened = true;
            }
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canBeOpened = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canBeOpened = false;
        }
    }
    protected virtual void Effect(){}
}
