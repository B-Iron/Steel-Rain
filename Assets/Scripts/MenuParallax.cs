using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] float offsetMultiplier = 1.0f;
    [SerializeField] float smoothTime = 0.3f;

    private Vector2 startPosition;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
    }
}
