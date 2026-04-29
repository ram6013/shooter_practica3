using UnityEngine;
using UnityEngine.UI;

public class MuzzleFlash : MonoBehaviour
{
    [Header("Settings")]
    public Image flashImage;
    public Sprite flashSprite;
    public float flashDuration = 0.05f;

    private float hideTime;

    void Start()
    {
        if (flashImage == null)
        {
            GameObject canvas = new GameObject("MuzzleFlashCanvas");
            canvas.transform.SetParent(transform);
            Canvas c = canvas.AddComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            c.sortingOrder = 10;

            GameObject img = new GameObject("FlashImage");
            img.transform.SetParent(canvas.transform);
            flashImage = img.AddComponent<Image>();
            flashImage.enabled = false;

            RectTransform rt = img.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(200, 200);
            rt.anchoredPosition = Vector2.zero;
        }

        if (flashSprite != null)
            flashImage.sprite = flashSprite;

        flashImage.enabled = false;
    }

    public void ShowFlash()
    {
        if (flashImage != null)
        {
            flashImage.enabled = true;
            hideTime = Time.time + flashDuration;
        }
    }

    void Update()
    {
        if (flashImage != null && flashImage.enabled && Time.time >= hideTime)
        {
            flashImage.enabled = false;
        }
    }
}
