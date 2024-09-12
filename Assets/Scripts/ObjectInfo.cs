using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    public string objectID;
    public string interactionButton;
    public string objectName;
    public string objectInfo;
    public bool interactable = true;

    [Header("Item")]
    public bool pickable = false;

    [Header("Door")]
    public string regKeyID;
    public bool isDoor = false;
    public bool winDoor = false;
    [Range(-180, 180)] public float openAngle;
    public bool locked = false;
    public bool opened = false;

    [Header("Dialogue")]
    public bool isDialogue;
    public List<string> dialogues = new List<string>();
    public string riddleAnswer;

    [Header("Sink")]
    public bool isSink;
}
