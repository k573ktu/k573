using UnityEngine;

public class SimulationStart : MonoBehaviour
{
    public virtual void OnSimulationStart() { }
    public virtual void OnSimulationStop() { }
    public virtual void OnSimulationPause() { }
    public virtual void OnSimulationUnpause() { }
}
