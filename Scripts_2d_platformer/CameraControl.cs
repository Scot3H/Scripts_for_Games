using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject Player;
    private GameManager GM;
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GM.gameOver)
        {
            if (Player.transform.position.y < 0)
            {
                if (Player.transform.position.x > 6.1f || Player.transform.position.x < -6.1f)
                {
                    transform.position = new Vector3(transform.position.x, 0, -10);
                }
                else
                {
                    transform.position = new Vector3(Player.transform.position.x, 0, -10);
                }
            }
            else
            {
                if (Player.transform.position.x > 6.1f || Player.transform.position.x < -6.1f)
                {
                    transform.position = new Vector3(transform.position.x, Player.transform.position.y, -10);
                }
                else
                {
                    transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
                }

            }
        }
    }
}
