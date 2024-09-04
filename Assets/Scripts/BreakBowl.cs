using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBowl : MonoBehaviour
{
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) && Interact.instance.lastObj == this.gameObject))
        {
            GameManager.instance.ChangeLevel(GameManager.instance.currentLevel + 1);
        }
    }
}
