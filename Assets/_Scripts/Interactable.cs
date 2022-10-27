using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public void Interact(GameObject fromObject)
    {
        Debug.LogFormat("I've been interacted with by {0}!", fromObject);
    }
}
