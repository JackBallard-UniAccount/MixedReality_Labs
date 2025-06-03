using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isKnockedOver = false;

    [Tooltip("Angle (in degrees) allowed to still consider the pin upright")]
    public float uprightAngleThreshold = 10f;

    [SerializeField] public NewPinManager pinManager;

    private void Start()
    {
        // Store the starting transform of the pin
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (!isKnockedOver && !IsUpright())
        {
            KnockOver();
        }
    }

    private bool IsUpright()
    {
        // Calculate angle between this pin's up vector and world up
        float angle = Vector3.Angle(transform.forward, Vector3.up);
        return angle <= uprightAngleThreshold;
    }

    private void KnockOver()
    {
        isKnockedOver = true;

        // Notify a GameManager, if needed
        //NewPinManager.Instance?.PinKnockedOver(this);
        pinManager.PinKnockedOver(this);

        // Disable the pin (simulate falling)
        //gameObject.SetActive(false);
    }

    // Call to reset and re-enable the pin
    public void ResetPin()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        isKnockedOver = false;
        gameObject.SetActive(true);
    }

    // Optional: helper to check status
    public bool IsKnockedOver()
    {
        return isKnockedOver;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}