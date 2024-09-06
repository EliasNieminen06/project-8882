using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Hand.instance.heldObj != null && Hand.instance.heldObj.GetComponent<ObjectInfo>().objectID.StartsWith("riddleObj"))
        {
            if (Interact.instance.lastObj != null && Interact.instance.lastObj.GetComponent<ObjectInfo>().isDialogue && Interact.instance.lastObj.GetComponent<ObjectInfo>().riddleAnswer == Hand.instance.heldObj.GetComponent<ObjectInfo>().objectID)
            {
                GameManager.instance.dialogs.Add("Correct! You got it.");
                GameManager.instance.startDialogue();
                GameManager.instance.ChangeLevel(GameManager.instance.currentLevel + 1);
                Debug.Log("SUS");
            }
            else if (Interact.instance.lastObj != null && Interact.instance.lastObj.GetComponent<ObjectInfo>().isDialogue)
            {
                GameManager.instance.dialogs.Add("Not right...");
                GameManager.instance.startDialogue();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && Interact.instance.lastObj != null && Interact.instance.lastObj.GetComponent<ObjectInfo>().isDialogue)
            {
                GameManager.instance.dialogs.Clear();
                for (int i = 0; i < Interact.instance.lastObj.GetComponent<ObjectInfo>().dialogues.Count; i++)
                {
                    GameManager.instance.dialogs.Add(Interact.instance.lastObj.GetComponent<ObjectInfo>().dialogues[i]);
                }
                GameManager.instance.startDialogue();
            }
        }
    }
}
