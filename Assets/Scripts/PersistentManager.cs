using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{

    public Vector3 rubiksCubeCenter = new Vector3(0.0f, 0.0f, 0.0f);
    public int rubiksCubeSize = 3;

    public static PersistentManager Instance { get; private set; }

    public void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

}
