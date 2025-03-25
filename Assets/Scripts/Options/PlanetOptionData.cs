using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    [SerializeField] GameObject analyzedObject;

    [SerializeField] List<PlanetData> planets;

    protected override void Start()
    {
        base.Start();
    }


    protected override void OnValueChanged(float value)
    {
        if (planets.Count <= (int)Mathf.Round(value)) return;

        PlanetData selected = planets[(int)Mathf.Round(value)];

        analyzedObject.GetComponent<Rigidbody2D>().mass = selected.mass;

        analyzedObject.transform.localScale = new Vector3(selected.radius, selected.radius, analyzedObject.transform.localScale.z);

        analyzedObject.GetComponent<SpriteRenderer>().sprite = selected.planetSprite;

        analyzedObject.GetComponent<ShadowCaster2D>().Update();

        text.text = OptionName + ": " + selected.name;
    }

    protected override void UpdateDisplay(float value) { }
}
