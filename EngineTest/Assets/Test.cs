using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SendRequest();
            Debug.Log("SendRequest type1");
        }

	}
    void SendRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, "100");
        data.Add(2,"3512553高噶");
        PhotonEngine.Peer.OpCustom(1, data, true);
    }
}
