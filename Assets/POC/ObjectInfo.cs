using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    public string interactionButton;
    public string objectName;
    public string objectInfo;
    public bool interactable = true;

    [Header("Item")]
    public bool pickable = false;

    [Header("Door")]
    public bool isDoor = false;
    public GameObject parent;
    [Range(0, 180)] public float openAngle;
    public bool locked = false;
    public bool opened = false;
}
