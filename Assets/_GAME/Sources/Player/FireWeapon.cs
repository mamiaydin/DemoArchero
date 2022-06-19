using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    private LevelManager _levelManager;
    private Player _player;
    private PlayerAttacker _playerAttacker;
    public int chainAttackNumber = 5;
    

    void Start()
    {
        _levelManager = LevelManager.Instance;
        _player = Player.Instance;
        _playerAttacker = _player.GetComponent<PlayerAttacker>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))  
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            enemy.ReceiveDamage(_player.attackDamage);
            chainAttackNumber--;
            if (chainAttackNumber <=0)
            {
                Destroy(gameObject);
            }
            else
            {
                var nextEnemy = FindClosestEnemy(enemy);
                if (nextEnemy == null)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log($"next enemy : {nextEnemy.name}");
                    var lookAtGoal = new Vector3(nextEnemy.transform.position.x, 
                        this.transform.position.y, 
                        nextEnemy.transform.position.z);
                    var direction =   this.transform.position - lookAtGoal;
                    var degreeMovementVector = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0,degreeMovementVector,0);

                    var b = GetComponent<Rigidbody>();
                    b.velocity = Vector3.zero;
                    b.AddRelativeForce( transform.forward * _playerAttacker.bulletSpeed);
                }
            }
            //todo: inside enemy
            if (enemy.Health <=0)
            {
                _levelManager.RemoveEnemy(enemy);
                Destroy(other.gameObject);
            }
        }
    }
    
    private Enemy FindClosestEnemy(Enemy lastHitEnemy)
    {
        var lasthitEnemy = new List<Enemy>{lastHitEnemy};
        var enemyList = _levelManager.enemyList.Except(lasthitEnemy).ToList();
        if (!enemyList.Any())
        {
            Debug.Log("Level Cleared!");
            return null;
        }
        var distances = enemyList.Select(x => (transform.position - x.transform.position).magnitude).ToList();
        var minDistance = distances.Min();
        var closestEnemyIndex = distances.FindIndex(x=>Math.Abs(x - minDistance) < float.Epsilon);
        
        return enemyList[closestEnemyIndex];
    }
}