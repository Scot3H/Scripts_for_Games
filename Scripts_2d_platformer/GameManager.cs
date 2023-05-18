using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public bool startGame = true;
    [SerializeField] private GameObject Player = null;
    [SerializeField] private GameObject Platforms = null;
    public bool paused = false;

    UIManager UI = null;
    GhostSpawn GS = null;
    PowerUpSpawn PUS = null;
    Player P = null;
    // Start is called before the first frame update
    void Start()
    {
        Platforms.SetActive(false);
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        GS= GameObject.Find("GhostSpawn").GetComponent<GhostSpawn>();
        PUS = GameObject.Find("PowerUpSpawn").GetComponent<PowerUpSpawn>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (paused)
            {
                Time.timeScale = 1f;
                paused = false;
                UI.HidePauseScreen();
            }
            else
            {
                Time.timeScale = 0f;
                paused = true;
                UI.ShowPauseScreen();
            }
        }

        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Player.SetActive(true);
            P = GameObject.Find("Player").GetComponent<Player>();
            P.resetHealth();
            UI.HideTitleScreen();
            gameOver = false;
            if (startGame)
            {
                GS.StartSpawn();
                startGame= false;
            }
            PUS.startSpawn();
            Platforms.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOver= true;
        Platforms.SetActive(false);
    }
}
