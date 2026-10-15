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
    [SerializeField] private GameObject[] debrisPiecePrefabs;
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
                // Hancur dan lemparkan beberapa kepingan fisik (Rigidbody2D)
                if (debrisPiecePrefabs != null && debrisPiecePrefabs.Length > 0)
                {
                    Vector2 platformSize = new Vector2(1.5f, 0.3f); // Ukuran default estimasi
                    BoxCollider2D col = GetComponent<BoxCollider2D>();
                    if (col != null)
                    {
                        platformSize = col.size;
                    }

                    for (int i = 0; i < 6; i++) // Spawn 6 kepingan
                    {
                        int randomIndex = Random.Range(0, debrisPiecePrefabs.Length);
                        GameObject selectedPiecePrefab = debrisPiecePrefabs[randomIndex];

                        float randomX = Random.Range(-platformSize.x / 2f, platformSize.x / 2f);
                        float randomY = Random.Range(-platformSize.y / 2f, platformSize.y / 2f);
                        Vector3 spawnPos = transform.position + new Vector3(randomX, randomY, 0f);

                        GameObject piece = Instantiate(selectedPiecePrefab, transform.position, Quaternion.identity);
                        
                        // Berikan dorongan fisik acak ke kiri/kanan dan atas agar mental
                        Rigidbody2D rbPiece = piece.GetComponent<Rigidbody2D>();
                        if (rbPiece != null)
                        {
                            Vector2 randomForce = new Vector2(randomX * 3f + Random.Range(-2f, 2f), Random.Range(3f, 6f));
                            rbPiece.linearVelocity = randomForce;
                            
                            // Hapus kepingan setelah 1.5 detik biar nggak numpuk di memori
                            Destroy(piece, 1.5f);
                        }
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
