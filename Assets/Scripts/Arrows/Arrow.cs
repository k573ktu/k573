using UnityEngine;
using UnityEngine.Rendering;

public class Arrow : MonoBehaviour
{
    public float maxArrowDisplayLength;

    [SerializeField] protected GameObject arrowPointPrefab;
    private GameObject arrowPoint;

    [SerializeField] protected GameObject analyzedObject;

    LineRenderer arrowLine;

    protected Vector2 arrowDirection;


    protected virtual void Start()
    {
        arrowLine = GetComponent<LineRenderer>();
        arrowPoint = Instantiate(arrowPointPrefab);
        arrowPoint.SetActive(false);
    }

    protected virtual void UpdateArrowDirection() { }

    public void show()
    {
        arrowLine.enabled = true;
        arrowPoint.SetActive(true);
    }

    public void hide()
    {
        arrowLine.enabled = false;
        arrowPoint.SetActive(false);
    }

    void Update()
    {
        UpdateArrowDirection();
        Vector2 endPoint = (Vector2)analyzedObject.transform.position + arrowDirection;

        arrowLine.SetPosition(0, analyzedObject.transform.position);
        arrowLine.SetPosition(1, endPoint);
        arrowPoint.transform.position = endPoint;
    }
}
