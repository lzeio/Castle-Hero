using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;

        
    }

   float rotY;
   public float sens;
    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis("Mouse X");
        rotY += y * sens * Time.deltaTime;

        Quaternion localrot = Quaternion.Euler(0, rotY, 0);
        transform.rotation = localrot;  

    }
}
