using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] public List<DungeonRoom> roomPrefabs = new List<DungeonRoom>();
    public List<Vector3> allRoomPosition = new List<Vector3>();

    // Grid Variables
    [SerializeField] private Vector2 gridSize = new Vector2();
    [SerializeField] private Vector2 roomSize = new Vector2();

    [SerializeField] private int roomLeft; // Total rooms to generate Aroud
    private List<List<DungeonRoom>> roomList;
    private Vector2 startingPointPosition;

    public void Start()
    {
        ChooseStartingPoint();
        CreateGrid();
        RecursiveRoomCreation(roomLeft, startingPointPosition);
        InstantiateGrid();
    }

    // Choose a random starting point
    private void ChooseStartingPoint()
    {
        startingPointPosition = new Vector2(Mathf.Floor(Random.Range(0, gridSize.x)), Mathf.Floor(Random.Range(0, gridSize.y)));
    }

    // Create the grid with room prefabs
    private void CreateGrid()
    {
        roomList = new List<List<DungeonRoom>>();

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
            if (IsPositionInGrid(nextPosition) && roomList[Mathf.FloorToInt(nextPosition.x)][Mathf.FloorToInt(nextPosition.y)] == null)
            {
                // Place the room
                roomList[Mathf.FloorToInt(nextPosition.x)][Mathf.FloorToInt(nextPosition.y)] = roomPrefabs[0];
                roomLeft--; // Decrement room count

                // Recursively try to create more rooms from the new position
                RecursiveRoomCreation(roomLeft-1, nextPosition);
            }
        }
    }

    // Check if the position is within bounds
    private bool IsPositionInGrid(Vector2 position)
    {
        return position.x >= 0 && position.x < gridSize.x && position.y >= 0 && position.y < gridSize.y;
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
