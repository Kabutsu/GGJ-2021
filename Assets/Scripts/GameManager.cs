using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Helpers;

public class GameManager : MonoBehaviour
{
    private readonly Dictionary<string, GameObject> registeredObjects = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, int> playerInventory = new Dictionary<string, int>();
    private readonly Dictionary<string, int> itemQuantitiesInWorld = new Dictionary<string, int>();

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Victory()
    {
        Debug.Log("Victory!");
    }

    public void GameOver()
    {

    }

    public void Register(string name, GameObject gameObject)
    {
        registeredObjects.Add(name, gameObject);
    }

    public void AddWorldItem(string name)
    {
        if (itemQuantitiesInWorld.ContainsKey(name))
        {
            itemQuantitiesInWorld[name]++;
        }
        else
        {
            itemQuantitiesInWorld.Add(name, 1);
        }
    }

    public void AddInventory(string name, int amount = 1)
    {
        if (playerInventory.ContainsKey(name))
        {
            playerInventory[name] += amount;
        } else
        {
            playerInventory.Add(name, amount);
        }

        CheckEffects(name);
    }

    public int CheckInventory(string name)
    {
        if (playerInventory.TryGetValue(name, out int amount))
        {
            return amount;
        }

        return 0;
    }

    private int CheckItemQuantityInWorld(string name)
    {
        if (itemQuantitiesInWorld.TryGetValue(name, out int amount))
        {
            return amount;
        }

        return 0;
    }

    private void CheckEffects(string itemName)
    {
        switch (itemName)
        {
            case "Key":
                if (CheckInventory(itemName) >= CheckItemQuantityInWorld(itemName))
                    OpenDoor();
                break;
            default:
                break;
        }
    }

    private void OpenDoor()
    {
        registeredObjects[GameTag.Door].GetComponent<DoorController>().Open();
    }
}
