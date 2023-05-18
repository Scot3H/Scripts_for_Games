using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            p.StartReload();
            AudioSource.PlayClipAtPoint(PickupFX, Camera.main.transform.position, 1f);
            Destroy(gameObject);
        }
        }
    }
}
