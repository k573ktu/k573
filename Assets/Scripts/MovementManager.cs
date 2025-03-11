using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{
    public static MovementManager inst;

    [SerializeField] bool borderless;
    [SerializeField] Vector2 MaxCameraBorders;

    Camera cameraObj;
    Vector2 cameraWorldSize;

    Vector2 mouseStart;
    bool holding;
    [System.NonSerialized] public bool canMove;

    public GraphicRaycaster graphicRaycaster;

    private void OnDrawGizmosSelected()
    {
        if (!borderless)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector2.zero, MaxCameraBorders);
        }
    }

    bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                return true;
            }
        }

        return false;
    }

    private void Awake()
    {
        if (inst == null) inst = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        holding = false;
        cameraObj = Camera.main;
        canMove = true;

        if (graphicRaycaster == null)
        {
            graphicRaycaster = FindFirstObjectByType<GraphicRaycaster>();
        }

        updateCameraSize();
    }

    void updateCameraSize()
    {
        cameraWorldSize.y = cameraObj.orthographicSize * 2;
        cameraWorldSize.x = cameraWorldSize.y * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUI())
            {
                mouseStart = cameraObj.ScreenToWorldPoint(Input.mousePosition);
                holding = true;
            }
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

        
        
        updateCameraSize();

        if (borderless) return;

        float xOffsetMin = Mathf.Max(0, -MaxCameraBorders.x / 2 - (cameraObj.transform.position.x - cameraWorldSize.x / 2));
        float xOffsetMax = Mathf.Min(0, MaxCameraBorders.x / 2 - (cameraObj.transform.position.x + cameraWorldSize.x / 2));
        float xOffset;

        if (xOffsetMin == 0)
        {
            xOffset = xOffsetMax;
        }else if (xOffsetMax == 0)
        {
            xOffset = xOffsetMin;
        }
        else
        {
            return;
        }

        float yOffsetMin = Mathf.Max(0, -MaxCameraBorders.y/2 - (cameraObj.transform.position.y - cameraWorldSize.y/2));
        float yOffsetMax = Mathf.Min(0, MaxCameraBorders.y/2 - (cameraObj.transform.position.y + cameraWorldSize.y/2));
        float yOffset;

        if (yOffsetMin == 0)
        {
            yOffset = yOffsetMax;
        }
        else if (yOffsetMax == 0)
        {
            yOffset = yOffsetMin;
        }
        else
        {
            return;
        }

        cameraObj.transform.position += new Vector3(xOffset, yOffset, 0);
    }
}
