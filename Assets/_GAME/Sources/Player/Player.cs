using UnityEngine;

public class Player : SingletonMonobehavior<Player>
{
    public float Health = 100.0f;
    public float attackDamage = 20.0f;

    public void ReceiveDamage(float damage)
    {
        Debug.Log($"taking damage == {damage}");
        Health -= damage;
    }

    void Update()
    {
        if (Health <= 0)
        {
            Debug.Log("DIEEEEEEEEE");
        }
    }
}