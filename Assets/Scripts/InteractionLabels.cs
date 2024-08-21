using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionLabels : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI info;
    void Update()
    {
        if (Interact.instance.lastObj != null)
        {
            header.text = Interact.instance.lastObj.GetComponent<ObjectInfo>().interactionButton + " " + Interact.instance.lastObj.GetComponent<ObjectInfo>().objectName;
            if (Interact.instance.lastObj.GetComponent<ObjectInfo>().isDoor)
            {
                if (Interact.instance.lastObj.GetComponent<ObjectInfo>().locked)
                {
                    info.text = "I need a key to open this door.";
                }
            }
            info.text = Interact.instance.lastObj.GetComponent<ObjectInfo>().objectInfo;
        }
        else
        {
            header.text = "";
            info.text = "";
        }
    }
}
