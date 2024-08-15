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
        if (Interact.instance.lookingAtObj)
        {
            header.text = "[ " + Interact.instance.lastObj.GetComponent<ObjectInfo>().interactionButton + " ] " + Interact.instance.lastObj.GetComponent<ObjectInfo>().objectName;
            info.text = Interact.instance.lastObj.GetComponent<ObjectInfo>().objectInfo;
        }
        else
        {
            header.text = "";
            info.text = "";
        }
    }
}
