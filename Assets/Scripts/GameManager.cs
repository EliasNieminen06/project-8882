using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
        if (player != null)
        {
            Vector3 lastPos = player.transform.position;
            float lastPlayerRotation = player.transform.rotation.y;
            float lastCamAngle = playerCamera.transform.rotation.x;

            LoadSceneWithPos(lastCamAngle, lastPlayerRotation, lastPos, level);
        }
        else
        {
            LoadScene(level);
        }
    }

    private void LoadScene(int level)
    {
        SceneManager.LoadScene("Level" + level);
        isInGame = true;
        gameUI.SetActive(true);
        currentLevel = level;
        Debug.Log("s");
    }

    IEnumerator LoadSceneWithPos(float lastCamAngle, float lastPlayerRotation, Vector3 lastPos, int level)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync("Level" + level);

        while (!loadAsync.isDone)
        {
            yield return null;
        }

        playerCamera.transform.Rotate(lastCamAngle, 0, 0);
        player.transform.Rotate(0, lastPlayerRotation, 0);
        player.transform.position = lastPos;
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
}
