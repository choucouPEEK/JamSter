using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] public int roomSize = 20;
    [SerializeField] public bool isRoomOpenUp;
    [SerializeField] public bool isRoomOpenDown;
    [SerializeField] public bool isRoomOpenLeft;
    [SerializeField] public bool isRoomOpenRight;

    [SerializeField] public int RoomRemaning;

    [SerializeField] public List<DungeonRoom> roomPrefabs = new List<DungeonRoom>();
    [SerializeField] public  List<DungeonRoom> endRoomPrefab = new List<DungeonRoom>();


    void Start()
    {
        transform.rotation = Quaternion.identity;
        if (RoomRemaning==1) CreateEndRooms();
        if (RoomRemaning>1) CreateRooms();
    }

    private void CreateRooms()
    {
        DungeonRoom room;
        List<DungeonRoom> lPotentialRooms = new List<DungeonRoom>();

        if (isRoomOpenUp)
        {
            for (int i = 0; i < roomPrefabs.Count; i++)
            {
                if (roomPrefabs[i].isRoomOpenDown) lPotentialRooms.Add(roomPrefabs[i]);
            }
            CheckRoomEntry(lPotentialRooms);
            room = lPotentialRooms[Random.Range(0, lPotentialRooms.Count-1)];
            Instantiate(room);
            room.RoomRemaning = RoomRemaning - 1;
            room.transform.position = transform.position + new Vector3(0,0,roomSize);

            room.roomPrefabs = roomPrefabs;
            room.endRoomPrefab = endRoomPrefab;

        }
        if (isRoomOpenDown)
        {
            for (int i = 0; i < roomPrefabs.Count; i++)
            {
                if (roomPrefabs[i].isRoomOpenUp) lPotentialRooms.Add(roomPrefabs[i]);
            }
            CheckRoomEntry(lPotentialRooms);
            room = lPotentialRooms[Random.Range(0, lPotentialRooms.Count-1)];
            Instantiate(room);
            room.RoomRemaning = RoomRemaning - 1;
            room.transform.position = transform.position + new Vector3(0, 0, -roomSize);

            room.roomPrefabs = roomPrefabs;
            room.endRoomPrefab = endRoomPrefab;
        }
        if (isRoomOpenLeft)
        {
            for (int i = 0; i < roomPrefabs.Count; i++)
            {
                if (roomPrefabs[i].isRoomOpenRight) lPotentialRooms.Add(roomPrefabs[i]);
            }
            CheckRoomEntry(lPotentialRooms);
            room = lPotentialRooms[Random.Range(0, lPotentialRooms.Count-1)];
            Instantiate(room);
            room.RoomRemaning = RoomRemaning - 1;
            room.transform.position = transform.position + new Vector3(-roomSize, 0, 0);
            room.roomPrefabs = roomPrefabs;
            room.endRoomPrefab = endRoomPrefab;
        }
        if (isRoomOpenRight)
        {
            for (int i = 0; i < roomPrefabs.Count; i++)
            {
                if (roomPrefabs[i].isRoomOpenLeft) lPotentialRooms.Add(roomPrefabs[i]);
            }
            CheckRoomEntry(lPotentialRooms);
            room = lPotentialRooms[Random.Range(0, lPotentialRooms.Count-1)];
            Instantiate(room);
            room.RoomRemaning = RoomRemaning - 1;
            room.transform.position = transform.position + new Vector3(roomSize, 0, 0);
            room.roomPrefabs = roomPrefabs;
            room.endRoomPrefab = endRoomPrefab;
        }
    }
    private void CreateEndRooms()
    {
        DungeonRoom room = new DungeonRoom();
        if (isRoomOpenUp)
        {
            room = endRoomPrefab[0];
            Instantiate(room);

            room.RoomRemaning = RoomRemaning - 1;
            room.transform.position = transform.position + new Vector3(0, 0, roomSize);
            room.roomPrefabs = roomPrefabs;
            room.endRoomPrefab = endRoomPrefab;
        }
        if (isRoomOpenDown)
        {
            room = endRoomPrefab[3];
            Instantiate(room);

            room.RoomRemaning = RoomRemaning - 1;
            room.transform.position = transform.position + new Vector3(0, 0, -roomSize);
            room.roomPrefabs = roomPrefabs;
            room.endRoomPrefab = endRoomPrefab;
        }
        if (isRoomOpenLeft)
        {
            room = endRoomPrefab[2];
            Instantiate(room);
            room.RoomRemaning = RoomRemaning - 1;
            room.transform.position = transform.position + new Vector3(-roomSize, 0, 0);
            room.roomPrefabs = roomPrefabs;
            room.endRoomPrefab = endRoomPrefab;
        }
        if (isRoomOpenRight)
        {
            room = endRoomPrefab[1];
            Instantiate(room);
            room.RoomRemaning = RoomRemaning - 1;
            room.transform.position = transform.position + new Vector3(roomSize, 0, 0);
            room.roomPrefabs = roomPrefabs;
            room.endRoomPrefab = endRoomPrefab;
        }
    }

    private void CheckRoomEntry(List<DungeonRoom> lPotentialRooms)
    {
        for (int i = 0;i < lPotentialRooms.Count;i++)
        {
            if (lPotentialRooms[i].isRoomOpenUp && !IsPlaceFree(lPotentialRooms[i].transform.position + new Vector3(0, 0, roomSize)))
            {
                if (transform.position == lPotentialRooms[i].transform.position + new Vector3(0, 0, -roomSize))
                {
                    continue;
                }
                else
                lPotentialRooms.Remove(lPotentialRooms[i]);
            }
            if (lPotentialRooms[i].isRoomOpenDown && !IsPlaceFree(lPotentialRooms[i].transform.position + new Vector3(0, 0, -roomSize)))
            {
                if (transform.position == lPotentialRooms[i].transform.position + new Vector3(0, 0, roomSize))
                {
                    continue;
                }
                else
                    lPotentialRooms.Remove(lPotentialRooms[i]);
            }
            if (lPotentialRooms[i].isRoomOpenLeft && !IsPlaceFree(lPotentialRooms[i].transform.position + new Vector3(-roomSize, 0, 0)))
            {
                if (transform.position == lPotentialRooms[i].transform.position + new Vector3(roomSize, 0, 0))
                {
                    continue;
                }
                else
                    lPotentialRooms.Remove(lPotentialRooms[i]);
            }
            if (lPotentialRooms[i].isRoomOpenRight && !IsPlaceFree(lPotentialRooms[i].transform.position + new Vector3(roomSize, 0, 0)))
            {
                if (transform.position == lPotentialRooms[i].transform.position + new Vector3(-roomSize, 0, 0))
                {
                    continue;
                }
                else
                    lPotentialRooms.Remove(lPotentialRooms[i]);
            }
        }
    }
    private bool IsPlaceFree(Vector3 lPosition)
    {
        for (int i = 0; i < DungeonGenerator.allRoomPosition.Count; i++)
        {
            if (DungeonGenerator.allRoomPosition[i] == lPosition) return false;
        }
        return true;
    }
}
