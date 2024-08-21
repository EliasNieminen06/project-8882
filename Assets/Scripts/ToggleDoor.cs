using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoor : MonoBehaviour
{
    public void toggleDoor()
    {
        if (Interact.instance.lastObj.GetComponent<ObjectInfo>().opened != true)
        {
            Interact.instance.lastObj.GetComponent<ObjectInfo>().opened = true;
            transform.Rotate(0f, Interact.instance.lastObj.GetComponent<ObjectInfo>().openAngle, 0f, Space.Self);
        }
        else
        {
            Interact.instance.lastObj.GetComponent<ObjectInfo>().opened = false;
            transform.Rotate(0f, -Interact.instance.lastObj.GetComponent<ObjectInfo>().openAngle, 0f, Space.Self);
        }
    }
}
