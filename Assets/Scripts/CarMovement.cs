using UnityEngine;

public class CarMovement : MonoBehaviour
{
    Rigidbody2D rigid;

    Vector2 currSpeed;

    bool hit;

    private void Start()
    {
        currSpeed = Vector2.zero;
        hit = false;
        rigid = GetComponent<Rigidbody2D>();
    }

    public void ChangeSpeed(Vector2 speed)
    {
        currSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "box")
        {
            hit = true;
        }
    }

    void Update()
    {
        if (GameManager.inst.simPlaying)
        {
            if (!hit)
            {
                rigid.linearVelocity = currSpeed;
            }
            else
            {
                rigid.linearVelocity = new Vector2(Mathf.Max(0, rigid.linearVelocity.x), rigid.linearVelocity.y);
            }
        }
        else
        {
            hit = false;
        }
    }
}
