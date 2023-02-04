using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrapsSpawner : MonoBehaviour
{

    [SerializeField] GameObject cube;
    [SerializeField] Transform globe;
    Rigidbody rb;
  

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    GameObject temp = Instantiate(cube, hit.point, cube.transform.rotation, globe);
                }   
            }
        }
    }
}
