using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<WheelCollider> throttleWheels = new List<WheelCollider>();
    public List<WheelCollider> steeringWheels = new List<WheelCollider>();
    public float throttleCoefficient = 20000f;
    public float maxTurn = 20f;
    float turn = 0f;
    float acceleration = 1f;

	void FixedUpdate()
    {
        foreach (var wheel in throttleWheels)
        {
            wheel.motorTorque = throttleCoefficient * Time.fixedDeltaTime * acceleration;
        }
    }

    void Update()
    {
        foreach (var wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * turn;
        }
        turn = 0f;
    }

    public void SetTurn(float _turn)
    {
        turn = _turn;
    }

    public void SetAcceleration(float value)
    {
        acceleration = value;
    }
}