using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Text;

public class EasyCodeScannerExample : MonoBehaviour {
	
	static string dataStr;
	public Renderer PlaneRender;
    public UIButton btn;
    public UIButton closeBtn;
    public GameObject tip;
    public UILabel resultLb;
    public GameObject search;
    public List<item> itemlist = new List<item>();
    // public string host= "http://10.1.17.15:8080";
    // public string host = "http://10.1.0.66:8081";
    public string host = "http://60.30.201.10:8081";
    void Start () {
		dataStr = "";
        btn.gameObject.SetActive(true);
        btn.isEnabled = true;
        closeBtn.isEnabled = true;
		// Initialize EasyCodeScanner
		EasyCodeScanner.Initialize();
		
		//Register on Actions
		EasyCodeScanner.OnScannerMessage += onScannerMessage;
		EasyCodeScanner.OnScannerEvent += onScannerEvent;
		EasyCodeScanner.OnDecoderMessage += onDecoderMessage;

        //Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    private void Awake()
    {
        dataStr = "";
        btn.gameObject.SetActive(true);
        btn.isEnabled = true;
        closeBtn.isEnabled = true;
    }
    void OnDestroy() {
		
		//Unregister
		EasyCodeScanner.OnScannerMessage -= onScannerMessage;
		EasyCodeScanner.OnScannerEvent -= onScannerEvent;
		EasyCodeScanner.OnDecoderMessage -= onDecoderMessage;
	}
	
	public void Update() {
			
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			Application.Quit();
		}
		
	}

    public void searchCloseBtnClick()
    {
        // GameObject.FindGameObjectWithTag("searchPanel").SetActive(false);
        search.SetActive(false);
    }


        //Callback when returns from the scanner
        void onScannerMessage(string data){
		Debug.Log("EasyCodeScannerExample - onScannerMessage data=:"+data);
		dataStr = data;
        onSearch(dataStr);
      //  string result = executeUpdateAsset(dataStr);



        //  resultLb.text = "已盘点:" + result + "!";
        //Just to show case : get the image and display it on a Plane
        //Texture2D tex = EasyCodeScanner.getScannerImage(200, 200);
        //PlaneRender.material.mainTexture = tex;

        //Just to show case : decode a texture/image - refer to code list
        //EasyCodeScanner.decodeImage(-1, tex);
    }
	
	//Callback which notifies an event
	//param : "EVENT_OPENED", "EVENT_CLOSED"
	void onScannerEvent(string eventStr){
		Debug.Log("EasyCodeScannerExample - onScannerEvent:"+eventStr);
	}
	
	//Callback when decodeImage has decoded the image/texture 
	void onDecoderMessage(string data){
		Debug.Log("EasyCodeScannerExample - onDecoderMessage data:"+data);
		

    }
    public void OnScanBtnClick()
    {
        Debug.Log("scanner click");
        //btn.gameObject.SetActive(false);
        //btn.isEnabled = false;
        EasyCodeScanner.launchScanner(true, "scanning...", -1, true);

    }
    public void OnCloseClick()
    {

        SceneManager.LoadScene("Resources/Scene/pandian");
    }
    /// <summary>
    /// 更新固定资产
    /// </summary>
    /// <param name="FD_CODE"></param>
    /// <returns></returns>
    public string executeUpdateAsset(string FD_CODE,string pandianren,string pandianresult)
    {
     
        string url = host+"/cep/OaController/updateAssetStatus";
        httpmanage hm = new httpmanage();
        try
        {
            hm.DoWebRequestByPost(url, "FD_CODE=" + FD_CODE+"&pandianren="+pandianren+"&pandianresult="+ pandianresult, resultLb);
        }
        catch (Exception e)
        {
            tip.SetActive(true);
            GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
            GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "读取失败，请检查网络";

        }
        
        return FD_CODE;
    }

