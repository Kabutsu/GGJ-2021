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

    private KeyValuePair<string, string>[,] pathDirection = new KeyValuePair<string, string>[3, 3]
    {
        { new KeyValuePair<string, string>("", ""), new KeyValuePair<string, string>("", ""), new KeyValuePair<string, string>("", "") },
        { new KeyValuePair<string, string>("", ""), new KeyValuePair<string, string>("", ""), new KeyValuePair<string, string>("", "") },
        { new KeyValuePair<string, string>("", ""), new KeyValuePair<string, string>("", ""), new KeyValuePair<string, string>("", "") }
    };

    private readonly Dictionary<string, GameObject> registeredObjects = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, int> playerInventory = new Dictionary<string, int>();
    private readonly Dictionary<string, int> itemQuantitiesInWorld = new Dictionary<string, int>();

    void Start()
    {
        var row = 0;
        var col = Random.Range(0, 3);
        var prevRoom = pathDirection[row, col];

        for (int i = 0; i < 9; i++)
        {
            var possibleNextRooms = GetPathOptions(prevRoom, row, col);
            prevRoom = possibleNextRooms[possibleNextRooms.Count == 1 ? 0 : Random.Range(0, possibleNextRooms.Count)];

            Debug.Log($"{prevRoom.Key}{prevRoom.Value} at [{row}, {col}]");

            pathDirection[row, col] = prevRoom;

            if (prevRoom.Value == "Left") col--;
            if (prevRoom.Value == "Right") col++;

            if (prevRoom.Value == "Bottom") row++;

            if (prevRoom.Value == "None") break;
        }

        for (int i = 0; i < 3; i++)
        {
            var roomName = pathDirection[0, i];

            if (!string.IsNullOrWhiteSpace(roomName.Key))
            {
                var roomPrefab = RoomPrefabs.FirstOrDefault(x => x.name.EndsWith($"{roomName.Key}{roomName.Value}"));
                var roomPosition = TopRoomPositions[i];
                Instantiate(roomPrefab, roomPosition.position, roomPosition.rotation);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            var roomName = pathDirection[1, i];

            if (!string.IsNullOrWhiteSpace(roomName.Key))
            {
                var roomPrefab = RoomPrefabs.FirstOrDefault(x => x.name.EndsWith($"{roomName.Key}{roomName.Value}"));
                var roomPosition = MiddleRoomPositions[i];
                Instantiate(roomPrefab, roomPosition.position, roomPosition.rotation);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            var roomName = pathDirection[2, i];

            if (!string.IsNullOrWhiteSpace(roomName.Key))
            {
                var roomPrefab = RoomPrefabs.FirstOrDefault(x => x.name.EndsWith($"{roomName.Key}{roomName.Value}"));
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

    private List<KeyValuePair<string, string>> GetPathOptions(KeyValuePair<string, string> prev, int row, int col)
    {
        string inDirection = OppositeDirection(prev.Value);

        var canGoRight = inDirection != "Right" && col < 2 && string.IsNullOrWhiteSpace(pathDirection[row, col + 1].Key);
        var canGoLeft = inDirection != "Left" && col > 0 && string.IsNullOrWhiteSpace(pathDirection[row, col - 1].Key);
        var canGoDown = row < 2;
        var canGoNone = row == 2;

        var possibleOptions = new List<KeyValuePair<string, string>>();

        if (canGoRight) possibleOptions.Add(new KeyValuePair<string, string>(inDirection, "Right"));
        if (canGoLeft) possibleOptions.Add(new KeyValuePair<string, string>(inDirection, "Left"));
        if (canGoDown) possibleOptions.Add(new KeyValuePair<string, string>(inDirection, "Bottom"));
        if (canGoNone) possibleOptions.Add(new KeyValuePair<string, string>(inDirection, "None"));

        return possibleOptions;
    }

    private string OppositeDirection(string? direction)
    {
        switch (direction)
        {
            case "Left":
                return "Right";
            case "Right":
                return "Left";
            case "Top":
                return "Bottom";
            case "Bottom":
                return "Top";
            default:
                return "None";
        }
    }

    public void Victory()
    {
        Debug.Log("Victory!");
    }

    public void GameOver()
    {

    }

    public void OnQuit()
    {
        Application.Quit();
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
