using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    [Header("Settings")]
    public float trailDuration = 0.05f;
    public float trailWidth = 0.05f;
    public Color trailColor = Color.yellow;

    private LineRenderer lineRenderer;
    private float endTime;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = trailWidth;
        lineRenderer.endWidth = trailWidth * 0.5f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = trailColor;
        lineRenderer.endColor = trailColor;
        lineRenderer.enabled = false;
    }

    public void ShowTrail(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;
        endTime = Time.time + trailDuration;
    }

    void Update()
    {
        if (lineRenderer.enabled && Time.time >= endTime)
        {
            lineRenderer.enabled = false;
        }
    }
}
