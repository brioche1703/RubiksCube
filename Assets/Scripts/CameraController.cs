using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    float dragSpeed = 20.0f;
    float keyboatMovementSpeed = 10.0f;
    float rotationSpeed = 1750.0f;
    float zoomingSpeed = 10.0f;

    private Vector3 mouseOrigin;
    private bool isDraging = false;
    private bool isRotating = false;

    // Rotation of the camera around the center of the rubiks cube
    private Vector3 rubiksCubeCenter = new Vector3(0.0f, 0.0f, 0.0f);

    void Start() {
        rubiksCubeCenter = PersistentManager.Instance.rubiksCubeCenter;
    }

    void Update()
    {
        rubiksCubeCenter = PersistentManager.Instance.rubiksCubeCenter;
        ProcessInputs();
    }

    private void ProcessInputs() {
        ProcessKeyboardInputs();
        RotateAroundCubeCenter();
        ZoomCamera();
    }

    private void ResetCameraPosition() {
        
        transform.position = new Vector3(rubiksCubeCenter.x, rubiksCubeCenter.y, -5.0f);
        transform.rotation = Quaternion.identity;
    }

    private void RotateAroundCubeCenter() {
        if (Input.GetMouseButtonDown(1)) {
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        if (!Input.GetMouseButton(1))
            isRotating = false;

        if (isRotating) {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            mouseOrigin = Input.mousePosition;
            transform.RotateAround(rubiksCubeCenter, transform.right, -pos.y * rotationSpeed * Time.deltaTime);
            transform.RotateAround(rubiksCubeCenter, Vector3.up, pos.x * rotationSpeed * Time.deltaTime);
        }
    }

    private void ZoomCamera() {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            transform.Translate(0, 0, -zoomingSpeed * Time.deltaTime);
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            transform.Translate(0, 0, zoomingSpeed * Time.deltaTime);
    }

    private void ProcessKeyboardInputs() {
        if (Input.GetKey(KeyCode.R))
            ResetCameraPosition();

        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(new Vector3(0, keyboatMovementSpeed * Time.deltaTime, 0)); 
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(0, -keyboatMovementSpeed * Time.deltaTime, 0); 
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(keyboatMovementSpeed * Time.deltaTime, 0, 0); 
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(-keyboatMovementSpeed * Time.deltaTime, 0, 0); 
        }
    }

}
