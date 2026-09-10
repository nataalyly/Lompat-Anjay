using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offsetY = 2f;
    [SerializeField] private float deadZoneBottom = 3f;

    private float highestY;

    void Start()
    {
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player == null) return;

        float targetY = player.position.y + offsetY;

        if (targetY > highestY + deadZoneBottom)
        {
            highestY = targetY - deadZoneBottom;
            transform.position = new Vector3(
                transform.position.x,
                highestY,
                transform.position.z
            );
        }
    }
}