using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item _item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add the item to the player's inventory
            PlayerLocomotion inventory = other.GetComponent<PlayerLocomotion>();
            if (inventory != null)
            {
                Debug.Log($"Picked up item: {_item.name}");
                Destroy(gameObject); // Remove the item from the scene
            }
        }
    }
   
}
