using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera;
    private Vector3 dragOrigin;

    // Update is called once per frame
    void Update()
    {
        PanCamera();   
    }

    private void PanCamera()
    {
        //We save where we pressed down
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Camera.ScreenToWorldPoint(Input.mousePosition);
        }

        //Adding the difference created by the drag to the camera position
        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - Camera.ScreenToWorldPoint(Input.mousePosition);
            Camera.transform.position += difference;
        }
    }
}
