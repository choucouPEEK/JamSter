using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopcorn : Enemy
{
    [SerializeField] Transform PlayerPosition = default;
    // Start is called before the first frame update
    void Start()
    {
        _MaxSpeed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWalls();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CompareTag(PLAYER_TAG))
        {
            transform.position += PlayerPosition.position * _MaxSpeed * Time.deltaTime;
        }
    }

    
}
