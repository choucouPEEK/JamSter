using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
        Movements();
    }

    private void Movements()
    {
        Vector3 lInput = new Vector3(Input.GetAxis(_HorizontalAxis), 0, Input.GetAxis(_VerticalAxis));
        lInput = Vector3.ClampMagnitude(lInput, 1);
        transform.position += transform.TransformDirection(lInput) * (_MaxSpeed * Time.deltaTime);
    }
}
