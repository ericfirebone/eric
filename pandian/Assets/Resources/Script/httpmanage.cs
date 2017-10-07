using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using UnityEngine;
using System;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
public class httpmanage : MonoBehaviour {
   public List<item> itemlist = new List<item>();
  public GameObject itemPrefab;
  public UIGrid mygrid;
    public UILabel warningLabel;
    public int currentIndex =0;
    float gridwarning = 15.5f;
    float gridreinit =16.5f;
    public GameObject tip;
    // public string host= "http://10.1.17.15:8080";
    //  public string host = "http://10.1.0.66:8081";
    public string host = "http://60.30.201.10:8081";
    // Use this for initialization
    void Start () {
        //  Debug.Log(accessWebUrlPost());
       
    }
	
	// Update is called once per frame
	void Update () {
        testGridBottom();

    }
    public void reinit()
    {
     
        warningLabel.gameObject.SetActive(false);
        mygrid.enabled = true;
        itemlist.Clear();
        string url = host+"/cep/OaController/getAssetList?from=" + currentIndex;
        try
        {
            accessWebUrlPost(url);
        }
        catch (Exception e)
        {

            tip.SetActive(true);
            GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
            GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "读取失败，请检查网络";

        }


        while (mygrid.transform.childCount > 0)
           {
               DestroyImmediate(mygrid.transform.GetChild(0).gameObject);
           }


        GameObject  parent= mygrid.gameObject;
        UIScrollView parentScrollView = parent.GetComponent<UIScrollView>();
        mygrid.Reposition();
        parentScrollView.ResetPosition();
        GameObject ipb = Resources.Load("Prefabs/item") as GameObject;
        for (int i=0;i<itemlist.Count;i++)
        {
            //  GameObject go = GameObject.Instantiate(itemPrefab) as GameObject;
            item it=itemlist[i] as item;
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
            NGUITools.AddChild(parent, ipb);

  

          }
        mygrid.repositionNow = true;
        mygrid.Reposition();
        parentScrollView.ResetPosition();
        currentIndex += 10;

    }
   public void accessWebUrl()
    {
    try
    {
        string url = host+"/cep/OaController/getAssetList";
        WebRequest wRequest = WebRequest.Create(url);
        wRequest.Method = "GET";
        wRequest.ContentType = "text/html;charset=UTF-8";
        WebResponse wResponse = wRequest.GetResponse();
        Stream stream = wResponse.GetResponseStream();
        StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
        string str = reader.ReadToEnd();   //url返回的值  
        Console.WriteLine(str);

        reader.Close();
        wResponse.Close();
        }
        catch (Exception e)
        {
            tip.SetActive(true);
            GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
            GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "读取失败，请检查网络";
      
        }
    }

    public void accessWebUrlPost(string url)
    {
      
           
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "text/html; charset=utf-8";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string encoding = response.ContentEncoding;
        if (encoding == null || encoding.Length < 1)
        {
            encoding = "UTF-8"; //默认编码  
        }
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
        string retString = reader.ReadToEnd();
        //解析JSON并装入list
        itemlist = JsonHelper.DeserializeJsonToList<item>(retString);
        
     
    }

    public string accessWebUrlPost2(string url)
    {


        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "text/html; charset=utf-8";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string encoding = response.ContentEncoding;
        if (encoding == null || encoding.Length < 1)
        {
            encoding = "UTF-8"; //默认编码  
        }
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
        string retString = reader.ReadToEnd();
        //解析JSON并装入list
        return retString;
        

    }
    /// <summary>
    /// 检测是否滑动到底部
    /// </summary>
    public void testGridBottom()
    {
        if (mygrid.transform.position.y > gridwarning)
        {
            warningLabel.gameObject.SetActive(true);

        }
     
      
        
        if (mygrid.transform.position.y > gridreinit&&Input.touchCount==0)
        {
            mygrid.enabled = false;
            reinit();
        }

     
        return;
    }

    /// <summary>
    /// POST请求，参数按流传输
    /// </summary>
    /// <param name="url"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public string DoWebRequestByPost(string url, string param,UILabel label)
    {

        Encoding encoding = Encoding.Default;
        Encoding gb2312 = Encoding.GetEncoding("gb2312");
        Encoding utf8 = Encoding.UTF8;
        label.text = encoding.EncodingName;
        HttpWebResponse webResponse = null;
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        //使用post方式提交
        webRequest.Method = "POST";
        string responseStr = null;
        webRequest.Timeout = 50000;
        //要post的字节数组
        //byte[] postBytes = encoding.GetBytes(param);
        byte[] postBytes = encoding.GetBytes(param);
        try
        {
            postBytes = Encoding.Convert(encoding, gb2312, postBytes);
        }
        catch (Exception e)
        {
            label.text = encoding.EncodingName;
        }
        webRequest.ContentType = "application/x-www-form-urlencoded;charset=gb2312";
        webRequest.ContentLength = postBytes.Length;

        using (Stream reqStream = webRequest.GetRequestStream())
        {
            reqStream.Write(postBytes, 0, postBytes.Length);
        }
        try
        {
            //尝试获得要请求的URL的返回消息
            webResponse = (HttpWebResponse)webRequest.GetResponse();
        }
        catch (Exception)
        {
            //出错后直接抛出
            throw;
        }
        finally
        {
            if (webResponse != null)
            {
                //获得网络响应流
                using (StreamReader responseReader = new StreamReader(webResponse.GetResponseStream(), gb2312))
                {
                    responseStr = responseReader.ReadToEnd();//获得返回流中的内容
                }
                webResponse.Close();//关闭web响应流
            }
        }
        return responseStr;
    }
}
public class item {
    public string FD_ID;
    public string FD_NAME;
    public string FD_CODE;
    public string FD_STANDARD;
    public string FD_MEASURE;
    public string FD_FIRST_VALUE;
    public string FD_BUY_DATE;
    public string FD_NUM;
    public string FD_TIME;
    public string SFD_NAME;
    public string DEPT_NAME;
    public string LAST_RESULT;
} 
