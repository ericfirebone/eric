using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
public class PhotonEngine : MonoBehaviour,IPhotonPeerListener {
    public static PhotonPeer Peer {
        get {

            return peer;
        } 

    }
    public static PhotonEngine Instance {

        get {

            return _instance;
        }
    }
    private static PhotonEngine _instance;
    private static PhotonPeer peer;
    public void DebugReturn(DebugLevel level, string message)
    {
      
    }

    public void OnEvent(EventData eventData)
    {
        switch (eventData.Code)
        {
            case 1:
  
                Dictionary<byte, object> data2 = eventData.Parameters;
                object intvalue, stringvalue;
                data2.TryGetValue(1, out intvalue);
                data2.TryGetValue(2, out stringvalue);
                Debug.Log(intvalue.ToString() + "," + stringvalue.ToString());
                break;

        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        switch (operationResponse.OperationCode)
        {
            case 1:
                Debug.Log("收到服务器端的响应,opcode:1");
                Dictionary <byte,object> data2= operationResponse.Parameters;
                object intvalue,stringvalue;
                data2.TryGetValue(1,out intvalue);
                data2.TryGetValue(2, out stringvalue);
                Debug.Log(intvalue.ToString()+","+stringvalue.ToString());
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
      
    }

    void Awake()
    {
        //if (_instance == null)
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else if (_instance != this)
        //{
        //    Destroy(this.gameObject); return;
        //}
        _instance = this;
    }
    // Use this for initialization
    void Start () {
         peer = new PhotonPeer(this,ConnectionProtocol.Udp);
         peer.Connect("127.0.0.1:5055","MyGame1");
	}
	
	// Update is called once per frame
	void Update () {
       
            peer.Service();
       
   
	}
    void OnDestory()
    {
        if (peer!=null&&peer.PeerState==PeerStateValue.Connected)
        {
            peer.Disconnect();
        }

    }
}
