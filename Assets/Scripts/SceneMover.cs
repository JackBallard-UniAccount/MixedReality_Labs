using UnityEngine;

public class MoveAndReorientToAnchor : MonoBehaviour
{
    [Tooltip("The GameObject to move to this object's position.")]
    public GameObject targetObject;

    [Header("Optional Position and Rotation Offsets")]
    public Vector3 positionOffset;
    public Vector3 eulerRotationOffset;

    private void Awake()
    {
        SpawnAndReorient();
    }

    void SpawnAndReorient()
    {
        if (targetObject != null)
        {
            Instantiate(targetObject, transform.position, Quaternion.identity);

            // Move to anchor's position with offset
            targetObject.transform.position = transform.position + positionOffset;

            // Apply rotation offset
            Quaternion rotationOffset = Quaternion.Euler(eulerRotationOffset);
            targetObject.transform.rotation = transform.rotation * rotationOffset;

            // Disable the anchor object (this one)
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Target object not assigned.");
        }
    }
}
