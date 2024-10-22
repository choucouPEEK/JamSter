using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] Vector2 dungeonSize;
    [SerializeField] int dungeonRoomOffset;

    [SerializeField] int dungeonRoomNumber = 5;

    [SerializeField] Vector2 dungeonRoomMinSize;

    void Start()
    {
        Debug.Log(DungeonRooms());
    }

    private List<Rect> DungeonRooms()
    {
        List<Rect> allRoomsRect = new List<Rect>();
        allRoomsRect.Add(new Rect(0f,0f,dungeonSize.x,dungeonSize.y));

        for (int i = 0; i < dungeonRoomNumber-1; i++)
        {
            Rect biggestRoom = allRoomsRect[BiggestRectInList(allRoomsRect)];
            allRoomsRect.Remove(biggestRoom);

            if (i%2==0)
            {
                float randomSlice = Random.Range(0+dungeonRoomMinSize.x,biggestRoom.size.x-dungeonRoomMinSize.x);

                allRoomsRect.Add(new Rect(biggestRoom.position.x, biggestRoom.position.y,
                                          randomSlice, biggestRoom.size.y));
                allRoomsRect.Add(new Rect(biggestRoom.position.x + randomSlice, biggestRoom.position.y, 
                                          biggestRoom.size.x- randomSlice ,biggestRoom.size.y));
            }
            else
            {
                float randomSlice = Random.Range(0 + dungeonRoomMinSize.y, biggestRoom.size.y - dungeonRoomMinSize.y);

                allRoomsRect.Add(new Rect(biggestRoom.position.x, biggestRoom.position.y, 
                                          biggestRoom.size.x, randomSlice));
                allRoomsRect.Add(new Rect(biggestRoom.position.x, biggestRoom.position.y + randomSlice, 
                                          biggestRoom.size.x, biggestRoom.size.y - randomSlice));
            }
        }
        
        return allRoomsRect;
    }

    private int BiggestRectInList(List<Rect> rects)
    {
        if (rects == null || rects.Count == 0)
        {
            throw new ArgumentException("The list of rectangles cannot be null or empty.");
        }

        Rect biggestRect = rects[0];
        int occurrence = 1;
        float biggestArea = biggestRect.width * biggestRect.height;

        for (int i = 1; i < rects.Count; i++)
        {
            var rect = rects[i];
            float area = rect.width * rect.height;

            if (area > biggestArea)
            {
                biggestRect = rect;
                biggestArea = area;
                occurrence = 1;
            }
            else if (area == biggestArea)
            {
                occurrence++;
            }
        }
        return occurrence;
    }
}
