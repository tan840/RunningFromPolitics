using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ItemCollection : MonoBehaviour
{
    private Dictionary<string, int> collectedCounts = new Dictionary<string, int>();

    private Dictionary<string, UnityEvent> itemEvents = new Dictionary<string, UnityEvent>();

    public void CollectItem(string itemType)
    {
        if (collectedCounts.ContainsKey(itemType))
        {
            collectedCounts[itemType]++;

            itemEvents[itemType]?.Invoke();

            if (collectedCounts[itemType] >= 5)
            {
                Debug.Log($"Collect 5 {itemType}s now!");
            }
        }
        else
        {
            Debug.LogWarning($"Item type '{itemType}' not found.");
        }
    }

    public void AddItemType(string itemType)
    {
        if (!collectedCounts.ContainsKey(itemType))
        {
            collectedCounts.Add(itemType, 0);
            itemEvents.Add(itemType, new UnityEvent());
        }
        else
        {
            Debug.LogWarning($"Item type '{itemType}' already exists.");
        }
    }

    public void SubscribeToItemEvent(string itemType, UnityAction action)
    {
        if (itemEvents.ContainsKey(itemType))
        {
            itemEvents[itemType].AddListener(action);
        }
        else
        {
            Debug.LogWarning($"Item type '{itemType}' not found.");
        }
    }
}