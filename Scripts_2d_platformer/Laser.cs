using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Instance fields
    [SerializeField] private float speed = 20.0f;
    public bool direction;
    public bool lookingUp;
    public float startingY;
    public float startingX;
    [SerializeField] private float damage = 0f;


    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
        startingX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (transform.position.y <= startingY - 6f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (transform.position.y >= startingY+10f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {

            if (direction)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }

            if (transform.position.x <= startingX - 20f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (transform.position.x >= startingX+20f)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        } else if (collision.gameObject.tag == "Enemy")
        {
            Enemy E = collision.GetComponent<Enemy>();
            E.Damage(damage);
            Destroy(gameObject);
        }
    }
}
