using System.Net;
using UnityEngine;
using UnityEngine.Rendering;

public class Arrow : MonoBehaviour
{
    public float maxArrowDisplayLength;

    [SerializeField] protected GameObject arrowPointPrefab;
    private GameObject arrowPoint;

    [SerializeField] Color ArrowColor;

    [SerializeField] protected GameObject analyzedObject;

    LineRenderer arrowLine;

    protected Vector2 arrowDirection;

    Vector2 endPoint;


    protected virtual void Start()
    {
        arrowLine = GetComponent<LineRenderer>();
        arrowLine.startColor = ArrowColor;
        arrowLine.material.color = ArrowColor;
        arrowLine.enabled = false;
        arrowPoint = Instantiate(arrowPointPrefab);
        arrowPoint.GetComponent<SpriteRenderer>().color = ArrowColor;
        arrowPoint.SetActive(false);

        GameManager.inst.RegisterArrow(this);
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

    protected virtual void UpdateDisplay()
    {
        if (!arrowLine.enabled) return;
        arrowLine.SetPosition(0, analyzedObject.transform.position);
        arrowLine.SetPosition(1, endPoint);
        arrowPoint.transform.position = endPoint;
        arrowPoint.transform.up = endPoint - (Vector2)analyzedObject.transform.position;
    }

    void Update()
    {
        UpdateArrowDirection();
        endPoint = (Vector2)analyzedObject.transform.position + arrowDirection;
        UpdateDisplay();  
    }
}
