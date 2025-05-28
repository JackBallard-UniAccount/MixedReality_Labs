using UnityEngine;
using System.Collections;

public class MoveAndReorientToAnchor : MonoBehaviour
{
    [Tooltip("The GameObject to move to this object's position.")]
    public GameObject targetObject;

    [Tooltip("Delay in seconds to wait before moving the object.")]
    public float waitSeconds = 0.5f;

    [Header("Optional Position and Rotation Offsets")]
    public Vector3 positionOffset;
    public Vector3 eulerRotationOffset;

    void Start()
    {
        StartCoroutine(MoveAndReorient());
    }

    IEnumerator MoveAndReorient()
    {
        yield return new WaitForSeconds(waitSeconds);

        if (targetObject != null)
        {
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
