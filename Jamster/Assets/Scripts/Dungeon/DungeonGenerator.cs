using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] public List<DungeonRoom> roomPrefabs = new List<DungeonRoom>();
    public List<Vector3> allRoomPosition = new List<Vector3>();

    // Grid Variables
    [SerializeField] private Vector2 gridSize = new Vector2();
    [SerializeField] private Vector2 roomSize = new Vector2();

    [SerializeField] private int roomLeft; // Total rooms to generate Aroud
    private List<List<DungeonRoom>> roomList = new List<List<DungeonRoom>>();
    private Vector2 startingPointPosition;

    public void Start()
    {
        ChooseStartingPoint();
        CreateGrid();
        RecursiveRoomCreation(roomLeft, startingPointPosition);
        OpenRoomEverySide();
        InstantiateGrid();
    }

    // Choose a random starting point
    private void ChooseStartingPoint()
    {
        //startingPointPosition = new Vector2(Mathf.Floor(Random.Range(0, gridSize.x)), Mathf.Floor(Random.Range(0, gridSize.y)));
        startingPointPosition = new Vector2(Mathf.Floor(gridSize.x/2), Mathf.Floor(gridSize.y/2));
    }

    // Create the grid with room prefabs
    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            roomList.Add(new List<DungeonRoom>());

            for (int y = 0; y < gridSize.y; y++)
            {
                if (x == (int)startingPointPosition.x && y == (int)startingPointPosition.y)
                {
                    roomList[x].Add(roomPrefabs[0]); // Place the starting room
                }
                else
                {
                    roomList[x].Add(null); // Empty cell
                }
            }
        }
    }

    // Recursive function to create rooms
    private void RecursiveRoomCreation(int roomLeft, Vector2 currentPosition)
    {
        // Base case: no more rooms to place
        if (roomLeft <= 0)
            return;

        // Possible directions (right, left, up, down)
        Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        // Try placing rooms in the available directions
        foreach (Vector2 direction in directions)
        {
            Vector2 nextPosition = currentPosition + direction;

            // Check if the next position is valid
            if (IsPositionInGrid(nextPosition) && roomList[Mathf.FloorToInt(nextPosition.x)][Mathf.FloorToInt(nextPosition.y)] == null && Random.Range(0,3) != 0)
            {
                // Place the room
                roomList[Mathf.FloorToInt(nextPosition.x)][Mathf.FloorToInt(nextPosition.y)] = roomPrefabs[0];
                roomLeft--; // Decrement room count

                // Recursively try to create more rooms from the new position
                RecursiveRoomCreation(roomLeft, nextPosition);
            }
        }
    }

    // Check if the position is within bounds
    private bool IsPositionInGrid(Vector2 position)
    {
        return position.x >= 0 && position.x < gridSize.x && position.y >= 0 && position.y < gridSize.y;
    }

    //Set room to Prefab with every possible open
    private void OpenRoomEverySide()
    {
        List<bool> lRoomOpen = new List<bool>();
        for (int i = 0; i < 4; i++)
        {
            lRoomOpen.Add(false);
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.x; y++)
            {
                for (int room = 0; room < lRoomOpen.Count; room++)
                {
                    lRoomOpen[room] = false;
                }
                if (roomList[x][y] != null)
                {
                    if (IsPositionInGrid(new Vector2(x, y + 1)) && roomList[x][y + 1] != null) lRoomOpen[0] = true;
                    if (IsPositionInGrid(new Vector2(x, y - 1)) && roomList[x][y - 1] != null) lRoomOpen[1] = true;
                    if (IsPositionInGrid(new Vector2(x - 1, y)) && roomList[x - 1][y] != null) lRoomOpen[2] = true;
                    if (IsPositionInGrid(new Vector2(x + 1, y)) && roomList[x + 1][y] != null) lRoomOpen[3] = true;

                    roomList[x][y] = FindRoomWithOpens(lRoomOpen);
                }
            }
        }
    }
    
    //Check in List Of room prefab compatible rooms; 
    private DungeonRoom FindRoomWithOpens(List<bool> lRoomOpens)
    {
        
        List<DungeonRoom> roomPosibilities = new List<DungeonRoom>();
        for (int x = 0; x < roomPrefabs.Count; x++)
        {
            if (lRoomOpens[0] == roomPrefabs[x].roomOpens[0] &&
                lRoomOpens[1] == roomPrefabs[x].roomOpens[1] &&
                lRoomOpens[2] == roomPrefabs[x].roomOpens[2] &&
                lRoomOpens[3] == roomPrefabs[x].roomOpens[3]
                )
            {
                roomPosibilities.Add(roomPrefabs[x]);
            }
        }
        if (roomPosibilities.Count > 0) return roomPosibilities[Random.Range(0, roomPosibilities.Count - 1)];
        else return roomPrefabs[0];
    }

    // Instantiate the rooms in the scene
    private void InstantiateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (roomList[x][y] != null)
                {
                    DungeonRoom instantiatedRoom = Instantiate(roomList[x][y]);
                    instantiatedRoom.transform.position = new Vector3(x * roomSize.x, 0, y * roomSize.y);
                }
            }
        }
    }
}
