using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkPuzzle : MonoBehaviour
{
    public int number;
    public bool isOn;
    public bool shouldBeOn;
    private Sink sink;

    public GameObject water;

    // Start is called before the first frame update
    void Start()
    {
        sink = new Sink(gameObject, number, isOn, shouldBeOn);
        GameManager.instance.sinks.Add(sink);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Interact.instance.lastObj == this.gameObject)
        {
            if (Interact.instance.lastObj != null && Interact.instance.lastObj.GetComponent<ObjectInfo>().isSink)
            {
                Interact.instance.lastObj.GetComponent<SinkPuzzle>().ToggleSink();
                Sink s = GameManager.instance.sinks.Find(sink => sink.number == number);
                s.isOn = isOn;
                GameManager.instance.CheckSinks();
            }
        }
    }

    void ToggleSink()
    {
        if (!isOn)
        {
            isOn = true;
            water.SetActive(true);
        }
        else
        {
            isOn = false;
            water.SetActive(false);
        }
    }
}
