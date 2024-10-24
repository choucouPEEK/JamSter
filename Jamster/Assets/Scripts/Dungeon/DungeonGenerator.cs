using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private DungeonRoom startRoom;

    [SerializeField] public static List<DungeonRoom> roomPrefabs = new List<DungeonRoom>();
    [SerializeField] public static List<DungeonRoom> endRoomPrefab = new List<DungeonRoom>();

    public static List<Vector3> allRoomPosition = new List<Vector3>();
    void Start()
    {
        //RecursiveRoomGeneration(startRoom, 8);
    }

    private void RecursiveRoomGeneration(DungeonRoom lPreviousRoom, int lMaxRoomLeft) //Create Rooms with Recursivity, maxRoomLeft say how much room in any way can spawn
    {
        if (lMaxRoomLeft == 0)
        {
            DungeonRoom room;
            /*if (lPreviousRoom.isRoomOpenUp)
            {
                room = FindEndRoom(lPreviousRoom, lMaxRoomLeft, "up");
                PlaceRoom(lPreviousRoom, room, "up");
            }
            if (lPreviousRoom.isRoomOpenDown)
            {
                room = FindEndRoom(lPreviousRoom, lMaxRoomLeft, "down");
                PlaceRoom(lPreviousRoom, room, "down");
            }
            if (lPreviousRoom.isRoomOpenLeft)
            {
                room = FindEndRoom(lPreviousRoom, lMaxRoomLeft, "left");
                PlaceRoom(lPreviousRoom, room, "left");
            }
            if (lPreviousRoom.isRoomOpenRight)
            {
                room = FindEndRoom(lPreviousRoom, lMaxRoomLeft, "right");
                PlaceRoom(lPreviousRoom, room, "right");
            }*/
        }
        else if (lMaxRoomLeft > 0)
        {
            DungeonRoom room;
            if (lPreviousRoom.isRoomOpenUp)
            {
                room = FindRandomRoom(lPreviousRoom, lMaxRoomLeft, "up");
                PlaceRoom(lPreviousRoom, room, "up");
                allRoomPosition.Add(room.transform.position);
                RecursiveRoomGeneration(room, lMaxRoomLeft - 1);
            }
            if (lPreviousRoom.isRoomOpenDown)
            {
                room = FindRandomRoom(lPreviousRoom, lMaxRoomLeft, "down");
                PlaceRoom(lPreviousRoom, room, "down");
                allRoomPosition.Add(room.transform.position);
                RecursiveRoomGeneration(room, lMaxRoomLeft - 1);
            }
            if (lPreviousRoom.isRoomOpenLeft)
            {
                room = FindRandomRoom(lPreviousRoom, lMaxRoomLeft, "left");
                PlaceRoom(lPreviousRoom, room, "left");
                allRoomPosition.Add(room.transform.position);
                RecursiveRoomGeneration(room, lMaxRoomLeft - 1);
            }
            if (lPreviousRoom.isRoomOpenRight)
            {
                room = FindRandomRoom(lPreviousRoom, lMaxRoomLeft, "right");
                PlaceRoom(lPreviousRoom, room, "right");
                allRoomPosition.Add(room.transform.position);
                RecursiveRoomGeneration(room, lMaxRoomLeft - 1);
            }
        }
    }

    private DungeonRoom FindRandomRoom(DungeonRoom lPreviousRoom, int lMaxRoomLeft, string lDirection)
    {
        List<DungeonRoom> lPossibleRoom = new List<DungeonRoom>();
        DungeonRoom lSelectedRoom;
        Vector3 lPreviousPosition = lPreviousRoom.transform.position;
        for (int room = 0; room < roomPrefabs.Count; room++)
        {
            if (lDirection == "down" && roomPrefabs[room].isRoomOpenUp && IsPlaceFree(lPreviousPosition + new Vector3(0, 0, 10)))
                lPossibleRoom.Add(roomPrefabs[room]);

            if (lDirection == "up" && roomPrefabs[room].isRoomOpenDown && IsPlaceFree(lPreviousPosition + new Vector3(0, 0, -10)))
                lPossibleRoom.Add(roomPrefabs[room]);

            if (lDirection == "left" && roomPrefabs[room].isRoomOpenRight && IsPlaceFree(lPreviousPosition + new Vector3(10, 0, 0)))
                lPossibleRoom.Add(roomPrefabs[room]);

            if (lDirection == "right" && roomPrefabs[room].isRoomOpenLeft&& IsPlaceFree(lPreviousPosition + new Vector3(-10, 0, 0)))
                lPossibleRoom.Add(roomPrefabs[room]);
        }
        lSelectedRoom = lPossibleRoom[Random.Range(0, lPossibleRoom.Count-1)];
        return lSelectedRoom; 
    }
    /*private DungeonRoom FindEndRoom(DungeonRoom lPreviousRoom, int lMaxRoomLeft, string lDirection)
    {
        for (int room = 0; room < existingEndRoom.Count; room++)
        {
            if (lDirection == "down" && existingEndRoom[room].isRoomOpenUp)
                return existingEndRoom[room];

            if (lDirection == "up" && existingEndRoom[room].isRoomOpenDown)
                return existingEndRoom[room];

            if (lDirection == "left" && existingEndRoom[room].isRoomOpenRight)
                return existingEndRoom[room];

            if (lDirection == "right" && existingEndRoom[room].isRoomOpenLeft)
                return existingEndRoom[room];
        }
        return null;
    }*/

    private void PlaceRoom(DungeonRoom lPreviousRoom, DungeonRoom actualRoom, string lDirection)
    {
        Instantiate(actualRoom);
        Debug.Log(actualRoom.name);
        if(lDirection == "down")
            actualRoom.transform.position = lPreviousRoom.transform.position+new Vector3(0,0,-10);
        else if (lDirection == "up")
            actualRoom.transform.position = lPreviousRoom.transform.position + new Vector3(0, 0, 10);
        else if (lDirection == "left")
            actualRoom.transform.position = lPreviousRoom.transform.position + new Vector3(-10, 0, 0);
        else if (lDirection == "right")
            actualRoom.transform.position = lPreviousRoom.transform.position + new Vector3(10, 0, 0);
    }

    private bool IsPlaceFree(Vector3 lPosition)
    {
        for (int i = 0; i < allRoomPosition.Count; i++)
        {
            if (allRoomPosition[i] == lPosition) return false;
        }
        return true;
    }
}