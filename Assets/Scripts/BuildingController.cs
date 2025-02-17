using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildingController : MonoBehaviour
{
    [SerializeField] float GridScale;
    [SerializeField] GameObject ObjectPrefab;
    [SerializeField] GameObject CursorPrefab;
    GameObject currObject;
    GameObject cursor;
    Vector2 FirstPoint;
    bool clicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clicked = false;
        cursor = Instantiate(CursorPrefab, Vector2.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currFixedPosition = new Vector2(Mathf.Round(currPosition.x / GridScale) * GridScale, Mathf.Round(currPosition.y / GridScale) * GridScale);

        cursor.transform.position = currFixedPosition;

        if (Input.GetMouseButtonDown(0))
        {
            FirstPoint = currFixedPosition;
            currObject = Instantiate(ObjectPrefab, Vector2.zero, Quaternion.identity);
            clicked = true;
        }else if (Input.GetMouseButtonUp(0))
        {
            currObject = null;
            clicked = false;
        }
        if (clicked)
        {
            currObject.transform.position = (FirstPoint + currFixedPosition) / 2;
            currObject.transform.localScale = new Vector2(Vector2.Distance(FirstPoint, currFixedPosition), currObject.transform.localScale.y);
            currObject.transform.right = currFixedPosition - (Vector2)currObject.transform.position;
        }
    }
}
