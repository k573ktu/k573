using DG.Tweening.Core.Easing;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraFollowZoom : MonoBehaviour
{
    public Transform[] targets;
    public float smoothTime = 0.5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (targets.Length == 0) return;

        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 newPosition = GetCenterPoint();

        newPosition = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float greatestDistance = GetGreatestDistance();

        float newZoom = Mathf.Lerp(maxZoom, minZoom, greatestDistance / zoomLimiter);
        if (cam.orthographic)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        }
    }

    Vector3 GetCenterPoint()
    {
        bool was = false;

        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            if (!targets[i].gameObject.activeSelf) continue;
            if (!was)
            {
                was = true;
                bounds = new Bounds(targets[i].position, Vector3.zero);
            }
            else
            {
                bounds.Encapsulate(targets[i].position);
            }
        }

        if (!was)
        {
            return transform.position;
        }

        return bounds.center;
    }

    float GetGreatestDistance()
    {
        bool was = false;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            if (!targets[i].gameObject.activeSelf) continue;
            if (!was)
            {
                was = true;
                bounds = new Bounds(targets[i].position, Vector3.zero);
            }
            else
            {
                bounds.Encapsulate(targets[i].position);
            }
        }

        return Mathf.Max(bounds.size.x, bounds.size.y);
    }
}

