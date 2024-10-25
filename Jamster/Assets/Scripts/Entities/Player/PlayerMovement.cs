using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _PlayerWidth = 1; //used for raycast to the walls
    [SerializeField] private string _TagWalls;

    //Movements
    [SerializeField] private float _MaxSpeed = 20;
    [SerializeField] private string _HorizontalAxis = "Horizontal";
    [SerializeField] private string _VerticalAxis = "Vertical";


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForWalls();
        Movements();
    }

    private void Movements()
    {
        Vector3 lInput = new Vector3(Input.GetAxis(_HorizontalAxis), 0, Input.GetAxis(_VerticalAxis));
        lInput = Vector3.ClampMagnitude(lInput, 1);
        transform.position += transform.TransformDirection(lInput) * (_MaxSpeed * Time.deltaTime);
    }

    private void CheckForWalls()
    {
        Vector3 myPosition = transform.position;
        Vector3 rayDirection = transform.forward;
        RaycastHit hitInfo;
        //forward:
        if (Physics.Raycast(myPosition, rayDirection, out hitInfo, _PlayerWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls.ToString())
            {
                transform.position -= transform.forward * (_MaxSpeed * Time.deltaTime);
            }
        }
        //backward
        if (Physics.Raycast(myPosition, -rayDirection, out hitInfo, _PlayerWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls.ToString())
            {
                transform.position += transform.forward * (_MaxSpeed * Time.deltaTime);
            }
        }
        rayDirection = transform.right;
        //right
        if (Physics.Raycast(myPosition, rayDirection, out hitInfo, _PlayerWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls.ToString())
            {
                transform.position -= transform.right * (_MaxSpeed * Time.deltaTime);
            }
        }
        //left
        if (Physics.Raycast(myPosition, -rayDirection, out hitInfo, _PlayerWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls.ToString())
            {
                transform.position += transform.right * (_MaxSpeed * Time.deltaTime);
            }
        }
    }
}
