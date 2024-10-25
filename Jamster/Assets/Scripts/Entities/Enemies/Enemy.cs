using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _EnemyWidth = 1; //used for raycast to the walls
    [SerializeField] protected string _TagWalls;
    protected const string PLAYER_TAG = "Player";

    [Header("Health")]
    [SerializeField] protected int life = 1;

    [Header("Movements")]
    [SerializeField] protected float _MaxSpeed = 20;

    [Header("Attack")]
    [SerializeField] protected float _AttackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (CompareTag(PLAYER_TAG))
        {
            LooseLife();
        }
    }
    protected void LooseLife()
    {
        life--;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
    protected void CheckForWalls()
    {
        Vector3 myPosition = transform.position;
        Vector3 rayDirection = transform.forward;
        RaycastHit hitInfo;
        //forward:
        if (Physics.Raycast(myPosition, rayDirection, out hitInfo, _EnemyWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls.ToString())
            {
                transform.position -= transform.forward * (_MaxSpeed * Time.deltaTime);
            }
        }
        //backward
        if (Physics.Raycast(myPosition, -rayDirection, out hitInfo, _EnemyWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls.ToString())
            {
                transform.position += transform.forward * (_MaxSpeed * Time.deltaTime);
            }
        }
        rayDirection = transform.right;
        //right
        if (Physics.Raycast(myPosition, rayDirection, out hitInfo, _EnemyWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls.ToString())
            {
                transform.position -= transform.right * (_MaxSpeed * Time.deltaTime);
            }
        }
        //left
        if (Physics.Raycast(myPosition, -rayDirection, out hitInfo, _EnemyWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls.ToString())
            {
                transform.position += transform.right * (_MaxSpeed * Time.deltaTime);
            }
        }
    }
}
