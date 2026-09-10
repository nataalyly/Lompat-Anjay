using UnityEngine;

public enum PlatformType
{
    Normal,
    Moving,
    Cracked
}

public class PlatformController : MonoBehaviour
{
    [Header("Platform Type")]
    public PlatformType platformType = PlatformType.Normal;

    [Header("Moving Platform")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 2f;

    [Header("Cracked Platform")]
    [SerializeField] private Sprite spriteNormal;
    [SerializeField] private Sprite spriteCracked;
    private bool isCracked = false;
    private SpriteRenderer sr;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (platformType == PlatformType.Moving)
            HandleMoving();
    }

    private void HandleMoving()
    {
        float x = startPos.x + Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public void OnPlayerLand()
    {
        if (platformType == PlatformType.Cracked)
        {
            if (!isCracked)
            {
                isCracked = true;
                if (spriteCracked != null)
                    sr.sprite = spriteCracked;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
