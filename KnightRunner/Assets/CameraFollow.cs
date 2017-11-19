using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float smoothing;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPosition, smoothing * Time.deltaTime);


    }
}
