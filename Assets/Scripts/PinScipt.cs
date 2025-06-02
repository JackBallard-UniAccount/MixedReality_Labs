using UnityEngine;

public class UprightDetector : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isUpright = true;

    // Threshold to determine if object is considered upright (in degrees)
    public float uprightAngleThreshold = 10f;

    private void Start()
    {
        // Save the initial position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        // Check if the object is upright
        isUpright = CheckIfUpright();

        // If not upright, disable the object
        if (!isUpright)
        {
            gameObject.SetActive(false);
        }
    }

    // Checks if the object's up vector is aligned with world up
    private bool CheckIfUpright()
    {
        float angle = Vector3.Angle(transform.forward, Vector3.up);
        return angle <= uprightAngleThreshold;
    }

    // Call this function to reset the object and re-enable it
    public void ResetObject()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        gameObject.SetActive(true);
    }
}
