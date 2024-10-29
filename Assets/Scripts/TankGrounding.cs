using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGrounding : MonoBehaviour
{
    [SerializeField] public Transform tankBody; // The main part of the tank to adjust
    [SerializeField] public float raycastLength = 1.0f; // Adjust based on how far from the tank you want to detect ground
    [SerializeField] public LayerMask groundLayer; // Layer mask to specify what is considered ground

    private Vector3 lastValidFrontPoint;
    private Vector3 lastValidBackPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GroundTank();
    }

    void GroundTank()
    {
        // Raycast from the front and back of the tank
        Vector3 frontRayOrigin = transform.position + (transform.forward * 0.5f);
        Vector3 backRayOrigin = transform.position - (transform.forward * 0.5f);

        bool frontHit = Physics.Raycast(frontRayOrigin, Vector3.down, out RaycastHit frontHitInfo, raycastLength, groundLayer);
        bool backHit = Physics.Raycast(backRayOrigin, Vector3.down, out RaycastHit backHitInfo, raycastLength, groundLayer);

        if (frontHit) lastValidFrontPoint = frontHitInfo.point;
        if (backHit) lastValidBackPoint = backHitInfo.point;

        if (frontHit && backHit)
        {
            // Calculate the middle point between the front and back hits
            Vector3 groundPosition = (lastValidFrontPoint + lastValidBackPoint) / 2;
            transform.position = groundPosition;

            // Set the tank's rotation to align with the ground
            Vector3 direction = lastValidFrontPoint - lastValidBackPoint;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, targetRotation.eulerAngles.z);
        }
        else if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit centerHitInfo, raycastLength, groundLayer))
        {
            // Fallback to center raycast if both front and back fail (e.g., straddling a tiny platform)
            transform.position = centerHitInfo.point;
        }
    }
}
