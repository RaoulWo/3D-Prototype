using UnityEngine;

public class Pushing : MonoBehaviour
{
    // Defines the possible types of pushing that can be applied
    public enum PushMode
    {
        // Do not allow any pushing
        NoPushing,
        // Push by directly setting the velocity of things we hit
        DirectlySetVelocity,
        // Push by applying a physical force to the impact point
        ApplyForces
    }
    
    // The type of pushing selected
    [SerializeField] private PushMode pushMode = PushMode.DirectlySetVelocity;
    // The amount of force to apply when pushMode is set to ApplyForces
    [SerializeField] private float pushPower = 5f;

    // Is called when a CharacterController of the object touches another collider
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Do nothing if pushMode is NoPushing
        if (pushMode == PushMode.NoPushing)
        {
            return;
        }
        
        // Get the rigidbody attached to the hit collider
        Rigidbody hitRigidbody = hit.rigidbody;
        
        // Do nothing if rigidbody does not exist or is kinematic
        if (hitRigidbody == null || hitRigidbody.isKinematic)
        {
            return;
        }
        
        // Get a reference to the CharacterController
        CharacterController controller = hit.controller;
        
        // Calculate the world position of the lowest point on the character controller
        float footPosition = controller.transform.position.y - controller.center.y - controller.height / 2;
        
        // Do not push any object beneath the footPosition
        if (hit.point.y <= footPosition)
        {
            return;
        }
        
        // Apply the push based on the chosen pushMode
        switch (pushMode)
        {
            case PushMode.DirectlySetVelocity:
                // Directly apply the velocity
                hitRigidbody.velocity = controller.velocity;
                break;
            case PushMode.ApplyForces:
                // Calculate how much force to apply
                Vector3 force = controller.velocity * pushPower;
                // Apply the force to the object 
                hitRigidbody.AddForceAtPosition(force, hit.point);
                break;
        }
    }
}
