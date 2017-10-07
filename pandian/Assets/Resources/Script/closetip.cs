using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closetip : MonoBehaviour {
    public GameObject startPanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onCloseTipClick()
    {
        GameObject go=GameObject.FindGameObjectWithTag("tip");
        go.SetActive(false);
        startPanel.SetActive(true);
    }

    public void closeThis()
    {
        this.transform.parent.parent.gameObject.SetActive(false);
    }
    public void closeThis2()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
