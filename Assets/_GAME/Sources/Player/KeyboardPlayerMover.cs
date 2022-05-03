using UnityEngine;

public class KeyboardPlayerMover : MonoBehaviour,IPlayerMover
{
    private bool _isMoving;
    
    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    
    // Start is called before the first frame update
    void Start()
    {
        _isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalSpeed = Input.GetAxis("Vertical") * speed;
        float horizontalSpeed = Input.GetAxis("Horizontal") * speed;
        if (Mathf.Abs(verticalSpeed) <= 0.1 && Mathf.Abs(horizontalSpeed) <= 0.1)
        {
            _isMoving = false;
            return; 
        }

        _isMoving = true;    
        
        verticalSpeed *= Time.deltaTime;
        horizontalSpeed *= Time.deltaTime;
        var movementVector = new Vector3{x=horizontalSpeed, z = verticalSpeed};
        movementVector.Normalize();
        movementVector *= speed;
        transform.position += movementVector;
        var degreeMovementVector = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
        Debug.Log($"degreeMovementVector== {degreeMovementVector}");
        transform.rotation = Quaternion.Euler(0,degreeMovementVector,0);
        
    }

    public bool IsMoving => _isMoving;
}
