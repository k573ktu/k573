using UnityEngine;

public class ZoomingManager : MonoBehaviour
{
    [SerializeField] Vector2 CameraZoomMinMax;
    [SerializeField] float ScrollStrength;

    Camera cameraObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraObj = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MovementManager.inst.canMove) return;
        cameraObj.orthographicSize = Mathf.Clamp(cameraObj.orthographicSize - Input.mouseScrollDelta.y * ScrollStrength, CameraZoomMinMax.x, CameraZoomMinMax.y);
    }
}
