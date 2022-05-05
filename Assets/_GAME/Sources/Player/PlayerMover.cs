using UnityEngine;

public class PlayerMover : MonoBehaviour,IPlayerMover
{

    private PlayerAttacker _playerAttacker;
    private bool _isMoving;
    
    public float speed = 10.0F;

    // Start is called before the first frame update
    void Start()
    {
        _playerAttacker = GetComponent<PlayerAttacker>();
        _isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float verticalSpeed = Input.GetAxis("Vertical") * speed;
        float horizontalSpeed = Input.GetAxis("Horizontal") * speed;
        if (Mathf.Abs(verticalSpeed) <= 0.1 && Mathf.Abs(horizontalSpeed) <= 0.1)
        {
            _isMoving = false;
            _playerAttacker.Attack();
            return; 
        }

        _isMoving = true;
        
        var movementVector = new Vector3{x=horizontalSpeed, z = verticalSpeed};
        movementVector.Normalize();
        movementVector *= speed * Time.deltaTime;
        var degreeMovementVector = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,degreeMovementVector,0);

        transform.position += movementVector;
    }
    

    public bool IsMoving => _isMoving;
}
