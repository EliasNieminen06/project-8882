using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int levelOfInsanity = 0;
    public int currentCheckpoint = 0;

    private void Awake()
    {
        instance = this;
    }
}
