using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public static Interact instance;

    public GameObject cam;
    public LayerMask interactMask;
    public Material outlineMaterial;
    public bool lookingAtObj;
    public GameObject lastObj;
    [Range(0, 5)] public float interactionDistance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Vector3 startPos = cam.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(startPos, cam.transform.TransformDirection(Vector3.forward), out hit, interactionDistance, interactMask) && hit.collider.gameObject.GetComponent<ObjectInfo>().interactable && !lookingAtObj)
        {
            lookingAtObj = true;
            lastObj = hit.collider.gameObject;
            List<Material> newMaterials = new List<Material>();
            for (int i = 0; i < lastObj.GetComponent<MeshRenderer>().materials.Length; i++)
            {
                newMaterials.Add(hit.collider.gameObject.GetComponent<MeshRenderer>().materials[i]);
            }
            newMaterials.Add(outlineMaterial);
            Material[] newMaterialsArr = newMaterials.ToArray();
            hit.collider.gameObject.GetComponent<MeshRenderer>().materials = newMaterialsArr;
        }
        else if (!Physics.Raycast(startPos, cam.transform.TransformDirection(Vector3.forward), out hit, interactionDistance, interactMask))
        {
            if (lastObj)
            {
                lookingAtObj = false;
                List<Material> newMaterials = lastObj.GetComponent<MeshRenderer>().materials.ToList();
                newMaterials.RemoveAt(newMaterials.Count - 1);
                Material[] newMaterialsArr = newMaterials.ToArray();
                lastObj.GetComponent<MeshRenderer>().materials = newMaterialsArr;
                lastObj = null;
            }
        }
    }
}
