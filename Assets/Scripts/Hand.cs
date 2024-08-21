using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand instance;

    public Transform holdPos;
    public GameObject heldObj;
    [Range(1, 5)] public float throwForce;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldObj != null)
            {
                ThrowFromHand();
            }
        }
    }

    public void ThrowFromHand()
    {
        if (heldObj.GetComponent<Rigidbody>())
        {
            heldObj.GetComponent<Rigidbody>().isKinematic = false;
            heldObj.GetComponent<Rigidbody>().AddForce(this.transform.forward * throwForce, ForceMode.Impulse);
        }
        heldObj.transform.SetParent(null);
        heldObj.GetComponent<ObjectInfo>().interactable = true;
        heldObj = null;
    }

    public void HoldInHand(GameObject obj)
    {
        heldObj = obj;
        heldObj.transform.SetParent(holdPos, true);
        heldObj.transform.position = holdPos.transform.position;
        heldObj.transform.rotation = holdPos.transform.rotation;
        heldObj.GetComponent<ObjectInfo>().interactable = false;
        if (heldObj.GetComponent<Rigidbody>())
        {
            heldObj.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
