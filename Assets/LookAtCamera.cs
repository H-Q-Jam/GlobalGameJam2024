using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera camera;

    public Transform objectToRotate;

    public float speedRotation = 10f;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindAnyObjectByType<Camera>();   
    }

    // Update is called once per frame
    void Update()
    {
        objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, camera.transform.rotation, speedRotation * Time.deltaTime);
    }
}
