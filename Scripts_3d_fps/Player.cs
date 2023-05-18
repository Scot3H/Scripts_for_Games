using ArionDigital;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject muzzleFlash = null;
    [SerializeField] private GameObject muzzleFlash2 = null;
    [SerializeField] private GameObject hitMarker = null;
    [SerializeField] private GameObject hitMarker2 = null;
    [SerializeField] private float speed = 10.0f;
    private float gravity = -9.8f;
    private CharacterController controller = null;
    private UIManager UI = null;
    [SerializeField] private AudioSource shoot = null;
    [SerializeField] private GameObject Gun = null;
    [SerializeField] private GameObject Gun2 = null;
    [SerializeField] private AudioSource shoot2 = null;
    private bool reloading = false;

    private int currentAmmo = 0;
    private int maxAmmo = 50;

    private int Coins = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        currentAmmo = 0;
        UI.updateAmmo(currentAmmo);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Shoot();

        if(UnityEngine.Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if(UnityEngine.Input.GetKey(KeyCode.R) && !reloading)
        {
            currentAmmo = maxAmmo;
            StartCoroutine(reload());
            
        }

        BuyWeapons();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical"));
        Vector3 velocity = direction * speed;
        velocity.y += gravity;
        //Converts local movement to global
        velocity = transform.transform.TransformDirection(velocity);
        controller.Move(velocity * Time.deltaTime);
    }
    
    private void Shoot()
    {
        RaycastHit hitInfo;
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(.5f,.5f,0));  //Gets middle of the game window
        if (Physics.Raycast(rayOrigin, out hitInfo)) { //if the ray is intercepted
            if(hitInfo.collider.tag == "Destructible")
            {
                //Debug.Log("Hover Over Box");
                UI.ChangeColor();
            }
            else if (hitInfo.collider.tag == "Collect")
            {
                UI.ChangeColorCollect();
            }
            else
            {
                UI.RevertColor();
            }
        }

        if (UnityEngine.Input.GetMouseButton(0) && currentAmmo > 0 && !reloading && (Gun.activeSelf || Gun2.activeSelf))
        {
            if (Gun.activeSelf)
            {
                muzzleFlash.SetActive(true);
                if (!shoot.isPlaying)
                {
                    shoot.Play();
                }
            }
            if(Gun2.activeSelf)
            {
                muzzleFlash2.SetActive(true);
                if (!shoot2.isPlaying)
                {
                    shoot2.Play();
                }
            }
            currentAmmo--;
            UI.updateAmmo(currentAmmo);
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.collider.tag == "Destructible")
                {
                    GameObject hit = Instantiate(hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    Destroy(hit, 1.0f);
                    Destructibles d = hitInfo.transform.GetComponent<Destructibles>();
                    if (d.Item_ID == 1)
                    {
                        CrashCrate cc = hitInfo.transform.GetComponent<CrashCrate>();
                        cc.DestroyCrate();
                    } else
                    {
                        ColumnBreakScript col = hitInfo.transform.GetComponent<ColumnBreakScript>();
                        col.BreakColumn();
                    }

                }
                else
                {
                    GameObject hit = Instantiate(hitMarker2, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    Destroy(hit, 1.0f);
                }
            }
        }
        else
        {
            muzzleFlash.SetActive(false);
            muzzleFlash2.SetActive(false);
            shoot.Stop();
            shoot2.Stop();
        }
    }

    public void StartReload()
    {
        currentAmmo = maxAmmo;
        StartCoroutine(reload());
    }
    private IEnumerator reload()
    {
        reloading= true;
        UI.updateAmmo(currentAmmo);
        yield return new WaitForSeconds(0.5f);
        reloading = false;
    }

    public void addCoin()
    {
        Coins++;
        UI.updateCoins(Coins);
    }

    private void BuyWeapons()
    {
        if (UnityEngine.Input.GetKey(KeyCode.Tab))
        {
            UI.Shop.SetActive(true);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1) && Coins >= 1 && !Gun.activeSelf)
            {
                Coins--;
                UI.updateCoins(Coins);
                Gun.SetActive(true);
                Gun2.SetActive(false);
                currentAmmo = maxAmmo;
                UI.updateAmmo(currentAmmo);
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2) && Coins >= 2 && !Gun2.activeSelf)
            {
                Coins-=2;
                UI.updateCoins(Coins);
                Gun2.SetActive(true);
                Gun.SetActive(false);
                maxAmmo *= 2;
                currentAmmo = maxAmmo;
                UI.updateAmmo(currentAmmo);
            }
        }
        else
        {
            UI.Shop.SetActive(false);
        }
    }

}
