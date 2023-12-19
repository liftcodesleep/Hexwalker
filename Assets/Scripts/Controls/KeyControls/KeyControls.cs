using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControls : MonoBehaviour
{
    private Vector3 oldPosition;
    public static float speed = 100;
    [SerializeField] GameObject HexMap;
    [SerializeField] Camera otherCamera;
    private Vector3 lastMousePosition;
    public float rotationSpeed = 10.0f;
    // private float northBound = -20f;
    // private float southhBound = 10f;
    private bool isShifting;
    private Vector3 shiftingTarget;
    private Vector3 shiftingTargetz;
    public static float shiftingSpeed = 3f;
    public static float zoomSpeed = 10;
    public float minZoom = 10.0f;
    public float maxZoom = 100.0f;

    [SerializeField] GameObject ExcapeMenu;

    void Start() {
        oldPosition = this.transform.position;
        lastMousePosition = Input.mousePosition;
        isShifting = false;
    }

    void Update() {
        CheckIfCameraMoved();

        if (isShifting) {
            ShiftCamera();
        }
        else
        {
            CamaraControls();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TogglePauseStack();
        }

        if (Input.GetKeyUp(KeyCode.Escape)){
            ExcapeMenu.SetActive(!ExcapeMenu.active);
        }

    }

    private void CamaraControls() {
        float xChange = Input.GetAxis("Horizontal");
        float yChange = Input.GetAxis("Vertical");

        Vector3 change_heriz = new Vector3(xChange, 0, 0);
        this.transform.position += change_heriz * Time.deltaTime * speed;

        Vector3 change_vert = new Vector3(0, 0, yChange);

        // Change so that we move the camera and not the map
        //Vector3 new_vert = HexMap.transform.position + change_vert * Time.deltaTime * speed;
        this.transform.position += change_vert * Time.deltaTime * speed;
        /*
        if (new_vert.z < southhBound && new_vert.z > northBound) {
            HexMap.transform.position = new_vert;
        }
        */


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if(scroll < 0 || Input.GetKey(KeyCode.Q) ) {
            
            Camera.main.fieldOfView += zoomSpeed;
            otherCamera.fieldOfView += zoomSpeed;
        }
        else if(scroll > 0 || Input.GetKey(KeyCode.E)) {
            Camera.main.fieldOfView -= zoomSpeed;
            otherCamera.fieldOfView -= zoomSpeed;
        }
        

    }
    private void CheckIfCameraMoved() {
        if (this.oldPosition != this.transform.position) {
            oldPosition = this.transform.position;

            HexComponent[] hexes = GameObject.FindObjectsOfType<HexComponent>();

            foreach (HexComponent hex in hexes) {
                hex.UpdatePosition();
            }
        }
    }

    private void ShiftCamera() {

        Debug.Log(KeyControls.shiftingSpeed);
        Vector3 newPosition = Vector3.Lerp(Camera.main.transform.position, shiftingTarget, shiftingSpeed * Time.deltaTime);
        transform.position = newPosition;

        Vector3 newPositionz = Vector3.Lerp(HexMap.transform.position, shiftingTargetz, shiftingSpeed * Time.deltaTime);
        HexMap.transform.position = newPositionz;

        if (Vector3.Distance(newPosition, shiftingTarget) < .2f && Vector3.Distance(newPositionz, shiftingTargetz) < .2f) {
            isShifting = false;
        }
    }

    public void MoveCameraTo(GameObject target, int height) {

        shiftingTarget = new Vector3(target.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        shiftingTargetz = new Vector3(HexMap.transform.position.x, HexMap.transform.position.y, -(height - 2));
        isShifting = true;
    }

    public void TogglePauseStack()
    {
        Game.stack.isPaused = !Game.stack.isPaused;
    }
}
