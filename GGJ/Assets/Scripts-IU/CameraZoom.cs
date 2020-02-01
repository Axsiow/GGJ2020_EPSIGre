using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public float zoomSpeed = 100f;
    public float zoomTime = 0.1f;

    public float maxHeight = 100f;
    public float minHeight = 20f;

    public float focusHeight = 10f;
    public float focusDistance = 20f;

    public int panBorder = 25;
    public float dragPanSpeed = 25f;
    public float edgePanSpeed = 25f;
    public float keyPanSpeed = 25f;

    private float zoomVelocity = 0f;
    private float targetHeight;

    void Start () {
        // Start zoomed out
        targetHeight = maxHeight;
    }

    void Update () {
        var newPosition = transform.position;

        // First, calculate the height we want the camera to be at
        targetHeight += Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed * -1f;
        targetHeight = Mathf.Clamp (targetHeight, minHeight, maxHeight);

        // Then, interpolate smoothly towards that height
        newPosition.y = Mathf.SmoothDamp (transform.position.y, targetHeight, ref zoomVelocity, zoomTime);

        // Always pan the camera using the keys
        var pan = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")) * keyPanSpeed * Time.deltaTime;

        // Optionally pan the camera by either dragging with middle mouse or when the cursor touches the screen border
        if (Input.GetMouseButton (2)) {
            pan -= new Vector2 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y")) * dragPanSpeed * Time.deltaTime;
        }
        else {
            var border = Vector2.zero;
            if (Input.mousePosition.x < panBorder) border.x -= 1f;
            if (Input.mousePosition.x >= Screen.width - panBorder) border.x += 1f;
            if (Input.mousePosition.y < panBorder) border.y -= 1f;
            if (Input.mousePosition.y > Screen.height - panBorder) border.y += 1f;
            pan += border * edgePanSpeed * Time.deltaTime;
        }

        newPosition.x += pan.x;
        newPosition.z += pan.y;

        var focusPosition = new Vector3 (newPosition.x, focusHeight, newPosition.z + focusDistance);

        transform.position = newPosition;
        transform.LookAt (focusPosition);
    }
}
