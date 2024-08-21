using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int levelOfInsanity = 0;
    public int currentCheckpoint = 0;
    public GameObject pauseMenu;
    public bool isPuased;
    public GameObject player;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPuased = !isPuased;
        if (isPuased)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            player.GetComponent<Movement>().enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            player.GetComponent<Movement>().enabled = true;
        }
    }
}
