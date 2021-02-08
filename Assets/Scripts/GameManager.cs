using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Helpers;

public class GameManager : MonoBehaviour
{
    public List<Transform> TopRoomPositions;
    public List<Transform> MiddleRoomPositions;
    public List<Transform> BottomRoomPositions;
    public List<GameObject> RoomPrefabs;

    private NavMeshSurface[] surfaces;

    private string[,] pathDirection = new string[3, 3]
    {
        { "NoneRight", "LeftRight", "LeftBottom" },
        { "", "RightBottom", "TopLeft" },
        { "", "TopRight", "LeftNone" }
    };

    private readonly Dictionary<string, GameObject> registeredObjects = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, int> playerInventory = new Dictionary<string, int>();
    private readonly Dictionary<string, int> itemQuantitiesInWorld = new Dictionary<string, int>();

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            var roomName = pathDirection[0, i];

            if (!string.IsNullOrWhiteSpace(roomName))
            {
                var roomPrefab = RoomPrefabs.FirstOrDefault(x => x.name.EndsWith(roomName));
                var roomPosition = TopRoomPositions[i];
                Instantiate(roomPrefab, roomPosition.position, roomPosition.rotation);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            var roomName = pathDirection[1, i];

            if (!string.IsNullOrWhiteSpace(roomName))
            {
                var roomPrefab = RoomPrefabs.FirstOrDefault(x => x.name.EndsWith(roomName));
                var roomPosition = MiddleRoomPositions[i];
                Instantiate(roomPrefab, roomPosition.position, roomPosition.rotation);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            var roomName = pathDirection[2, i];

            if (!string.IsNullOrWhiteSpace(roomName))
            {
                var roomPrefab = RoomPrefabs.FirstOrDefault(x => x.name.EndsWith(roomName));
                var roomPosition = BottomRoomPositions[i];
                Instantiate(roomPrefab, roomPosition.position, roomPosition.rotation);
            }
        }

        surfaces = GameObject
            .FindGameObjectsWithTag(GameTag.Room)
            .Select(x => x.GetComponent<NavMeshSurface>())
            .ToArray();

        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
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
