using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Tank : MonoBehaviour
{

    [SerializeField] TankShooting tankShooting;  // Reference to the TankShooting script
    [Header("Naming")]
    [SerializeField] string tankName;
    [Header("Health")]
    [SerializeField] int healthPoints = 1000;
    [Header("Movement")]
    [SerializeField] float speed = 1.0f;
    [Header("Track Information")]
    [SerializeField] bool isDead = false;

    private Vector3 mapCenter = Vector3.zero;  // Define map center, change this if your center is different

    void Awake()
    {
        // Determine initial facing direction based on tank's position relative to the center
        if (transform.position.x < mapCenter.x)  // Left of center
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Face right
            tankShooting?.SetFacingDirection(true);  // Update TankShooting to face right
        }
        else if (transform.position.x > mapCenter.x)  // Right of center
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);  // Face left
            tankShooting?.SetFacingDirection(false);  // Update TankShooting to face left
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (tankShooting == null)
        {
            tankShooting = GetComponent<TankShooting>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Move(Vector3 movement)
    {
        transform.localPosition += movement * speed * Time.deltaTime;

        if (movement.x > 0)  // Moving left
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);  // Face left
            tankShooting.SetFacingDirection(false);  // Update direction in TankShooting
        }
        else if (movement.x < 0)  // Moving right
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);  // Face right
            tankShooting.SetFacingDirection(true);  // Update direction in TankShooting
        }
    }

}
