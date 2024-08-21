using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Interact.instance.lastObj != null && Interact.instance.lookingAtObj && Interact.instance.lastObj.GetComponent<ObjectInfo>().pickable && Hand.instance.heldObj == null)
            {
                Interact.instance.lookingAtObj = false;
                Hand.instance.HoldInHand(Interact.instance.lastObj);
            }
        }
    }
}
