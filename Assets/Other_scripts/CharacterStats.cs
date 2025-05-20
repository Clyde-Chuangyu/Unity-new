using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("major stats")]
    public Stats damage;
    public Stats maxHP;
    public Stats strength;
    public Stats defense;
    [SerializeField]public int currentHP;
    public System.Action OnHealthChanged;

    protected virtual void Start()
    {
        currentHP = GetMaxHP();
    }
    
    public int GetMaxHP()
    {
        return maxHP.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage=damage.GetValue()+strength.GetValue()-defense.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }
    
    public virtual void TakeDamage(int _damage)
    {
        DcreaseHP(_damage);
        if(currentHP <= 0)
        {
            Die();
        }
    }
    protected virtual void DcreaseHP(int _damage)
    {
        currentHP -= _damage;
        if(OnHealthChanged!=null)
        {
            OnHealthChanged();
        }
    } 
    protected virtual void Die(){}
}
