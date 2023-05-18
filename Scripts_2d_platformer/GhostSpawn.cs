using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private GameObject Ghost = null;
    private float Playerx;
    private float Playery;

    private GameManager GM;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartSpawn()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(EnemySpawn());
    }


    // Update is called once per frame
    void Update()
    {
        if (!GM.gameOver)
        {
            Playerx = Player.transform.position.x;
            Playery = Player.transform.position.y;
        }
    }

    private IEnumerator EnemySpawn()
    {
        if (!GM.gameOver)
        {
            yield return new WaitForSeconds(Random.Range(4f, 10f));
            Instantiate(Ghost, new Vector3(Playerx + Random.Range(-10f, 10f), Playery + 10f, 0), Quaternion.identity);
            StartCoroutine(EnemySpawn());
        } else
        {

        }
    }
}
