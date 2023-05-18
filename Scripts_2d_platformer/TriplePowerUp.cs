using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriplePowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float power_id = 0;
    private GameManager GM;
    [SerializeField] private AudioClip[] PowerUpSounds = null;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.gameOver)
        {
            Destroy(gameObject);
        }
        Hover();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player P = collision.GetComponent<Player>();

            if (P != null)
            {
                if (power_id == 0)
                {
                    P.SetTripleShot(true);
                    
                }
                else if(power_id == 1) { 
                    P.speedUp();
                } else if(power_id==2) { 
                    P.antiSpeed();
                } else if (power_id == 3)
                {
                    P.Heal();
                } else if (power_id == 4)
                {
                    P.ShieldPowerUpOn();
                }
            }
            int ranNum=Random.Range(0, PowerUpSounds.Length);
            AudioSource.PlayClipAtPoint(PowerUpSounds[ranNum], Camera.main.transform.position, 10f);
            //Debug.Log(Camera.main.transform.position);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Hover()
    {
        Vector3 pos = this.transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * 5f);
        //set the object's Y to the new calculated Y
        this.transform.position = new Vector3(pos.x, newY*0.1f, pos.z);
    }
}
