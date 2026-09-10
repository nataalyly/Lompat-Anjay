using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool followX = false;
    [SerializeField] private bool followY = true;

    private Vector3 startOffset;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        startOffset = transform.position - cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = transform.position;

        if (followY)
            newPos.y = cameraTransform.position.y + startOffset.y;

        if (followX)
            newPos.x = cameraTransform.position.x + startOffset.x;

        transform.position = newPos;
    }
}
