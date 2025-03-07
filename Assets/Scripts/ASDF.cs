using UnityEngine;

public class ASDF : MonoBehaviour
{
    [SerializeField] Rigidbody2D planetA; // First planet
    [SerializeField] Rigidbody2D planetB; // Second planet

    void FixedUpdate()
    {
        //Time.timeScale = 0.2f;
        if (planetA != null && planetB != null)
        {
            // Calculate the midpoint based on position
            Vector2 midpoint = (planetA.position + planetB.position) / 2f;

            // Move this object to the calculated midpoint
            //transform.position = midpoint;
            transform.position = new Vector3(midpoint.x, midpoint.y, -10);
            //transform.position.z = -10;
        }
    }
}

