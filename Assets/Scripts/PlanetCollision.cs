using UnityEngine;

public class PlanetCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "planet")
        {
            GameObject toDisable = (collision.GetComponent<Rigidbody2D>().mass > GetComponent<Rigidbody2D>().mass)?gameObject:collision.gameObject;

            Debug.Log("Dead");
        }
    }
}
