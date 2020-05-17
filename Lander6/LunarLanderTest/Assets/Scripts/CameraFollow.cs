using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0f, 0f, 5f);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerTransform.position - offset;   
    }
}
