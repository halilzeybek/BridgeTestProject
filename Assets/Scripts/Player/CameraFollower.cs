using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    [SerializeField] private Transform targetObject;
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Vector3 offset;

    private Vector3 velocity = Vector3.zero;


    private void LateUpdate()
    {
        if (targetObject != null)
        {
            Vector3 targetPosition = targetObject.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        }

    }
}
