using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform Prefabs")]
    [SerializeField] private GameObject platformNormal;
    [SerializeField] private GameObject platformMoving;
    [SerializeField] private GameObject platformCracked;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnWidth = 7f;
    [SerializeField] private float minGapY = 1.2f;
    [SerializeField] private float maxGapY = 2.0f;
    [SerializeField] private float spawnAheadDistance = 10f;

    [Header("Difficulty")]
    [SerializeField] private float movingStartHeight = 20f;
    [SerializeField] private float crackedStartHeight = 40f;
    [SerializeField] private float maxGapIncreasePerUnit = 0.005f;

    [Header("Despawn")]
    [SerializeField] private float despawnBelowCamera = 10f;

    private Transform cameraTransform;
    private float nextSpawnY;
    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        cameraTransform = Camera.main.transform;
        nextSpawnY = -2f;

        for (int i = 0; i < 10; i++)
            SpawnPlatform();
    }

    void Update()
    {
        while (nextSpawnY < cameraTransform.position.y + spawnAheadDistance)
            SpawnPlatform();

        DespawnOldPlatforms();
    }

    private void SpawnPlatform()
    {
        float height = nextSpawnY;

        float difficulty = Mathf.Clamp01((height - 30f) / 100f);
        float gapY = Mathf.Lerp(minGapY, maxGapY + height * maxGapIncreasePerUnit, difficulty);
        nextSpawnY += gapY;

        float x = Random.Range(-spawnWidth, spawnWidth);

        GameObject prefab = ChoosePlatformType(height);

        GameObject platform = Instantiate(prefab, new Vector3(x, height, 0), Quaternion.identity);
        activePlatforms.Add(platform);
    }

    private GameObject ChoosePlatformType(float height)
    {
        if (height >= crackedStartHeight)
        {
            float roll = Random.value;
            if (roll < 0.35f) return platformCracked;
            if (roll < 0.60f) return platformMoving;
            return platformNormal;
        }
        else if (height >= movingStartHeight)
        {
            float roll = Random.value;
            if (roll < 0.4f) return platformMoving;
            return platformNormal;
        }

        return platformNormal;
    }

    private void DespawnOldPlatforms()
    {
        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            if (activePlatforms[i] == null)
            {
                activePlatforms.RemoveAt(i);
                continue;
            }

            if (activePlatforms[i].transform.position.y < cameraTransform.position.y - despawnBelowCamera)
            {
                Destroy(activePlatforms[i]);
                activePlatforms.RemoveAt(i);
            }
        }
    }
}