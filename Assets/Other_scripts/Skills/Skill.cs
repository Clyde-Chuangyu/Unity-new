using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cd;
    [SerializeField] protected bool unlocked=false;
    [SerializeField] protected int damage;
    protected float cdTimer;
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    protected virtual void Update()
    {
        cdTimer -= Time.deltaTime;
    }
    public void UnlockSkill()=>unlocked=true;
    public virtual bool CanUseSkill()
    {
        if (cdTimer < 0 && unlocked)
        {
            UseSkill();
            cdTimer = cd;
            return true;
        }
        return false;
    }
    public virtual void UseSkill()
    {

    }
}
