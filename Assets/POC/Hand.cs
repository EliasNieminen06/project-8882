using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand instance;

    public Transform holdPos;
    public GameObject heldObj;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(heldObj.gameObject);
        }
    }

    public void HoldInHand(GameObject obj)
    {
        heldObj = Instantiate(obj, holdPos.transform.position, holdPos.transform.rotation, holdPos);
        heldObj.GetComponent<ObjectInfo>().interactable = false;

        List<Material> newMaterials = heldObj.GetComponent<MeshRenderer>().materials.ToList();
        newMaterials.RemoveAt(newMaterials.Count - 1);
        Material[] newMaterialsArr = newMaterials.ToArray();
        heldObj.GetComponent<MeshRenderer>().materials = newMaterialsArr;
    }
}
