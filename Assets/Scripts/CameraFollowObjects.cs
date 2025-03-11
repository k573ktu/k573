using UnityEngine;

public class CameraFollowObjects : MonoBehaviour
{
    [SerializeField] GameObject objA;
    [SerializeField] GameObject objB;

    void FixedUpdate()
    {
        if (objA != null && objB != null)
        {
            Vector2 midpoint = (objA.transform.position + objB.transform.position) / 2f;
            transform.position = new Vector3(midpoint.x, midpoint.y, transform.position.z);
        }
    }
}

