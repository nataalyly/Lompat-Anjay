using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 15f;

    [Header("Screen Wrap")]
    [SerializeField] private float screenHalfWidth = 2.7f;

    [Header("Visual Juice")]
    [SerializeField] private Transform visualTransform;
    [SerializeField] private ParticleSystem dustParticlePrefab;
    [SerializeField] private TrailRenderer trailRenderer;

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
    private Vector3 defaultScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (visualTransform == null)
            visualTransform = transform;

        defaultScale = visualTransform.localScale;

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

        trailRenderer.emitting = true;
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
        bool didWrap = false;

        if (pos.x > screenHalfWidth)
        {
            pos.x = -screenHalfWidth;
            didWrap = true;
        }
        else if (pos.x < -screenHalfWidth)
        {
            pos.x = screenHalfWidth;
            didWrap = true;
        }

        if (didWrap)
        {
            transform.position = pos;
            if (trailRenderer != null)
            {
                trailRenderer.Clear();
            }
        }
        else
        {
            transform.position = pos;
        }
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        StopAllCoroutines();
        StartCoroutine(Bounce2DRoutine());

        if (dustParticlePrefab != null)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            ParticleSystem dust = Instantiate(dustParticlePrefab, spawnPos, Quaternion.identity);
            dust.Play();
        }

        if (AudioManager.Instance != null)
        AudioManager.Instance.PlayJump();
    }

    IEnumerator Bounce2DRoutine()
    {
        if (visualTransform == null) yield break;

        // Squash (Melebar mendatar, memipih ke atas sesaat)
        visualTransform.localScale = new Vector3(defaultScale.x * 1.25f, defaultScale.y * 0.75f, defaultScale.z);
        yield return new WaitForSeconds(0.04f);

        if (visualTransform == null) yield break;

        // Stretch (Memanjang ke atas saat melesat)
        visualTransform.localScale = new Vector3(defaultScale.x * 0.75f, defaultScale.y * 1.25f, defaultScale.z);
        
        // Kembalikan pelan-pelan ke ukuran normal
        float time = 0f;
        float duration = 0.12f;

        while (time < duration)
        {
            time += Time.deltaTime;
            if (visualTransform == null) yield break;

            visualTransform.localScale = Vector3.Lerp(visualTransform.localScale, defaultScale, time / duration);
            yield return null;
        }

        visualTransform.localScale = defaultScale;
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