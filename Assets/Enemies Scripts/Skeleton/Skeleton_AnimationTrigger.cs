using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_AnimationTrigger : MonoBehaviour
{
    private Skeleton skeleton=>GetComponentInParent<Skeleton>();
    private void AnimationTrigger()
    {
        skeleton.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skeleton.attackOccur.position, skeleton.attackOccurRedius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats playerStats = hit.GetComponent<PlayerStats>();
                skeleton.stats.DoDamage(playerStats); 
            }
        }
    }
    protected void OpenCounterWindow()=>skeleton.OpenCounterAttackWindow();
    protected void CloseCounterWindow()=>skeleton.CloseCounterAttackWindow();
    
}
