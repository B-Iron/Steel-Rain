using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] Tank playerTank;
    [SerializeField] TankShooting tankShooting;

    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Handle movement input
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        playerTank.Move(movement);

        // Handle angle adjustment input
        if (Input.GetKey(KeyCode.UpArrow))
        {
            tankShooting.AdjustAngle(1); // Positive for up
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            tankShooting.AdjustAngle(-1); // Negative for down
        }

        // Handle shooting input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tankShooting.StartCharging();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            tankShooting.ChargeShot();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            tankShooting.ReleaseShot();
        }
    }
}
