using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item _item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add the item to the player's inventory
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(_item);   
                Destroy(gameObject); // Remove the item from the scene
            }
        }
    }
   
}
