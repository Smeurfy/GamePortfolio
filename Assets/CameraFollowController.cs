using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField]
    private Transform objToFollow;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float lookSpeed = 10;
    [SerializeField]
    private float followSpeed = 10;


    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 targetPos = objToFollow.position +
                            objToFollow.forward * offset.z +
                            objToFollow.right * offset.x +
                            objToFollow.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

    private void LookAtTarget()
    {
        Vector3 lookDirection = objToFollow.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookDirection, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime);
    }
}
