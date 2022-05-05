using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float speed = 0.1f;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = Player.Instance;
    }

    // Update is called once per frame
    public void Move(bool onlyRotate)
    {
        var lookAtGoal = new Vector3(_player.transform.position.x, 
            this.transform.position.y, 
            _player.transform.position.z);
        var direction = lookAtGoal - this.transform.position;
        var degreeMovementVector = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,degreeMovementVector,0);
        direction.Normalize();
        
        if (onlyRotate) return;
        direction *= speed * Time.deltaTime;
        transform.position += direction;
        
    }

}
