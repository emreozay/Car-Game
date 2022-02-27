using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    public static bool isBreaking;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    [SerializeField] private GameObject centerOfMass;
    
    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;

    private Quaternion firstRotation;
    public static bool isRestart;
    public static float carSpeed;

    private float distanceGround = 5f;
    private bool isGrounded = false;

    private void Start()
    {
        firstRotation = transform.rotation;

        if(centerOfMass != null)
            GetComponent<Rigidbody>().centerOfMass = centerOfMass.transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || isRestart)
        {
            transform.rotation = firstRotation;
        }
        print(GetComponent<Rigidbody>().centerOfMass);
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        GroundCheck();
    }

    private void GroundCheck()
    {
        if (!Physics.Raycast(transform.position + Vector3.up, Vector3.down, distanceGround))
        {
            isGrounded = false;
            UIController.gameOverEvent.Invoke();
        }
        else
        {
            isGrounded = true;
        }
    }

    private void GetInput()
    {
        horizontalInput = SimpleInput.GetAxis("Horizontal"); /*Input.GetAxis("Horizontal");*/
        verticalInput = SimpleInput.GetAxis("Vertical");/*Input.GetAxis("Vertical");*/


        //isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        carSpeed = GetComponent<Rigidbody>().velocity.magnitude;

        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;

        brakeForce = isBreaking ? 3000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;

        //carSpeed = frontLeftWheelCollider.rpm;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Finish"))
        {
            UIController.finishEvent.Invoke();

            PlayerPrefs.SetInt("AdCounter", PlayerPrefs.GetInt("AdCounter", 0) + 1);
            if (PlayerPrefs.GetInt("AdCounter", 0) >= 2)
            {
                InterstitialAd.interstitialAdEvent.Invoke();
                PlayerPrefs.SetInt("AdCounter", 0);
            }
        }
    }
}
