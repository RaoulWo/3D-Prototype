using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // The turn speed or mouse sensitivity
    [SerializeField] private float turnSpeed = 90f;
    // The upper limit for how far the head can tilt
    [SerializeField] private float headUpperAngleLimit = 85f;
    // The lower limit for how far the head can tilt
    [SerializeField] private float headLowerAngleLimit = -80f;
    
    // The current rotation around the y-axis from the start
    private float _yaw = 0f;
    // The current rotation around the x-axis from the start
    private float _pitch = 0f;
    
    // The orientation of the body at game start
    private Quaternion _bodyStartOrientation;
    // The orientation of the head at game start
    private Quaternion _headStartOrientation;
    
    // The reference to the head object to rotate the camera
    private Transform _head;

    private void Start()
    {
        // Set the head transform of the child object
        _head = GetComponentInChildren<Camera>().transform;
        
        // Cache the starting orientation of the body and head
        _bodyStartOrientation = transform.localRotation;
        _headStartOrientation = _head.localRotation;
        
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get the current movement
        float horizontalMovement = Input.GetAxis("Mouse X") * Time.deltaTime * turnSpeed;
        float verticalMovement = Input.GetAxis("Mouse Y") * Time.deltaTime * turnSpeed * -1;
        
        // Update the yaw and pitch values
        _yaw += horizontalMovement;
        _pitch += verticalMovement;
        
        // Clamp the pitch
        _pitch = Mathf.Clamp(_pitch, headLowerAngleLimit, headUpperAngleLimit);
        
        // Compute the rotation for the body by rotating around the y-axis
        Quaternion bodyRotation = Quaternion.AngleAxis(_yaw, Vector3.up);
        // Compute the rotation for the head by rotating around the x-axis
        Quaternion headRotation = Quaternion.AngleAxis(_pitch, Vector3.right);
        
        // Update the local rotations for the body and head
        transform.localRotation = bodyRotation * _bodyStartOrientation;
        _head.localRotation = headRotation * _headStartOrientation;
    }
}
