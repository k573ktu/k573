using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

[System.Serializable]
public struct PlanetData
{
    public string name;
    public float radius;
    public float mass;
    public Sprite planetSprite;
}

public class PlanetOptionData : OptionData
{
    [SerializeField] List<PlanetData> SpaceObjects;

    [SerializeField] Image planetImageUi;

    int currObject;

    [SerializeField] List<GameObject> planetObjects;

    [SerializeField] Sprite thrashSprite;

    Stack<GameObject> FreePlanets;

    GameObject picked;

    protected override void Start()
    {
        base.Start();
        currObject = 0;

        FreePlanets = new Stack<GameObject>();

        foreach(var obj in planetObjects)
        {
            FreePlanets.Push(obj);
        }

        updateCurrObject();

        MovementManager.inst.AssignMovingFunction(PickExisting);
    }

    void updateCurrObject()
    {
        if (picked != null)
        {
            planetImageUi.sprite = thrashSprite;
        }
        else
        {
            planetImageUi.sprite = SpaceObjects[currObject].planetSprite;
        }
        text.text = SpaceObjects[currObject].name;
    }

    public void NextObject()
    {
        currObject++;
        if (currObject == SpaceObjects.Count) currObject = 0;
        updateCurrObject();
    }

    public void PreviousObject()
    {
        currObject--;
        if (currObject == -1) currObject = SpaceObjects.Count-1;
        updateCurrObject();
    }

    void ShowCurrPlanet()
    {
        picked.GetComponent<Rigidbody2D>().mass = SpaceObjects[currObject].mass;

        picked.transform.localScale = new Vector3(SpaceObjects[currObject].radius, SpaceObjects[currObject].radius, analyzedObject.transform.localScale.z);

        picked.GetComponent<SpriteRenderer>().sprite = SpaceObjects[currObject].planetSprite;

        picked.GetComponent<ShadowCaster2D>().Update();

        picked.SetActive(true);
    }

    void HideCurrPlanet()
    {
        picked.SetActive(false);
    }

    public void PickObject(bool updateAppearance = true)
    {
        if(picked != null)
        {
            HideCurrPlanet();
            FreePlanets.Push(picked);
        }
        if (FreePlanets.Count == 0 || GameManager.inst.simPlaying) return;
        picked = FreePlanets.Pop();
        picked.GetComponent<TrailRenderer>().enabled = false;
        if (updateAppearance)
        {
            ShowCurrPlanet();
        }

        updateCurrObject();
    }

    void PickExisting(GameObject obj)
    {
        if (picked != null) return;
        FreePlanets.Push(obj);
        PickObject(false);
    }

    protected override void OnValueChanged(float value)
    {
        if (SpaceObjects.Count <= (int)Mathf.Round(value)) return;

        PlanetData selected = SpaceObjects[(int)Mathf.Round(value)];

        analyzedObject.GetComponent<Rigidbody2D>().mass = selected.mass;

        analyzedObject.transform.localScale = new Vector3(selected.radius, selected.radius, analyzedObject.transform.localScale.z);

        analyzedObject.GetComponent<SpriteRenderer>().sprite = selected.planetSprite;

        analyzedObject.GetComponent<ShadowCaster2D>().Update();

        text.text = OptionName + ": " + selected.name;
    }

    protected override void UpdateDisplay(float value) { }

    private void Update()
    {
        if(picked != null)
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            picked.transform.position = new Vector3(mouse.x, mouse.y, picked.transform.position.z);

            bool goodToPlace = !MovementManager.inst.IsPointerOverUI();

            if (picked.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Movable")))
            {
                goodToPlace = false;
            }

            if (goodToPlace)
            {
                picked.GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                picked.GetComponent<SpriteRenderer>().color = Color.red;
            }

            if (Input.GetMouseButtonUp(0))
            {
                picked.GetComponent<SpriteRenderer>().color = Color.white;
                if (goodToPlace)
                {
                    picked.GetComponent<TrailRenderer>().enabled = true;
                    picked = null;
                    updateCurrObject();
                }
                else
                {
                    FreePlanets.Push(picked);
                    HideCurrPlanet();
                    picked = null;
                    updateCurrObject();
                }
            }
        }
    }
}
