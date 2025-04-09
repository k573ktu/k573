using UnityEngine;

public class StarParallaxUpdater : MonoBehaviour
{
    public Material starMaterial;
    private Vector3 lastCamPos;
    void Start()
    {
        lastCamPos = transform.position;
    }
    void Update()
    {
        if (starMaterial != null)
        {
            starMaterial.SetVector("_CameraPos", transform.position);
        }
    }
}
