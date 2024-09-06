using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public static Flashlight instance;
    public GameObject flashLight;
    public bool isOn;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        flashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.flashlightBatteryLevel > 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioManager.Instance.Play("Flashlight");
                isOn = !isOn;
                if (isOn)
                {
                    flashLight.SetActive(true);
                }
                else
                {
                    flashLight.SetActive(false);
                }
            }
        }
        else
        {
            flashLight.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.ReloadFlashlight();
        }
    }
}
