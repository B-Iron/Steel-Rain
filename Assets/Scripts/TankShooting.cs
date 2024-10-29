using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    [SerializeField] CameraFollow cameraFollow; 
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firingPoint;
    [SerializeField] float minPower = 1f;
    [SerializeField] float maxPower = 100f;
    [SerializeField] float powerChargeRate = 20f;

    private float currentAngle = 45f;
    private float currentPower = 0f;
    private bool isCharging = false;
    private bool isFacingRight = true;  // Default to facing right
    private float minAngleRight = -210f;
    private float maxAngleRight = 360f;
    private float minAngleLeft = -30f;   // Mirror the range for left
    private float maxAngleLeft = 90f;    // Mirror the range for left

    // Start is called before the first frame update

    void Start()
    {
        UpdateAngleRange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustAngle(int direction)
    {
        // Adjust current angle within the range based on the facing direction
        currentAngle += direction * 60f * Time.deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, isFacingRight ? minAngleRight : minAngleLeft, isFacingRight ? maxAngleRight : maxAngleLeft);

        // Rotate the firing point for visual feedback
        float adjustedAngle = isFacingRight ? currentAngle : -currentAngle;  // Flip the angle if facing left
        firingPoint.rotation = Quaternion.Euler(0, 0, adjustedAngle);
    }

    public void SetFacingDirection(bool facingRight)
    {
        isFacingRight = facingRight;
        UpdateAngleRange();
    }

    private void UpdateAngleRange()
    {
        // Clamp angle for consistency when direction changes
        currentAngle = Mathf.Clamp(currentAngle, isFacingRight ? minAngleRight : minAngleLeft, isFacingRight ? maxAngleRight : maxAngleLeft);
    }

    public void StartCharging()
    {
        isCharging = true;
        currentPower = minPower;
    }

    public void ChargeShot()
    {
        if (isCharging)
        {
            currentPower += powerChargeRate * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, minPower, maxPower);
        }
    }

    public void ReleaseShot()
    {
        if (isCharging)
        {
            FireProjectile();
            isCharging = false;
            currentPower = minPower;
        }
    }

    public void FireProjectile()
    {
        // Flip the angle if facing left
        float firingAngle = isFacingRight ? currentAngle : -currentAngle;
        GameObject projectile = Instantiate(projectilePrefab, firingPoint.position, Quaternion.Euler(0, 0, firingAngle));
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(firingAngle, currentPower);
            // cameraFollow.FollowProjectile(projectile.transform);
        }
    }
}
