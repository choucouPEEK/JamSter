using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] public List<bool> roomOpens = new List<bool>(4);

    void Start()
    {
        transform.rotation = Quaternion.identity;
    }
}
