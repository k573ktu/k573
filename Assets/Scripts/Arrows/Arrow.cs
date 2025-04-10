using System.Net;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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

    [SerializeField] bool showAlways;

    protected virtual void Start()
    {
        arrowLine = GetComponent<LineRenderer>();
        arrowLine.startColor = ArrowColor;
        arrowLine.material.color = ArrowColor;
        arrowLine.enabled = false;
        
        arrowPoint = Instantiate(arrowPointPrefab);
        SceneManager.MoveGameObjectToScene(arrowPoint, gameObject.scene);
        arrowPoint.GetComponent<SpriteRenderer>().color = ArrowColor;
        arrowPoint.SetActive(false);

        if (GameManager.inst.displayScene) return;
        
        GameManager.inst.RegisterArrow(this);
        if (showAlways)
        {
            show();
        }
        UpdateDisplay();
    }

    protected virtual void UpdateArrowDirection() { }

    public void show()
    {
        if (GameManager.inst.displayScene) return;
        if (!analyzedObject.activeSelf)
        {
            hide();
            return;
        }
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
        if (GameManager.inst.displayScene) return;
        if (showAlways) show();
        UpdateArrowDirection();
        endPoint = (Vector2)analyzedObject.transform.position + arrowDirection;
        UpdateDisplay();  
    }
}