    public string executeUpdateAsset2(string FD_CODE, string pandianren, string pandianresult)
    {
      
        try
        {
            WWW www = new WWW(host+"/cep/OaController/updateAssetStatus", Encoding.GetEncoding("GB2312").GetBytes("FD_CODE=" + FD_CODE + "&pandianren=" + pandianren + "&pandianresult=" + pandianresult));
        }
        catch (Exception e)
        {
            tip.SetActive(true);
            GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
            GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "读取失败，请检查网络";

        }
        return FD_CODE;
    }
    /// <summary>
    /// 查找某编号的资产详细信息
    /// </summary>
    /// <param name="searchText">编号</param>
    public void onSearch(string searchText)
    {

        search.SetActive(true);

        if (searchText == "")
        {
            tip.SetActive(true);
            GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
            GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "查找编号不能为空";
        }
        else
        {
            string FD_CODE = searchText;
            string url = host+"/cep/OaController/searchAssetCard?FD_CODE=" + FD_CODE;
            httpmanage hm = new httpmanage();
            string json = "";
            try
            {
                json = hm.accessWebUrlPost2(url);
            }
            catch (Exception e)
            {
                tip.SetActive(true);
                GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
                GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "连接服务器异常";
                return;
            }
            if (json.IndexOf("查询失败") != -1 || json == "")
            {
                tip.SetActive(true);
                GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
                GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "查找不到对应编号的资产";

                return;
            }
            itemlist = JsonHelper.DeserializeJsonToList<item>(json);
            search.SetActive(true);
            for (int i = 0; i < itemlist.Count; i++)
            {
                GameObject ipb = GameObject.FindGameObjectWithTag("last");
                item it = itemlist[i] as item;
                string fd_name = it.FD_NAME;
                ipb.transform.Find("FD_NAME_VALUE").GetComponent<UILabel>().text = fd_name;
                string fd_code = it.FD_CODE;
                ipb.transform.Find("FD_CODE_VALUE").GetComponent<UILabel>().text = fd_code;
                string fd_standard = it.FD_STANDARD;
                ipb.transform.Find("FD_STANDARD_VALUE").GetComponent<UILabel>().text = fd_standard;
                string fd_measure = it.FD_MEASURE;
                ipb.transform.Find("FD_MEASURE_VALUE").GetComponent<UILabel>().text = fd_measure;
                string fd_first_value = it.FD_FIRST_VALUE;
                ipb.transform.Find("FD_FIRST_VALUE_VALUE").GetComponent<UILabel>().text = fd_first_value;
                string fd_buy_date = it.FD_BUY_DATE;
                ipb.transform.Find("FD_BUY_DATE_VALUE").GetComponent<UILabel>().text = fd_buy_date;
                string fd_num = it.FD_NUM;
                ipb.transform.Find("FD_NUM_VALUE").GetComponent<UILabel>().text = fd_num;
                string fd_time = it.FD_TIME;
                ipb.transform.Find("FD_TIME_VALUE").GetComponent<UILabel>().text = fd_time;
                string fd_responser = it.SFD_NAME;
                ipb.transform.Find("SFD_NAME_VALUE").GetComponent<UILabel>().text = fd_responser;
                string dept_name = it.DEPT_NAME;
                ipb.transform.Find("DEPT_NAME_VALUE").GetComponent<UILabel>().text = dept_name;
                string last_result = it.LAST_RESULT;
                ipb.transform.Find("LAST_RESULT_VALUE").GetComponent<UILabel>().text = last_result;
            }
        }

    }
    /// <summary>
    /// 盘点成功确认
    /// </summary>
    public void onUpdateAssetClick()
    {
         search.SetActive(false);
        string result = executeUpdateAsset2(dataStr,loginManager.globalUsername,"已盘点");
        resultLb.text = "已盘点:" + result + "!";

    }

    /// <summary>
    /// 盘点出问题提交
    /// </summary>
    public void onMistakeClick()
    {
        string mistake = GameObject.FindGameObjectWithTag("mistake").GetComponent<UILabel>().text;
        search.SetActive(false);
        string result = executeUpdateAsset(dataStr, loginManager.globalUsername,mistake);
        resultLb.text = "已提交问题:" + result + "!";

    }
}