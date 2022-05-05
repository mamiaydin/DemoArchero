using UnityEngine;


[RequireComponent(typeof(EnemyAttacker)), RequireComponent(typeof(EnemyMover))]
public class Enemy : SingletonMonobehavior<Enemy>
{
    public float Health = 100.0f;
    private EnemyAttacker _enemyAttacker;
    private EnemyMover _enemyMover;

    void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyAttacker = GetComponent<EnemyAttacker>();
    }
    
    public void ReceiveDamage(float damage)
    {
        Debug.Log($"dealing damage == {damage}");
        Health -= damage;
    }

    void Update()
    {
        if (Health <= 0)
        {
            Destroy(this);
        }
        
        var canAttack = _enemyAttacker.CanAttack;
        _enemyMover.Move(canAttack);

        if (canAttack)
        {
            _enemyAttacker.Attack();
        }
        else
        {
            _enemyAttacker.StopAttack();
        }
    }
}