using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevel = 0;
    public GameObject pauseMenu;
    public GameObject gameUI;
    public bool isPuased;
    public GameObject player;
    public bool isInGame;
    public int batteries = 0;
    public float flashlightBatteryLevel;
    public float maxFlashlightBatteryLevel;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        isInGame = false;
        gameUI.SetActive(false);
        flashlightBatteryLevel = maxFlashlightBatteryLevel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isInGame)
        {
            TogglePause();
        }
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        if (Flashlight.instance != null && Flashlight.instance.isOn)
        {
            drainFlashlightPower();
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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        isInGame = false;
        gameUI.SetActive(false);
    }

    public void ChangeLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
        isInGame = true;
        gameUI.SetActive(true);
    }

    public void drainFlashlightPower()
    {
        if (flashlightBatteryLevel > 0)
        {
            flashlightBatteryLevel -= Time.deltaTime;
        }
    }

    public void ReloadFlashlight()
    {
        if (flashlightBatteryLevel < 50 && batteries > 0)
        {
            flashlightBatteryLevel = maxFlashlightBatteryLevel;
            batteries--;
        }
    }
}
