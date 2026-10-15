using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class JuicyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    private Vector3 originalScale;
    private Coroutine scaleCoroutine;
    private bool isInitialized = false;

    void Awake()
    {
        InitScale();
    }

    void OnEnable()
    {
        // Pastikan skala selalu di-reset saat tombol aktif kembali (misal ganti panel/menu)
        InitScale();
    }

    void InitScale()
    {
        if (!isInitialized)
        {
            originalScale = transform.localScale;
            isInitialized = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Menjaga agar skala dasar selalu diperbarui jika ada perubahan parent
        if (!isInitialized)
        {
            originalScale = transform.localScale;
            isInitialized = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isInitialized) InitScale();

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(ScaleTo(originalScale * 0.85f, 0.08f));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isInitialized) InitScale();

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(ScaleTo(originalScale, 0.12f));
    }

    IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        float time = 0f;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime; // Pakai unscaledDeltaTime agar tetap responsif walau game pause
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}