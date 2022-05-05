using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;


public class PlayerAttacker : MonoBehaviour
{
    private LevelManager _levelManager;
    private Player _player;
    private Enemy _enemy;

    public Transform animationTransform;
    public GameObject bulletPrefab;
    public float bulletSpeed = 200.0f;
    
    public bool IsAttacking;

    void Start()
    {
        _levelManager = LevelManager.Instance;
        _player = Player.Instance;
        _enemy = Enemy.Instance;
    }

    private Enemy FindClosestEnemy()
    {
        var enemyList = _levelManager.enemyList;
        if (!enemyList.Any())
        {
            Debug.Log("Level Cleared!");
            return null;
        }
        var distances = enemyList.Select(x => (_player.transform.position - x.transform.position).magnitude).ToList();
        var minDistance = distances.Min();
        var closestEnemyIndex = distances.FindIndex(x=>Math.Abs(x - minDistance) < float.Epsilon);
        
        return enemyList[closestEnemyIndex];
    }

    public void Attack()
    {
        var enemy = FindClosestEnemy();
        if (enemy == null)
        {
            Debug.Log("Level Cleared!");
            return;
        }
        var lookAtGoal = new Vector3(enemy.transform.position.x, 
            this.transform.position.y, 
            enemy.transform.position.z);
        var direction =  lookAtGoal - this.transform.position;
        var degreeMovementVector = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,degreeMovementVector,0);
        
        if (IsAttacking)
            return;
        
        IsAttacking = true;
        var attackSequence = DOTween.Sequence();
        attackSequence.AppendInterval(0.1f);
        attackSequence.AppendCallback(TryToHit);
        
        var animationSequence = DOTween.Sequence();
        animationSequence.Append(animationTransform.DOLocalRotate(Vector3.right * -20, 0.3f).SetEase(Ease.OutSine));
        animationSequence.Join(animationTransform.DOScale(new Vector3(3f, 0, 1.5f),0.3f));
        animationSequence.Append(attackSequence);
        animationSequence.Append(animationTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.InSine));
        animationSequence.Join(animationTransform.DOScale(Vector3.one,0.3f));
        
        animationSequence.OnComplete(StopAttack);
    }
    
    public void StopAttack()
    {
        // animation stopped where it is currently
        //animationSequence.Kill();
        // animation complete and stop
        animationTransform.DOComplete();
        IsAttacking = false;
    }

    void TryToHit()
    {
        var bulletGo = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var enemy = FindClosestEnemy();
        if (enemy == null)
        {
            Debug.Log("Level Cleared!");
            return;
        }
        
        //TODO : fix rotation
        var lookAtGoal = new Vector3(enemy.transform.position.x, 
            this.transform.position.y, 
            enemy.transform.position.z);
        var direction =   this.transform.position - lookAtGoal;
        var degreeMovementVector = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        bulletGo.transform.rotation = Quaternion.Euler(0,degreeMovementVector,0);


        var b = bulletGo.GetComponent<Rigidbody>();
        b.AddRelativeForce( transform.forward  * bulletSpeed);
        Destroy(bulletGo,4);
    }
}