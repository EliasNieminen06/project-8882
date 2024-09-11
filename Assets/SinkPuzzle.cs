using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkPuzzle : MonoBehaviour
{
    public int number;
    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.sinks.Add();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Interact.instance.lastObj.GetComponent<ObjectInfo>().winDoor && Interact.instance.lastObj != null && Interact.instance.lastObj.GetComponent<ObjectInfo>().isDoor && !Interact.instance.lastObj.GetComponent<ObjectInfo>().locked)
            {
                Interact.instance.lastObj.GetComponent<ToggleDoor>().toggleDoor();
            }
            else if (!Interact.instance.lastObj.GetComponent<ObjectInfo>().winDoor && Interact.instance.lastObj != null && Interact.instance.lastObj.GetComponent<ObjectInfo>().locked)
            {
                if (GetComponent<Hand>().heldObj != null && GetComponent<Hand>().heldObj.GetComponent<ObjectInfo>().objectID == Interact.instance.lastObj.GetComponent<ObjectInfo>().regKeyID)
                {
                    Interact.instance.lastObj.GetComponent<ObjectInfo>().locked = false;
                    Destroy(GetComponent<Hand>().heldObj);
                }
            }
            else if (Interact.instance.lastObj != null && Interact.instance.lastObj.GetComponent<ObjectInfo>().winDoor)
            {
                GameManager.instance.WinGame();
            }
        }
    }
}
