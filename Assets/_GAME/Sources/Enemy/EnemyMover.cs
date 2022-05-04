using System.Collections;
using System.Collections.Generic;
using Assets._GAME.Sources.Enemy;
using UnityEngine;

public class EnemyMover : MonoBehaviour,IEnemyMover
{
    private bool _canAttack = false;

    public float speed = 0.1f;
    public float distance = 2.0f;
    private KeyboardPlayerMover _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = KeyboardPlayerMover.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
        var lookAtGoal = new Vector3(_player.transform.position.x, 
            this.transform.position.y, 
            _player.transform.position.z);
        var direction = lookAtGoal - this.transform.position;
        var degreeMovementVector = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,degreeMovementVector,0);
        if (direction.magnitude <= distance)
        {
            _canAttack = true;
            return;
        }
        direction.Normalize();
       
        direction *= speed * Time.deltaTime;
        transform.position += direction;
        
    }

    public bool CanAttack => _canAttack;
}
