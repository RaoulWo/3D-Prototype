using UnityEngine;

public class Movement : MonoBehaviour
{
    // The movement speed in units per second
    [SerializeField] private float moveSpeed = 6f;
    // The height of a jump in units
    [SerializeField] private float jumpHeight = 2f;
    // The rate at which the vertical speed will be reduced in units per second
    [SerializeField] private float gravity = 20f;
    // The degree to which the movement can be controlled while in midair
    [Range(0f, 10f), SerializeField] private float airControl = 5f;
    
    // The current movement direction
    private Vector3 _moveDirection = Vector3.zero;
    // The cached reference to the character controller
    private CharacterController _characterController;

    private void Start()
    {
        // Cache the CharacterController
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        // Get the user input
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        
        // Multiply the input by the desired movement speed
        input *= moveSpeed;
        
        // The CharacterController's Move method uses world-space directions,
        // therefore the direction needs to be converted to world space
        input = transform.TransformDirection(input);
        
        // Check if the controller is touching the ground
        if (_characterController.isGrounded)
        {
            _moveDirection = input;
            
            // Check if the user is pressing the jump button
            if (Input.GetButton("Jump"))
            {
                // Calculate the amount of upward speed needed to achieve the jumpHeight
                _moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
            else
            {
                // Set the downward movement to 0 when grounded and not jumping 
                _moveDirection.y = 0;
            }
        }
        else
        {
            // Preserve the current y-direction
            input.y = _moveDirection.y;
            // Slowly bring the movement towards the desired input
            _moveDirection = Vector3.Lerp(_moveDirection, input, airControl * Time.deltaTime);
        }
        
        // Bring the movement down by applying gravity over time
        _moveDirection.y -= gravity * Time.deltaTime;
        
        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}
