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

    [SerializeField] private float planeMaxZRotation;

    private Rigidbody2D rb;

    float previuosSpeed = 0;
    float naturalRotation = 0;

    [SerializeField] private Transform smallWheel;
    [SerializeField] private Transform bigWheel;
    [SerializeField] private Transform smallWheelOrigin;
    [SerializeField] private Transform bigWheelOrigin;
    [SerializeField] private Transform bigWheelVisual;
    [SerializeField] private Transform smallWheelVisual;

    private Vector3 smallWheelLenght;
    private Vector3 bigWheelLenght;

    [SerializeField] private float suspensionMult;
    [SerializeField] private float dumpingForce;

    [SerializeField] private LayerMask layerMask;

    bool isOnGround = false;
    float framesSinceOnGround = 0;
    public float currentSpeed;

    [SerializeField] private float planeFuelUsageMult;
    [SerializeField] private float planeFuel;
    [SerializeField] private int planeMaxFuel;

    [SerializeField] private float planeHP;
    [SerializeField] private int planeMaxHP;


    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    [SerializeField] private float outOfBoundsDamage;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(planeInitialSpeed, 0);

        smallWheelLenght = smallWheel.localPosition - smallWheelOrigin.localPosition;
        bigWheelLenght = bigWheel.localPosition - bigWheelOrigin.localPosition;
    }

    void FixedUpdate()
    {
        currentSpeed = Vector3.Dot(rb.velocity, Vector3.right);
        framesSinceOnGround++;
        //if (rb.velocity.x < 0) rb.velocity = new Vector2(0, rb.velocity.y);
        

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = -Input.GetAxis("Vertical");

        float planeSpeedVertical = Vector3.Dot(rb.velocity, transform.right);
        float planeSpeedHorizontal = Vector3.Dot(rb.velocity, transform.up);
        float planeRotationZ = GetPlaneRotation();

        if (planeRotationZ < -planeMaxZRotation)
        {
            //transform.eulerAngles = new Vector3(0, 0, -planeMaxZRotation);
            planeRotationZ = -planeMaxZRotation;
            naturalRotation = 0;
        }
        if (planeRotationZ > planeMaxZRotation)
        {
            //transform.eulerAngles = new Vector3(0, 0, planeMaxZRotation);
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
        if(desiredRotation < 0 && isOnGround) desiredRotation = 0;
        if (isOnGround) naturalRotation = 0;
        if ((planeSpeed - previuosSpeed) < 0) naturalRotation += (planeSpeed - previuosSpeed) * planeNaturalRotationMult;
        if(naturalRotation < 0 && (vertical > 0 || horizontal > 0)) naturalRotation += Mathf.Abs(planeSpeed - previuosSpeed) * planeNaturalRotationMult;
        if (!isOnGround)
        {
            float currentRotationChange = (desiredRotation + naturalRotation - planeRotationZ) * planeRotationSpeed / 100;
            if (framesSinceOnGround > 0 && framesSinceOnGround < 100) currentRotationChange *= framesSinceOnGround / 100;
            rb.MoveRotation(planeRotationZ + currentRotationChange);
        }
        
            


        rb.gravityScale = planeGravityBySpeed.Evaluate(1 - rb.velocity.magnitude / planeMaxSpeed * 1.5f);
        previuosSpeed = planeSpeed;

        RaycastHit2D hit = Physics2D.Raycast(smallWheelOrigin.position, smallWheelLenght.normalized, smallWheelLenght.magnitude, layerMask);
        bool didSmallWheelTouch = hit.collider;
        if (hit.collider)
        {
            Debug.DrawRay(smallWheelOrigin.position, smallWheelLenght.normalized * hit.distance, Color.yellow, 0.1f);
            Vector3 suspensionForce = GetSuspensionForce(-smallWheelLenght.normalized, smallWheelOrigin.position, hit.distance, smallWheelLenght.magnitude, suspensionMult, dumpingForce * 5);
            rb.AddForceAtPosition(suspensionForce, smallWheelOrigin.position);
            smallWheelVisual.transform.position = hit.point;
        }
        else
        {
            smallWheelVisual.transform.position = smallWheel.position;
            Debug.DrawRay(smallWheelOrigin.position, smallWheelLenght.normalized * smallWheelLenght.magnitude, Color.blue, 0.1f);
        }
        hit = Physics2D.Raycast(bigWheelOrigin.position, bigWheelLenght.normalized, bigWheelLenght.magnitude, layerMask);
        bool didBigWheelTouch = hit.collider;
        if (hit.collider)
        {

            Debug.DrawRay(bigWheelOrigin.position, bigWheelLenght.normalized * hit.distance, Color.yellow, 0.1f);
            Vector3 suspensionForce = GetSuspensionForce(-bigWheelLenght.normalized, bigWheelOrigin.position, hit.distance, bigWheelLenght.magnitude, suspensionMult, dumpingForce);    

            rb.AddForceAtPosition(suspensionForce, bigWheelOrigin.position);
            bigWheelVisual.transform.position = hit.point;
        }
        else
        {
            bigWheelVisual.transform.position = bigWheel.position;
            Debug.DrawRay(bigWheelOrigin.position, bigWheelLenght.normalized * bigWheelLenght.magnitude, Color.blue, 0.1f);
        }
        isOnGround = didBigWheelTouch;
        if(isOnGround) framesSinceOnGround = 0;

        CalculateFuelUsage(horizontal, planeRotationZ);

        CalculateOutOfBoundsDamage();
    }
    float GetPlaneRotation()
    {
        float planeRotationZ = transform.eulerAngles.z;
        if (planeRotationZ > 180) planeRotationZ -= 360;
        return planeRotationZ;
    }

    Vector3 GetSuspensionForce(Vector3 springDirection, Vector3 wheelPosition, float hitDistance, float suspensionRestDistance, float suspensionForce, float suspesionDumping)
    {
        Vector3 tireWorldVelocity = rb.GetPointVelocity(wheelPosition);

        float offset = suspensionRestDistance - hitDistance;
        float velocity = Vector3.Dot(springDirection, tireWorldVelocity);

        float force = (offset * suspensionForce) - (velocity * suspesionDumping);

        return springDirection * force;
    }

    void CalculateFuelUsage(float horizontal, float planeRotationZ)
    {
        float fuelUsage = 0;

        if (horizontal > 0){
            fuelUsage += horizontal * Time.fixedDeltaTime * planeFuelUsageMult;
            
        }

        planeFuel -= fuelUsage;
        UIManager.healthFuelUI.UpdateUI(planeHP, planeMaxHP, planeFuel, planeMaxFuel);
    }

    public void AddFuel(int fuelAmount)
    {
        planeFuel += fuelAmount;
        if(planeFuel > planeMaxFuel) planeFuel = planeMaxFuel;
        UIManager.healthFuelUI.UpdateUI(planeHP, planeMaxHP, planeFuel, planeMaxFuel);
    }
    public void AddHP(int HPAmount)
    {
        planeHP += HPAmount;
        if (planeHP > planeMaxHP) planeHP = planeMaxHP;
        UIManager.healthFuelUI.UpdateUI(planeHP, planeMaxHP, planeFuel, planeMaxFuel);
    }

    public void TakeDamage(float damageAmount)
    {
        planeHP -= damageAmount;
        if(planeHP < 0){
            planeHP = 0;
            UIManager.gameOverlUI.gameObject.SetActive(true);
        }
        UIManager.healthFuelUI.UpdateUI(planeHP, planeMaxHP, planeFuel, planeMaxFuel);
    }

    void CalculateOutOfBoundsDamage()
    {
        if(transform.position.y < minHeight || transform.position.y > maxHeight){
            TakeDamage(Time.deltaTime * outOfBoundsDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
        collision.GetContacts(contacts);
        float totalImpulse = 0;
        foreach (ContactPoint2D contact in contacts)
        {
            totalImpulse += contact.normalImpulse;
        }
        //Debug.Log(totalImpulse);
        if (totalImpulse > 20) totalImpulse *= 1.5f;
        TakeDamage(totalImpulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out EnemyController enemyController)){
            TakeDamage(30);
            rb.AddForce((transform.position - collision.transform.position) * 100, ForceMode2D.Force);
            enemyController.Explode();
        }
    }    
}
