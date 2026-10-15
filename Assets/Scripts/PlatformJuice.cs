using System.Collections;
using UnityEngine;

public class PlatformJuice : MonoBehaviour
{
    private Vector3 originalPosition;
    private Coroutine bounceCoroutine;

    void Start()
    {
        // Simpan posisi awal platform
        originalPosition = transform.localPosition;
    }

    public void TriggerPlatformBounce()
    {
        // Berhentikan coroutine yang sedang berjalan agar tidak bertabrakan jika diinjak berturut-turut
        if (bounceCoroutine != null)
        {
            StopCoroutine(bounceCoroutine);
        }
        bounceCoroutine = StartCoroutine(PlatformSpringRoutine());
    }

    IEnumerator PlatformSpringRoutine()
    {
        float elapsedTime = 0f;
        float duration = 0.5f; // Durasi total membal (dari amblas sampai benar-benar diam)
        
        // Atur seberapa jauh platform amblas ke bawah (dalam unit Unity)
        float dipAmount = 0.35f; 
        
        // Atur kecepatan/banyaknya getaran (frekuensi per)
        float frequency = 18f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            
            // 1. Hitung redaman (damping) agar getaran lama-lama mengecil dan berhenti
            float damping = Mathf.Exp(-5f * (elapsedTime / duration)); // eksponensial turun
            
            // 2. Hitung gelombang pegas (spring oscillation)
            // Menggunakan Sinus untuk memantul naik-turun
            float bounceOffset = -dipAmount * Mathf.Sin(elapsedTime * frequency) * damping;

            // Update posisi platform (hanya sumbu Y)
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + bounceOffset, originalPosition.z);

            yield return null;
        }

        // Pastikan kembali ke posisi awal tepat waktu
        transform.localPosition = originalPosition;
        bounceCoroutine = null;
    }
}