using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 15f;

    [Header("Screen Wrap")]
    [SerializeField] private float screenHalfWidth = 2.7f;

    [Header("Sprites")]
    [SerializeField] private Sprite[] jumpSprites;
    [SerializeField] private Sprite[] deadSprites;
    [SerializeField] private Sprite[] idleSprites;

    [Header("Animators")]
    [SerializeField] private RuntimeAnimatorController[] animatorControllers;


    private Sprite spriteJump;
    private Sprite spriteDead;
    private Sprite spriteIdle;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private bool isDead = false;
    private bool isGrounded = false;

    private float moveInput = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        int charIndex = GameData.SelectedCharacter;

        animator.runtimeAnimatorController = animatorControllers[charIndex];

        Jump();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;
    }

    void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleScreenWrap();
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput != 0)
            sr.flipX = moveInput < 0;
    }

    private void HandleScreenWrap()
    {
        Vector3 pos = transform.position;
        if (pos.x > screenHalfWidth)
            pos.x = -screenHalfWidth;
        else if (pos.x < -screenHalfWidth)
            pos.x = screenHalfWidth;
        transform.position = pos;
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        if (AudioManager.Instance != null)
        AudioManager.Instance.PlayJump();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        sr.sprite = spriteDead;
        animator.SetBool("isDead", true);
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        GameManager.Instance.GameOver();
    }

    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
        animator.SetBool("isGrounded", grounded);
    }

    public bool IsDead => isDead;
}