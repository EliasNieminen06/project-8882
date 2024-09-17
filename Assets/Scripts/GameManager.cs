using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class Sink
{
    public GameObject sinkObj;
    public int number;
    public bool isOn;
    public bool shouldBeOn;

    public Sink(GameObject ssinkObj, int snumber, bool sisOn, bool sshouldBeOn)
    {
        sinkObj = ssinkObj;
        number = snumber;
        isOn = sisOn;
        shouldBeOn = sshouldBeOn;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevel = 0;
    public GameObject pauseMenu;
    public GameObject gameUI;
    public GameObject dialogUI;
    public List<string> dialogs = new List<string>();
    public bool isPuased;
    public GameObject player;
    public GameObject playerCamera;
    public bool isInGame;
    public int batteries = 0;
    public float flashlightBatteryLevel;
    public float maxFlashlightBatteryLevel;

    public List<Sink> sinks = new List<Sink>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        isInGame = false;
        gameUI.SetActive(false);
        player.SetActive(false);
        flashlightBatteryLevel = maxFlashlightBatteryLevel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isInGame)
        {
            TogglePause();
        }
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        if (playerCamera == null)
        {
            playerCamera = GameObject.Find("Player Camera");
        }
        if (Flashlight.instance != null && Flashlight.instance.isOn)
        {
            drainFlashlightPower();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextDialog();
        }
    }

    public void TogglePause()
    {
        isPuased = !isPuased;
        if (isPuased)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            player.GetComponent<Movement>().enabled = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            player.GetComponent<Movement>().enabled = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        TogglePause();
        isInGame = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        isInGame = false;
        gameUI.SetActive(false);
    }

    public void ChangeLevel(int level)
    {
        LoadScene(level);
    }

    private void LoadScene(int level)
    {
        SceneManager.LoadScene("Level" + level);
        isInGame = true;
        gameUI.SetActive(true);
        player.SetActive(true);
        currentLevel = level;
        Interact.instance.lastObj = null;
        Interact.instance.lookingAtObj = false;
    }

    public void Respawn()
    {
        SceneManager.LoadScene(currentLevel);
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

    public void startDialogue()
    {
        dialogUI.SetActive(true);
        dialogUI.GetComponentInChildren<TextMeshProUGUI>().text = dialogs[0];
    }

    public void nextDialog()
    {
        if (dialogs.Count > 1)
        {
            dialogs.RemoveAt(0);
            dialogUI.GetComponentInChildren<TextMeshProUGUI>().text = dialogs[0];
        }
        else
        {
            dialogUI.SetActive(false);
            dialogs.Clear();
        }
    }

    public void WinGame()
    {
        isInGame = false;
        player.SetActive(false);
        gameUI.SetActive(false);
        SceneManager.LoadScene("EndScreen");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CheckSinks()
    {
        bool sinksAreValid = ValidateSinks();

        if (sinksAreValid)
        {
            ChangeLevel(currentLevel + 1);
        }
    }

    private bool ValidateSinks()
    {
        foreach (Sink sink in sinks)
        {
            if (sink.shouldBeOn && !sink.isOn) return false;
            if (!sink.shouldBeOn && sink.isOn) return false;
        }
        return true;
    }
}
