using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    //Instance fields
    private Rigidbody2D rb2D;
    private float speed = 10f;
    private float height = 13f;
    private float jumps = 0f;
    private float max_jumps = 2f;
    private float fireRate = 0.2f; //Delay
    private float canFire = 0.05f; //Timer
    private float fireRateAlt = 0.4f; //Delay
    private float canFireAlt = 1f; //Timer
    private bool direction = true;
    private bool LookingUp = false;
    private bool hasTriple = false;
    [SerializeField] private GameObject LaserPrefab = null;
    [SerializeField] private GameObject LaserPrefabAlt = null;
    [SerializeField] private GameObject TriplePrefab = null;
     [SerializeField] private GameObject TriplePrefabAlt = null;
    [SerializeField] private float powerUpTimer = 0f;
    [SerializeField] private int health = 3;
    [SerializeField] private GameObject Explosion1 = null;
    [SerializeField] private GameObject Explosion2 = null;
    [SerializeField] private GameObject Shield = null;
    private bool hasShield=false;
    private UIManager UI = null;
    private GameManager GM;
    [SerializeField] private GameObject[] Hurt = null;
    [SerializeField] private AudioClip Laser = null;



    // Start is called before the first frame update
    void Start() {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        //Centers player
        transform.position = new Vector3(0, 0, 0);
        Debug.Log("Location: " + transform.position);
        rb2D= gameObject.GetComponent<Rigidbody2D>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        Move();
        Shoot();
    }

    private void Move() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // -1 to 1 (-1 for left 1 for right)
        //Checks number of jumps available to allow for double jumps
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0) {
            //Add velocity to rigidbody
            rb2D.velocity = new Vector2(rb2D.velocity.x, height);
            //Decrement jumps
            jumps -= 1;
        }

        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            direction = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
            direction= false;
        }

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            LookingUp= true;
        }
        else {
            LookingUp= false;
        }
        rb2D.velocity=new Vector2(horizontalInput * speed, rb2D.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
            jumps = max_jumps;
        }
    }

    private void Shoot() {
        if (!GM.paused)
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetMouseButtonDown(0))
            {
                if (Time.time > canFire)
                {
                    if (hasTriple)
                    {
                        GameObject triple = Instantiate(TriplePrefab, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                        triple.GetComponent<Laser>().direction = direction;
                        triple.GetComponent<Laser>().lookingUp = LookingUp;
                    }
                    else
                    {
                        GameObject clone = Instantiate(LaserPrefab, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                        clone.GetComponent<Laser>().direction = direction;
                        clone.GetComponent<Laser>().lookingUp = LookingUp;
                    }
                    //Cloning prefab, Sets position to player, And prevents rotation
                    canFire = Time.time + fireRate;
                    AudioSource.PlayClipAtPoint(Laser, Camera.main.transform.position);
                }
            }

            if (Input.GetKey(KeyCode.X) || Input.GetMouseButtonDown(1))
            {
                if (Time.time > canFireAlt)
                {
                    if (hasTriple)
                    {
                        GameObject clone = Instantiate(TriplePrefabAlt, transform.position, Quaternion.identity);
                        clone.GetComponent<Laser>().direction = direction;
                        clone.GetComponent<Laser>().lookingUp = LookingUp;
                    }
                    else
                    {
                        GameObject clone = Instantiate(LaserPrefabAlt, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                        clone.GetComponent<Laser>().direction = direction;
                        clone.GetComponent<Laser>().lookingUp = LookingUp;
                    }
                    //Cloning prefab, Sets position to player, And prevents rotation
                    canFireAlt = Time.time + fireRateAlt;
                    AudioSource.PlayClipAtPoint(Laser, Camera.main.transform.position);
                }
            }
        }
    }

    public IEnumerator TripleShotPowerDown()
    {
        while (powerUpTimer > 0f)
        {
            yield return new WaitForSeconds(1f);
            powerUpTimer-=1f;
        }
        hasTriple = false;
    }

    public void SetTripleShot(bool value)
    {
        if(hasTriple)
        {
            powerUpTimer += 5f;
        }
        else
        {
            hasTriple = value;
            powerUpTimer += 5f;
            StartCoroutine(TripleShotPowerDown());
        }
    }

    public void speedUp()
    {
        speed *= 1.5f;
        max_jumps += 1;
        StartCoroutine(speedDown());
    }

    public IEnumerator speedDown()
    {
        yield return new WaitForSeconds(5f);
        speed /= 1.5f;
        max_jumps -= 1;

    }


    public void antiSpeed()
    {
        speed /= 2;
        max_jumps -= 1;
        StartCoroutine(anitSpeedDown());
    }

    public IEnumerator anitSpeedDown()
    {
        yield return new WaitForSeconds(5f);
        speed *= 2;
        max_jumps += 1;
    }

    public void Damage()
    {
        if (hasShield)
        {
            Shield.SetActive(false);
            hasShield= false;
            max_jumps-= 1;
        }
        else
        {
            health--;
            int HurtRandom = Random.Range(0, Hurt.Length);
            Hurt[HurtRandom].SetActive(true);
            StartCoroutine(turnOffHurt(HurtRandom));
            UI.UpdateLives(health);
            if (health <= 0)
            {
                float random = Random.Range(0f, 10f);
                if (random < 5f)
                {
                    Instantiate(Explosion1, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Explosion2, transform.position, Quaternion.identity);
                }
                UI.ShowTitleScreen();
                GM.gameOver = true;
                GM.GameOver();
                this.gameObject.SetActive(false);
            }
        }
    }

    public void Heal()
    {
        health++;
        UI.UpdateLives(health);
    }
    public void ShieldPowerUpOn()
    {
        Shield.SetActive(true);
        hasShield = true;
        max_jumps++;
    }

    public void resetHealth()
    {
        health = 3;
        hasShield= false;
        transform.position = new Vector3(0, 0, 0);
    }

    public IEnumerator turnOffHurt(int id)
    {
        yield return new WaitForSeconds(1f);
        Hurt[id].SetActive(false);
    }

}