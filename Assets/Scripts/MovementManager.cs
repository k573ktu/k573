using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] Vector2 CameraZoomMinMax;
    [SerializeField] float ScrollStrength;

    Camera cameraObj;

    Vector2 mouseStart;
    bool holding;
    [System.NonSerialized] public bool canMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        holding = false;
        cameraObj = Camera.main;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = cameraObj.ScreenToWorldPoint(Input.mousePosition);

            holding = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            holding = false;
        }

        if (holding)
        {
            Vector2 mouseCurrent = cameraObj.ScreenToWorldPoint(Input.mousePosition);

            Vector2 mouseOffset = mouseStart - mouseCurrent;

            cameraObj.transform.position = new Vector3(cameraObj.transform.position.x + mouseOffset.x, cameraObj.transform.position.y + mouseOffset.y, cameraObj.transform.position.z);
        }

        cameraObj.orthographicSize = Mathf.Clamp(cameraObj.orthographicSize - Input.mouseScrollDelta.y * ScrollStrength, CameraZoomMinMax.x, CameraZoomMinMax.y);
    }
}
