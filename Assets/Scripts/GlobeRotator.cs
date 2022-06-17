using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class GlobeRotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    public Camera cam;
    public bool isDragging;

    Rigidbody rb;

    float x, y;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
           
            
        }

       
    }

    private void OnMouseDrag()
    {

        x = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        
        transform.RotateAround(transform.position,Vector3.down, x);
        transform.RotateAround(transform.position, Vector3.right, y);

        
    }


      
    //private void FixedUpdate()
    //{

    //    if(isDragging)
    //    {
    //        float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
    //        float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;    

    //        rb.AddTorque(Vector3.down * x);
    //        rb.AddTorque(Vector3.right * y);
    //    }
    //}
}
