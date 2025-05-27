using System.Net;
using UnityEngine;

public class CameraFollowObjects : MonoBehaviour
{
    [SerializeField] GameObject objA;
    [SerializeField] GameObject objB;

    [SerializeField] float zoomPadding = 2f;
    [SerializeField] float zoomSpeed = 2f;
    [SerializeField] float minSize = 5f;
    [SerializeField] float maxSize = 20f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (objA.activeSelf && objB.activeSelf)
        {
            Vector2 midpoint = (objA.transform.position + objB.transform.position) / 2f;
            transform.position = new Vector3(midpoint.x, midpoint.y, transform.position.z);

            float distance = Vector2.Distance(objA.transform.position, objB.transform.position);
            float desiredSize = distance / 2f + zoomPadding;

            desiredSize = Mathf.Clamp(desiredSize, minSize, maxSize);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredSize, Time.deltaTime * zoomSpeed);
        }else if(objA.activeSelf)
        {
            transform.position = new Vector3(objA.transform.position.x, objA.transform.position.y, transform.position.z);
            cam.orthographicSize = 30;
        }
        else if (objB.activeSelf)
        {
            transform.position = new Vector3(objB.transform.position.x, objB.transform.position.y, transform.position.z);
            cam.orthographicSize = 30;
        }
    }
}

