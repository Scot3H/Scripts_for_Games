using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private AudioClip PickupFX = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Player p = other.GetComponent<Player>();
                if (p != null)
                {
                    p.addCoin();
                    AudioSource.PlayClipAtPoint(PickupFX, Camera.main.transform.position, 1f);
                    Destroy(gameObject);
                }
            }
        }
    }
}
