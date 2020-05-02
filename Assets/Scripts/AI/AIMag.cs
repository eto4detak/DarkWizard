using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMag : MonoBehaviour
{
    private Unit owner;
    private float currentTime;
    private float turnTime = 1f;
    private Vector3 moveDirection;

    private void Awake()
    {
        owner = GetComponent<Unit>();
        currentTime = turnTime;
        moveDirection = Vector3.forward;
    }

    private void FixedUpdate()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.fixedDeltaTime;
        }
        else
        {
            currentTime = turnTime;
            moveDirection *= -1;
        }
        owner.Move(moveDirection);
    }


}
