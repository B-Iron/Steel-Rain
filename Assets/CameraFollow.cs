using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTank;
    private Transform target;
    private Vector3 offset;
    private bool followingProjectile;

    // Start is called before the first frame update
    void Start()
    {
        target = playerTank; // Start by following the player tank
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Smoothly follow the target with an offset
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);
        }
    }

    // Set the camera to follow the projectile
    public void FollowProjectile(Transform projectile)
    {
        target = projectile;
        followingProjectile = true;
    }

    // Reset to follow the player tank once the projectile is destroyed
    public void ResetToPlayer()
    {
        target = playerTank;
        followingProjectile = false;
    }

}
