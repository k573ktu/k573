using System.Collections.Generic;
using TMPro;
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
    public bool blackHole;
}

public class PlanetOptionData : OptionData
{
    [SerializeField] List<PlanetData> SpaceObjects;

    [SerializeField] Material BlackHoleMaterial;
    Material PlanetMaterial;

    [SerializeField] Image planetImageUi;

    [SerializeField] TextMeshProUGUI CountText;

    int currObject;

    [SerializeField] List<GameObject> planetObjects;

    [SerializeField] Sprite thrashSprite;

    Stack<GameObject> FreePlanets;

    GameObject picked;
    GameObject currSpot;

    [SerializeField] List <GameObject> PlanetSpots;
    List<GameObject> spotTaken;
    [SerializeField] float distToSpot;

    protected override void Start()
    {
        base.Start();
        currObject = 0;
        
        PlanetMaterial = new Material(Shader.Find("Sprites/Default"));

        FreePlanets = new Stack<GameObject>();

        spotTaken = new List<GameObject>();

        foreach(var i in PlanetSpots)
        {
            spotTaken.Add(null);
            i.SetActive(false);
        }

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
            planetImageUi.color = Color.white;
            planetImageUi.sprite = thrashSprite;
        }
        else
        {
            if (SpaceObjects[currObject].blackHole)
            {
                planetImageUi.color = Color.black;
            }
            else
            {
                planetImageUi.color = Color.white;
            }

            planetImageUi.sprite = SpaceObjects[currObject].planetSprite;
        }
        text.text = SpaceObjects[currObject].name;
        CountText.text = string.Format("Planetos: {0}/2",planetObjects.Count-FreePlanets.Count);
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

        if (SpaceObjects[currObject].blackHole)
        {
            picked.GetComponent<SpriteRenderer>().color = Color.black;

            picked.GetComponent<SpriteRenderer>().material = BlackHoleMaterial;
            picked.GetComponent<ShadowCaster2D>().enabled = false;
        }
        else
        {
            picked.GetComponent<SpriteRenderer>().color = Color.white;

            picked.GetComponent<SpriteRenderer>().material = PlanetMaterial;
            picked.GetComponent<ShadowCaster2D>().enabled = true;
            picked.GetComponent<ShadowCaster2D>().Update();
        }

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
        for (int i = 0; i < PlanetSpots.Count; i++)
        {
            if (spotTaken[i] == null)
            {
                PlanetSpots[i].SetActive(true);
            }
        }

        updateCurrObject();
    }

    void PickExisting(GameObject obj)
    {
        if (picked != null) return;
        FreePlanets.Push(obj);
        spotTaken[spotTaken.IndexOf(obj)]=null;
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

            var spot = Physics2D.OverlapCircle(mouse, distToSpot, LayerMask.GetMask("PlanetSpot"));

            if (spot)
            {
                picked.transform.position = new Vector3(spot.gameObject.transform.position.x, spot.gameObject.transform.position.y, picked.transform.position.z);
                currSpot = spot.gameObject;
            }
            else
            {
                goodToPlace = false;
            }

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
                    
                    int id = PlanetSpots.IndexOf(currSpot);
                    spotTaken[id] = picked;
                    picked = null;
                    currSpot = null;
                    updateCurrObject();
                }
                else
                {
                    FreePlanets.Push(picked);
                    HideCurrPlanet();
                    picked = null;
                    currSpot = null;
                    updateCurrObject();
                }
                for (int i = 0; i < PlanetSpots.Count; i++)
                {
                    PlanetSpots[i].SetActive(false);
                }
            }
        }
    }
}
