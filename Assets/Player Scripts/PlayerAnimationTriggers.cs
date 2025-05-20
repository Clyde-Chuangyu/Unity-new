using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player=> GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackOccur.position, player.attackOccurRedius);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats enemyStas = hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(enemyStas);                
            }
        }
    }
    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();       
    }
}
