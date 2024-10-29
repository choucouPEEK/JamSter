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
    private Vector3 _CurrentSpeed = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //CheckForWalls();
        //Movements();
    }

    private void FixedUpdate()
    {
        Movements();
    }

    private void Movements()
    {
        Vector3 lInput = new Vector3(Input.GetAxis(_HorizontalAxis), 0, Input.GetAxis(_VerticalAxis));
        lInput = Vector3.ClampMagnitude(lInput, 1);
        _CurrentSpeed = transform.TransformDirection(lInput) * (_MaxSpeed );
        CheckForWalls() ;
        transform.position += _CurrentSpeed * Time.fixedDeltaTime;
    }

    private void CheckForWalls()
    {
        Vector3 myPosition = transform.position + new Vector3(0, -0.2f, 0);
        Vector3 rayDirection = transform.forward ;
        RaycastHit hitInfo;
        //forward:
        if (Physics.Raycast(myPosition, rayDirection, out hitInfo, _PlayerWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls)
            {
                _CurrentSpeed -= transform.forward * (_CurrentSpeed.z * 2 * Time.fixedDeltaTime);
            }
        }
        Debug.DrawRay(myPosition, rayDirection* _PlayerWidth, Color.green);
        //backward
        if (Physics.Raycast(myPosition, -rayDirection, out hitInfo, _PlayerWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls)
            {
                _CurrentSpeed += transform.forward * (_CurrentSpeed.z * 2 * Time.fixedDeltaTime);
            }
        }
        Debug.DrawRay(myPosition, -rayDirection * _PlayerWidth, Color.green);
        rayDirection = transform.right;
        //right
        if (Physics.Raycast(myPosition, rayDirection, out hitInfo, _PlayerWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls)
            {
                _CurrentSpeed -= transform.right * (_CurrentSpeed.x * 2 * Time.fixedDeltaTime);
            }
        }
        Debug.DrawRay(myPosition, rayDirection * _PlayerWidth, Color.green);
        //left
        if (Physics.Raycast(myPosition, -rayDirection, out hitInfo, _PlayerWidth))
        {
            if (hitInfo.collider.gameObject.tag == _TagWalls)
            {
                _CurrentSpeed += transform.right * (_CurrentSpeed.x *2 * Time.fixedDeltaTime);
            }
        }
        Debug.DrawRay(myPosition, -rayDirection * _PlayerWidth, Color.green);
    }
}
