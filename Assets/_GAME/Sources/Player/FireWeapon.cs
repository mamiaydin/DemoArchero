using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    private LevelManager _levelManager;
    private Player _player;
    

    void Start()
    {
        _levelManager = LevelManager.Instance;
        _player = Player.Instance;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))  
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(_player.attackDamage);
            if (enemy.Health <=0)
            {
                _levelManager.RemoveEnemy(enemy);
                Destroy(other.gameObject);
            }
            Destroy (gameObject);
        }
    }
}