using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenuController : MonoBehaviour {
    public GameObject tip;
    public GameObject search;
    public List<item> itemlist = new List<item>();
    // public string host= "http://10.1.17.15:8080";
    //  public string host = "http://10.1.0.66:8081";
    public string host = "http://60.30.201.10:8081";
    // Use this for initialization
    void Start () {
       
	}

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public TweenScale showListTween;
    public TweenScale startPanelTween;
   

    public void onShowListClick()
    {

        // StartCoroutine(hidePanel(startPanelTween.gameObject));
        startPanelTween.gameObject.SetActive(false);
      //  showListTween.PlayForward();
       showListTween.gameObject.SetActive(true);
        GameObject.Find("itemGrid").SendMessage("reinit");
    }

    /// <summary>
    /// 关闭列表
    /// </summary>
    public void onCloseClick()
    {

        //   StartCoroutine(hidePanel(showListTween.gameObject));
        showListTween.gameObject.SetActive(false);
        //  startPanelTween.PlayReverse();
        startPanelTween.gameObject.SetActive(true);
        if (search!=null)
        {
            search.SetActive(false);
        }
    }

    /// <summary>
    /// 扫描盘点
    /// </summary>
    public void onSaomiaoClick()
    {
        SceneManager.LoadScene("Easy Code Scanner/EasyCodeScanner");

    }
    /// <summary>
    /// 加载更多
    /// </summary>
    public void onLoadBtnClick()
    {
        GameObject.Find("itemGrid").SendMessage("reinit");

    }
    public IEnumerator hidePanel(GameObject go)
    {

        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);
    }

    public void onSearchClick()
    {
       
        UILabel searchText = GameObject.FindGameObjectWithTag("searchtext").GetComponent<UILabel>();

        if (searchText.text =="")
        {
            tip.SetActive(true);
            GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
            GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "查找编号不能为空";
        }
        else
        {
            string FD_CODE = searchText.text;
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
            if (json.IndexOf("查询失败") !=-1||json=="")
            {
                tip.SetActive(true);
                GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
                GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "查找不到对应编号的资产";

                return;
            }
            itemlist = JsonHelper.DeserializeJsonToList<item>(json);
            search.SetActive(true);
            for (int i=0;i<itemlist.Count;i++)
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
}
