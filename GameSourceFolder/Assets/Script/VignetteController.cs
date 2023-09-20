using UnityEngine;

public class VignetteController : MonoBehaviour
{
    private Vector3 initialVignettePosition;

    private void Start()
    {
        initialVignettePosition = transform.position;
    }

    public void MoveToTargetPosition(Vector3 targetPosition)
    {
        // Move the vignette effect to the target position
        transform.position = targetPosition;
    }

    public void ReturnToInitialPosition()
    {
        // Return the vignette effect to its initial position
        transform.position = initialVignettePosition;
    }
}
