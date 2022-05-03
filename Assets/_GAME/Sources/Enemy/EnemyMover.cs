using System.Collections;
using System.Collections.Generic;
using Assets._GAME.Sources.Enemy;
using UnityEngine;

public class EnemyMover : MonoBehaviour,IEnemyMover
{
    private bool _canAttack = false;

    public float speed = 0.1f;
    public float distance = 2.0f;
    private KeyboardPlayerMover player;
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<KeyboardPlayerMover>();
    }

    // Update is called once per frame
    void Update()
    {
        
        var lookAtGoal = new Vector3(player.transform.position.x, 
            this.transform.position.y, 
            player.transform.position.z);
        var direction = lookAtGoal - this.transform.position;
        if (direction.magnitude <= distance)
        {
            _canAttack = true;
            return;
        }
        direction.Normalize();
       
        direction *= speed * Time.deltaTime;
        transform.position += direction;
        var degreeMovementVector = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,degreeMovementVector,0);
        
    }

    public bool CanAttack => _canAttack;
}
