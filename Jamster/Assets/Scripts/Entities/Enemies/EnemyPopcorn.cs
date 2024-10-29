using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyPopcorn : Enemy
{
    [SerializeField] private Transform PlayerPosition = default;
    [SerializeField] private Collider _BoxColl;
    [SerializeField] private Collider _SphereColl;
    private bool followPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        followPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWalls();
        if (followPlayer) Mvmnt();
        print(life);
    }


    private void Mvmnt()
    {  
        Vector3 lDir = (_PlayerTransform.transform.position - transform.position).normalized;        
        transform.position += (lDir *_MaxSpeed) * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        followPlayer = true;            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            followPlayer = false;
        }
    }    
}
