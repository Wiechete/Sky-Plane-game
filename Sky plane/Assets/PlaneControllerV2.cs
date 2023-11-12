using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControllerV2 : MonoBehaviour
{
    [SerializeField] private float planeInitialSpeed;
    [SerializeField] private AnimationCurve planePower;
    [SerializeField] private float planePowerMult;
    [SerializeField] private float planeMaxSpeed;
    [SerializeField] private float planeRotationSpeed;
    [SerializeField] private float planeNaturalRotationMult;
    [SerializeField] private AnimationCurve planeUpForceBySpeed;
    [SerializeField] private AnimationCurve planeGravityBySpeed;
    [SerializeField] private float planeUpForceMult;


    [SerializeField] private float planeFuel;
    [SerializeField] private float planeMaxFuel;

    [SerializeField] private float planeMaxZRotation;

    private Rigidbody2D rb;

    float previuosSpeed = 0;
    float naturalRotation = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(planeInitialSpeed, 0);
    }

    void FixedUpdate()
    {
        if (rb.velocity.x < 0) rb.velocity = new Vector2(0, rb.velocity.y);
        

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = -Input.GetAxis("Vertical");

        float planeSpeedVertical = Vector3.Dot(rb.velocity, transform.right);
        float planeSpeedHorizontal = Vector3.Dot(rb.velocity, transform.up);
        float planeRotationZ = GetPlaneRotation();

        if (planeRotationZ < -planeMaxZRotation)
        {
            transform.eulerAngles = new Vector3(0, 0, -planeMaxZRotation);
            planeRotationZ = -planeMaxZRotation;
        }
        if (planeRotationZ > planeMaxZRotation)
        {
            transform.eulerAngles = new Vector3(0, 0, planeMaxZRotation);
            planeRotationZ = planeMaxZRotation;
        }

        Vector3 accelerationForce = transform.right * horizontal * planePower.Evaluate(planeSpeedVertical / planeMaxSpeed) * planePowerMult;
        if (horizontal < 0) accelerationForce *= 0.3f;


        float planeUpForce = -planeSpeedHorizontal * planeUpForceMult * planeUpForceBySpeed.Evaluate(rb.velocity.magnitude / planeMaxSpeed * 1.5f);        

        if (planeSpeedVertical > planeMaxSpeed && horizontal > 0) accelerationForce = Vector3.zero;
        if (planeSpeedVertical < 0 && horizontal < 0) accelerationForce = Vector3.zero;

        rb.AddForce(accelerationForce + transform.up * planeUpForce);

        float planeSpeed = rb.velocity.magnitude;

        float desiredRotation = planeMaxZRotation * vertical;        
        float naturalRotation = 0;
        if ((planeSpeed - previuosSpeed) < 0) naturalRotation += (planeSpeed - previuosSpeed) * planeNaturalRotationMult;
        if(naturalRotation < 0 && vertical > 0) naturalRotation += (desiredRotation + naturalRotation - planeRotationZ) * planeRotationSpeed / 100;

        rb.MoveRotation(planeRotationZ + (desiredRotation + naturalRotation - planeRotationZ) * planeRotationSpeed / 100);


        rb.gravityScale = planeGravityBySpeed.Evaluate(1 - rb.velocity.magnitude / planeMaxSpeed * 1.5f);
        previuosSpeed = planeSpeed;        
    }
    float GetPlaneRotation()
    {
        float planeRotationZ = transform.eulerAngles.z;
        if (planeRotationZ > 180) planeRotationZ -= 360;
        return planeRotationZ;
    }
}
