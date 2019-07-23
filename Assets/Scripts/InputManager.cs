using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public RubiksCube rubiksCube;    

    private Vector3 mouseOrigin;
    private bool isDraging =  false;

    // Mouse click to select a face
    Ray ray;
    RaycastHit hit;
    CubeController cubeParent;
    private bool isRotating = false;
    float rotationSpeed = 1750.0f;

    void Start() {
    }

    void Update() {
        ClickOnCubeFace(); 
    }

    /*void ProcessInputMouseButton0() {

        if (Input.GetMouseButtonDown(0)) {
            mouseOrigin = Input.mousePosition;
            isDraging = true; 
        }
        if (!Input.GetMouseButton(0))
            isDraging = false;

        if (isDraging) {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            rubiksCube.RotateRow(pos);
        }
    } */

    void ClickOnCubeFace() {
        if (Input.GetMouseButtonDown(0)) {
            mouseOrigin = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(mouseOrigin);
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.transform.parent != null) {
                    cubeParent = hit.collider.transform.parent.gameObject.GetComponent<CubeController>(); 
                    if (cubeParent != null) {
                        isRotating = true; 
                    }
                }
            }

        }
        
        if (!Input.GetMouseButton(0))
            isRotating = false;

        if (isRotating) {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            cubeParent.transform.RotateAround(cubeParent.transform.position, cubeParent.transform.right, -pos.y * rotationSpeed * Time.deltaTime);
        }
    }
}
