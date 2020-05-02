using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject target;

    private Vector3 startPosition;
    private Vector3 directionToCamera;

    private void Awake()
    {
        startPosition = transform.position;
        directionToCamera = transform.position - target.transform.position;
    }

    private void FixedUpdate()
    {
        FollowingDistance();
    }


    private void Following()
    {
        Vector3 newPosition = target.transform.position;
        newPosition.y = startPosition.y;
        transform.position = target.transform.position + directionToCamera;
    }

    private void FollowingDistance()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        float maxDistance = 10;
        if(distance > maxDistance)
        {
            transform.position = target.transform.position + directionToCamera;
        }
        else
        {
            Vector3 direction = target.transform.position + directionToCamera - transform.position;

            transform.Translate(direction * Time.deltaTime, Space.World);
        }

    }

}
