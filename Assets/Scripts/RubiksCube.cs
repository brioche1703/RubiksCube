using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubiksCube : MonoBehaviour
{


    public CubeController cubePrefab;

    private int size;
    private float center;
    private float width = 1.0f;

    private float rotationSpeed = 50.0f;
    private List<CubeController> cubeList;

    void Start()
    {
        size = 3;
        center = (size / 2) * width;
        PersistentManager.Instance.rubiksCubeSize = size;
        PersistentManager.Instance.rubiksCubeCenter = new Vector3(center, center, center);

        cubeList = new List<CubeController>();

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                for (int k = 0; k < size; k++) {
                    cubeList.Add(Instantiate(cubePrefab, cubePrefab.transform.position + new Vector3(i, j, k), Quaternion.identity) as CubeController);
                }
            }
        }
    }

    void Update() {
        //RotateRow();
    }

    public void RotateRow(Vector3 pos) {
        Vector3 axeRotation = GetRotationAxe(pos);

        for (int i = 0; i < 25; i = i + 3) {
            //cubeList[i].transform.RotateAround(new Vector3(center, center, center), new Vector3(0.0f, 0.0f, 1.0f), Time.deltaTime * rotationSpeed);
            cubeList[i].transform.RotateAround(new Vector3(center, center, center), axeRotation, Time.deltaTime * rotationSpeed);
        }
    }

    Vector3 GetRotationAxe(Vector3 mouseMovementVector) {
        float maxValue = 0.0f;
        float minValue = 0.0f;
        int rotationAxeMaxIndex = 0;
        int rotationAxeMinIndex = 0;
        int rotationAxeIndex = 0;
        for (int i = 0; i < 3; i++) {
            if (mouseMovementVector[i] > maxValue) {
                maxValue = mouseMovementVector[i];
                rotationAxeMaxIndex = i;
            }
            if (mouseMovementVector[i] < minValue) {
                minValue = mouseMovementVector[i];
                rotationAxeMinIndex = i;
            }
        }

        if (-minValue > maxValue) {
            maxValue = minValue;
            rotationAxeIndex = rotationAxeMinIndex;
        }
        else {
            rotationAxeIndex = rotationAxeMaxIndex;
        }

        switch (rotationAxeIndex) {
            case 0:
                return new Vector3(0.0f, -maxValue, 0.0f); 

            case 1:
                return new Vector3(maxValue, 0.0f, 0.0f); 

            case 2:
                return new Vector3(0.0f, 0.0f, maxValue); 
        }
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

}
