using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject cam;
    public LayerMask interactMask;
    public Material outlineMaterial;
    private GameObject lastObj;

    private void Update()
    {
        Vector3 startPos = cam.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(startPos, cam.transform.TransformDirection(Vector3.forward), out hit, 5, interactMask))
        {
            Debug.Log("Did Hit");
            lastObj = hit.collider.gameObject;
            List<Material> newMaterials = new List<Material>();
            newMaterials.Add(hit.collider.gameObject.GetComponent<MeshRenderer>().material);
            newMaterials.Add(outlineMaterial);
            Material[] newMaterialsArr = newMaterials.ToArray();
            hit.collider.gameObject.GetComponent<MeshRenderer>().materials = newMaterialsArr;
        }
        else
        {
            if (lastObj)
            {
                List<Material> newMaterials = hit.collider.gameObject.GetComponent<MeshRenderer>().materials.ToList();
                newMaterials.RemoveAt(1);
                Material[] newMaterialsArr = newMaterials.ToArray();
                hit.collider.gameObject.GetComponent<MeshRenderer>().materials = newMaterialsArr;
                lastObj = null;
            }
        }
    }
}
