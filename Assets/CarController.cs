using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public Transform leftWheelT;
        public Transform rightWheelT;
        public bool motor; // is this wheel attached to motor?
        public bool steering; // does this wheel apply steer angle?
    }


    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
                                   // finds the corresponding visual wheel
                                   // correctly applies the transform
    
    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            UpdateWheelPosition(axleInfo.leftWheel, axleInfo.leftWheelT);
            UpdateWheelPosition(axleInfo.rightWheel, axleInfo.rightWheelT);
        }
    }

    public void UpdateWheelPosition(WheelCollider collider, Transform transform)
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        collider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;
    }
}
