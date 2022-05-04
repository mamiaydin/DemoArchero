using UnityEngine;

namespace Assets._GAME.Sources.Enemy
{
    [RequireComponent(typeof(EnemyAttacker)), RequireComponent(typeof(EnemyMover))]
    public class Enemy : MonoBehaviour
    {
        public float Health = 100.0f;
        private EnemyAttacker _enemyAttacker;
        private EnemyMover _enemyMover;

        void Awake()
        {
            _enemyMover = GetComponent<EnemyMover>();
            _enemyAttacker = GetComponent<EnemyAttacker>();
        }
        
        void Update()
        {
            if (_enemyAttacker.CanAttack)
            {
                //TODO: enemy can still rotate 
                //TODO: this method runs twice
                _enemyAttacker.Attack();
            }
            else
            {
                _enemyMover.Move();
            }
        }
    }
}