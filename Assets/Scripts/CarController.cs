using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CarController : NetworkBehaviour
{
    private float horInput;
    private float verInput;
    private float steeringAngle;

    public WheelCollider flW, frW, rlW, rrW;
    public Transform flT, frT, rlT, rrT;    // for each wheel
    public float maxSteerAngle = 30;
    public float motorForce = 40;
    public float maxSpeed = 20;
    public Vector3 centerMass;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass -= centerMass;
    }

    [Client]
    public void FixedUpdate()
    {
        if(!hasAuthority) { return; }
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    public void GetInput()
    {
        horInput = Input.GetAxis("Horizontal");
        verInput = Input.GetAxis("Vertical");
    }

    public void Steer()
    {
        steeringAngle = maxSteerAngle * horInput;
        flW.steerAngle = steeringAngle;
        frW.steerAngle = steeringAngle;
    }

    public void Accelerate()
    {
        if(rb.velocity.magnitude < maxSpeed)
        {
            //flW.motorTorque = verInput * motorForce;
            //frW.motorTorque = verInput * motorForce;
            rlW.motorTorque = verInput * motorForce;
            rrW.motorTorque = verInput * motorForce;
        }
    }

    public void UpdateWheelPoses()
    {
        UpdateWheelPose(flW, flT);
        UpdateWheelPose(frW, frT);
        //UpdateWheelPose(rlW, rlT);
        //UpdateWheelPose(rrW, rrT);
    }

    public void UpdateWheelPose(WheelCollider wc, Transform t)
    {
        Vector3 pos = t.position;
        Quaternion q = t.rotation;

        wc.GetWorldPose(out pos, out q);
        t.position = pos;
        t.rotation = q;
    }
}
