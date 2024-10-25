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

    void Start()
    {
        transform.rotation = Quaternion.identity;
    }
}
