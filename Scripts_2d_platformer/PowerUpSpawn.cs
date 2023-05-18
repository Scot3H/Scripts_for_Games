using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{

    [SerializeField] private GameObject[] PowerUp = null;
    private GameManager GM;
    [SerializeField] private GameObject Bug = null;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    public void startSpawn()
    {
        Instantiate(PowerUp[UnityEngine.Random.Range(0, PowerUp.Length)], new Vector3(-6f, 0f, 0f), Quaternion.identity);
        Instantiate(PowerUp[UnityEngine.Random.Range(0, PowerUp.Length)], new Vector3(3.5f, 0f, 0f), Quaternion.identity);
        Instantiate(Bug, new Vector3(0.12f, 8.93f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
