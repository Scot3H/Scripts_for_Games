using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RawImage CrossHair;
    [SerializeField] private Text Ammo;
    [SerializeField] private Text Coins;
    [SerializeField] public GameObject Shop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        CrossHair.color = Color.red;
    }

    public void ChangeColorCollect()
    {
        CrossHair.color = Color.blue;
    }

    public void RevertColor()
    {
        CrossHair.color = Color.white;
    }

    public void updateAmmo(int count)
    {
        Ammo.text = "Ammo: "+count;
    }

    public void updateCoins(int count)
    {
        Coins.text = "Coins: " + count;
    }

}
