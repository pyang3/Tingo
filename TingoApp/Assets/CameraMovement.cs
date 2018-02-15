using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private static readonly float PanSpeed = 20f;
    private static readonly float zoomSpeedTouch = 0.1f;
    private static readonly float zoomSpeedMouse = 0.5f;

    private static readonly float[] BoundsX = new float[] { -10f, 5f };
    private static readonly float[] BoundsZ = new float[] { -18f, -4f };
    private static readonly float[] ZoomBounds = new float[] { 10f, 85f };
    
    private Camera cam;

    private Vector3 lastPanPosition;
    private int panFIngerId;

    private bool wasZoomingLastFrame;
    private Vector2[] lastZoomPositions;

    void Awake(){
        cam = GetComponent<Camera>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer){
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPanPosition = Input.mousePosition;
        }else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, zoomSpeedMouse);
    }

    void HandleTouch(){

        switch (Input.touchCount)
        {
            case 1: //One Finger
                wasZoomingLastFrame = false;

                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    lastPanPosition = touch.position;
                    panFIngerId = touch.fingerId;
                }
                else if(touch.fingerId == panFIngerId && touch.phase == TouchPhase.Moved)
                {
                    PanCamera(touch.position);
                }
                break;

            case 2: // Two Fingers
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame)
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else
                {
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, zoomSpeedTouch);

                    lastZoomPositions = newPositions;
                }
                break;

            default:

                wasZoomingLastFrame = false;
                break;
        }

    }

    void PanCamera(Vector3 newPosition){
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, 0, offset.y * PanSpeed);

        transform.Translate(move, Space.World);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);

        lastPanPosition = newPosition;
    }

    void ZoomCamera(float offset, float speed){
        if(offset == 0)
        {
            return;
        }

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }
}
