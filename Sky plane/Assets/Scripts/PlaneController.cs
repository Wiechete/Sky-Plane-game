using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float planePower;
    [SerializeField] private float planeMaxSpeed;
    [SerializeField] private float planeRotationSpeed;
    [SerializeField] private float planeNaturalRotation;
    [SerializeField] private AnimationCurve planeUpForceBySpeed;
    [SerializeField] private float planeUpForceMult;


    [SerializeField] private float planeFuel;
    [SerializeField] private float planeMaxFuel;

    [SerializeField] private float planeMaxZRotation;
    [SerializeField] private float planeMinZRotation;

    private Rigidbody2D rb;

    float previuosSpeed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = -Input.GetAxis("Vertical");

        Vector3 accelerationForce = transform.right * horizontal * planePower;

        if (horizontal < 0) accelerationForce *= 0.3f;

        
        float planeSpeedVertical = Vector3.Dot(rb.velocity, transform.right);
        float upForce = planeUpForceBySpeed.Evaluate(planeSpeedVertical / planeMaxSpeed) * planeUpForceMult;
        float planeRotationZ = GetPlaneRotation();

        if (planeSpeedVertical > planeMaxSpeed && horizontal > 0) accelerationForce = Vector3.zero;
        if (planeSpeedVertical < 0 && horizontal < 0) accelerationForce = Vector3.zero;

        //Debug.Log(planeSpeed + "   " + upForce);
        //Debug.Log(vertical + "   " + torque);
        if (upForce > 0 && planeRotationZ < 0) upForce = 0;
        rb.AddForce(accelerationForce + transform.up * upForce);

        float planeSpeed = rb.velocity.magnitude;
        float planeNaturalTorque = (planeSpeed - previuosSpeed) * planeNaturalRotation;
        Debug.Log(planeNaturalTorque);
        if (planeNaturalTorque < 0) planeNaturalTorque *= 1.5f;

        rb.AddTorque(GetTorque(vertical, planeRotationZ) + planeNaturalTorque);
        if (rb.velocity.x < 0) rb.velocity = new Vector2(0, rb.velocity.y);
        previuosSpeed = planeSpeed;
    }
    float GetPlaneRotation()
    {
        float planeRotationZ = transform.eulerAngles.z;
        if (planeRotationZ > 180) planeRotationZ -= 360;
        return planeRotationZ;
    }

    float GetTorque(float vertical, float planeRotationZ)
    {
        float torque = vertical * planeRotationSpeed;        

        if (planeRotationZ > planeMaxZRotation)
        {
            if (torque > 0) torque = 0;
            transform.eulerAngles = new Vector3(0, 0, planeMaxZRotation);
        }
        if (planeRotationZ < planeMinZRotation)
        {
            if (torque < 0) torque = 0;
            transform.eulerAngles = new Vector3(0, 0, planeMinZRotation);
        }
        return torque;
    }
}
