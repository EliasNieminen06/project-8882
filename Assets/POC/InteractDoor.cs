using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interact.instance.lastObj.GetComponent<ObjectInfo>().isDoor && !Interact.instance.lastObj.GetComponent<ObjectInfo>().locked)
            {
                Interact.instance.lastObj.GetComponent<ObjectInfo>().parent.GetComponent<ToggleDoor>().toggleDoor();
            }
        }
    }
}
