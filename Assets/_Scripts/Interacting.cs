using System;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    // The key to press to interact with an object
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    // The range at which we can interact with objects
    [SerializeField] private float interactingRange = 2;

    private void Update()
    {
        // Check if user pressed down the interactionKey
        if (Input.GetKeyDown(interactionKey))
        {
            // Attempt to interact
            AttemptInteraction();
        }
    }

    private void AttemptInteraction()
    {
        // Create a ray from the current position in the forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        
        // Store information about the hit in this variable
        RaycastHit hit;
        
        // Create a layer mask that represents every layer expect the players
        LayerMask everythingExceptPlayers = ~(1 << LayerMask.NameToLayer("Player"));
        
        // Combine the layer mask with the one that raycasts use per default in order 
        // to remove the Player layer from the list of layers to raycast against
        LayerMask layerMask = Physics.DefaultRaycastLayers & everythingExceptPlayers;

        // Perform the raycast out
        if (Physics.Raycast(ray, out hit, interactingRange, layerMask))
        {
            // Get the interactable component of the hit object
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            // Check if the interactable exists
            if (interactable != null)
            {
                // Interact with the interactable
                interactable.Interact(this.gameObject);
            }
        }
    }
}
