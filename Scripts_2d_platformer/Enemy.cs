using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float MoveSpeed = 5f;
    public Transform target;
    private Rigidbody2D rb2D;
    [SerializeField] private float health = 1f;
    [SerializeField] private float enemyID = 1f;
    [SerializeField] private GameObject NormalExplosion= null;
    [SerializeField] private GameObject BigExplosion = null;
    [SerializeField] private int Score = 0;
    private UIManager UI = null;
    private GameManager GM;
    [SerializeField] private AudioClip Exploison = null;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!GM.gameOver)
        {
            UI = GameObject.Find("Canvas").GetComponent<UIManager>();
            if (enemyID == 1f)
            {
                target = GameObject.Find("Player").transform;
            }
            else if (enemyID == 2f)
            {
                float wait = Random.Range(2f, 10f);
                StartCoroutine(WaitToJump(wait));
            }
            rb2D = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyID == 1f)
        {
            if (target)
            {
                //transform.LookAt(target);
                Vector3 direction = (target.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb2D.rotation = angle;
                rb2D.velocity = new Vector2(direction.x, direction.y) * MoveSpeed;
            }
        } else if(enemyID == 2f)
        {
            float wait = Random.Range(2f, 10f);
        }

        if (GM.gameOver)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            if (health == 0)
            {
                Instantiate(NormalExplosion, transform.position, Quaternion.identity);
            } else
            {

                Instantiate(BigExplosion, transform.position, Quaternion.identity);
            }
            UI.UpdateScore(Score);
            AudioSource.PlayClipAtPoint(Exploison, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player P = collision.GetComponent<Player>();
            Instantiate(NormalExplosion, transform.position, Quaternion.identity);
            P.Damage();
            UI.UpdateScore(Score);
            AudioSource.PlayClipAtPoint(Exploison, Camera.main.transform.position);
            Destroy(gameObject);

        }
    }

    private IEnumerator WaitToJump(float time)
    {
            float newWait = Random.Range(2f, 10f);
            yield return new WaitForSeconds(time);
            rb2D.velocity = new Vector2(0f, 5f);
            StartCoroutine(WaitToJump(newWait));
    }
}
