using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public struct VehicleData
{
    public string name;
    public float mass;
    public GameObject vehiclePrefab;
    public Vector2 vehiclePosition;
}

public class VehicleOptionData : OptionData
{
    [SerializeField] List<VehicleData> vehicles;

    GameObject currVehicle;

    protected override void Start()
    {
        base.Start();
    }


    protected override void OnValueChanged(float value)
    {
        if (vehicles.Count <= (int)Mathf.Round(value)) return;

        if(currVehicle != null)
        {
            Destroy(currVehicle);
        }

        VehicleData selected = vehicles[(int)Mathf.Round(value)];

        currVehicle = Instantiate(selected.vehiclePrefab, selected.vehiclePosition, Quaternion.identity);

        GameManager.inst.StopObjectSimulation(currVehicle);

        GameManager.inst.SimulationObjects[GameManager.inst.SimulationObjects.Count - 1] = currVehicle;

        OptionsManager.inst.InsertOption(OptionName, Mathf.RoundToInt(selected.mass));

        foreach (var i in transform.parent.GetComponentsInChildren<OptionData>())
        {
            if (i == this) continue;
            i.analyzedObject = currVehicle.transform.GetChild(0).GetComponent<Rigidbody2D>();
            i.OnUpdated();
        }

        text.text = string.Format("{0}: {1}({2}kg)", OptionName, selected.name, Mathf.RoundToInt(selected.mass));
    }

    protected override void UpdateDisplay(float value) { }
}
