using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController controller;
    private Rigidbody2D rb;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (controller.IsDead) return;

        if (col.gameObject.CompareTag("Platform"))
        {
            foreach (ContactPoint2D contact in col.contacts)
            {
                if (contact.normal.y > 0.7f && rb.linearVelocity.y <= 0.1f)
                {
                    PlatformController platform = col.gameObject.GetComponent<PlatformController>();
                    if (platform != null)
                        platform.OnPlayerLand();

                    controller.SetGrounded(true);
                    controller.Jump();
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            controller.SetGrounded(false);
        }
    }
}