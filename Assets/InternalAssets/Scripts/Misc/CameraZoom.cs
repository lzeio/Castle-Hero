using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    
    public float zoomSpeed;
    public float dragSpeed;
    [Header("Min Values")]
    public float minZoomX;
    public float minZoomY;
    public float minZoomZ;


    [Header("Max Values")]
    public float maxZoomX;
    public float maxZoomY;
    public float maxZoomZ;


    void Update()
    {
        float zoomAmount = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x - zoomAmount, maxZoomX, minZoomX);
        pos.y = Mathf.Clamp(pos.y - zoomAmount, maxZoomY, minZoomY);
        if(Input.GetMouseButton(1))
        {
            float dragAmount = Input.GetAxis("Mouse X")*dragSpeed;
            pos.z = Mathf.Clamp(pos.z - dragAmount, maxZoomZ, minZoomZ);

        }
        transform.position = pos;
    }


    private void OnMouseDrag()
    {
        Debug.Log("Called");
    }
}
