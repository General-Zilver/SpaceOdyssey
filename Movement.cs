using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Fuel Settings")]
    public float maxFuel = 100f; // Total Fuel
    public float currentFuel = 100f; // Current Fuel (Updated)
    public float fuelBurnRate = 3f; // Fuel burn rate when moving forward or back
    public float turnFuelCost = 1f; // Fuel burn when turning

    [Header("Speed Settings")]
    public float maxSpeed = 5f;        // Fastest speed possible
    public float acceleration = 2f;    // How fast you build up speed
    public float deceleration = 50f;    // How fast you slow down when letting go
    public float rotationSpeed = 10f; // How fast you turn

    // Private variable to track current momentum
    private float currentSpeed = 0f;

    void Update()
    {
        // Turning Left/Right
        float turnInput = Input.GetAxis("Horizontal");
        if (turnInput != 0 && currentFuel > 0)
        {
            // 1. Do the rotation
            transform.Rotate(0, 0, -turnInput * rotationSpeed * Time.deltaTime);

            // 2. Burn the fuel (using the cheaper rate)
            currentFuel -= turnFuelCost * Time.deltaTime;
        }


        // Forward Momentum
        if (Input.GetAxis("Vertical") > 0 && currentFuel > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentFuel -= fuelBurnRate * Time.deltaTime;
        }
        else if (Input.GetAxis("Vertical") < 0 && currentFuel > 0)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            currentFuel -= fuelBurnRate * Time.deltaTime;
        }   

        // Never let speed fuel go over their max. 
        // Speed can go reverse up to -2, fuel can't go under 0
        currentSpeed = Mathf.Clamp(currentSpeed, -2, maxSpeed);

        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);

        // Apply the Movement
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
    }
}
