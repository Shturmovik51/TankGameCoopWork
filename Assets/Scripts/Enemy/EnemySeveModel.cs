using UnityEngine;

[System.Serializable]
public class EnemySaveModel
{
    [SerializeField] private int _shootDamageForce;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _abilityID;
    [SerializeField] private bool _isDead;
    public int ShootDamageForce => _shootDamageForce;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public int AbilityID => _abilityID;
    public bool IsDead => _isDead;
    public EnemySaveModel(int shootDamageForce, int currentHealth, int maxHealth, int abilityID, bool isDead)
    {
        _shootDamageForce = shootDamageForce;
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
        _abilityID = abilityID;
        _isDead = isDead;
    }
}
