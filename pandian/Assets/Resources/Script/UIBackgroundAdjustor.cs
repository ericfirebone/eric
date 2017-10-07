using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Awake()

    {

        SetBackgroundSize();

    }
    public float standard_height = 1024f;
    public float standard_width = 576f;
    public void SetBackgroundSize()
    {
        float device_height = Screen.height;
        float device_width = Screen.width;
        if (transform != null)
        {
            float standard_aspect = standard_width / standard_height;
            float device_aspect = device_width / device_height;
            float scale = 0f;
            if (device_aspect > standard_aspect)
            {
                scale = device_aspect / standard_aspect;
                transform.localScale = new Vector3(scale, 1, 1);
            }
            else
            {
                scale = standard_aspect / device_aspect;
                transform.localScale = new Vector3(1, scale, 1);
            }
        }
    }
}
