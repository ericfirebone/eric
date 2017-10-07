using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loginManager : MonoBehaviour {

    public GameObject username;
    public GameObject password;
    public static string globalUsername = "";
    public GameObject tip;
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


    public void onLoginButtonClick()
    {
        string usernameText = username.GetComponent<UILabel>().text;
        string passwordText = password.transform.parent.GetComponent<UIInput>().value;
     //   Debug.Log(Md5Encrypt(passwordText));
        if (usernameText == ""|| usernameText == "用户名")
        {
            tip.SetActive(true);
            GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
            GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "请输入合格用户名";

        }
        else if (passwordText == ""|| passwordText == "密码")
        {
            tip.SetActive(true);
            GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
            GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "请输入密码";
        }
        else
        {
            httpmanage hm = new httpmanage();
            string url = host+"/cep/OaController/loginPandian?username=" + usernameText+"&password="+ Md5Encrypt(passwordText).ToLower();
            Debug.Log(url);
            string result = "";
            try
            {
                 result = hm.accessWebUrlPost2(url);
            }
            catch (Exception e)
            {
                tip.SetActive(true);
                GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
                GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "连接服务器异常,请检查网络";
                return;
            }
            if (result == "success")
            {
                globalUsername = usernameText;
                //成功，跳转到主界面SCENE
                SceneManager.LoadScene("Resources/Scene/pandian");
            }
            else
            {
                tip.SetActive(true);
                GameObject.FindGameObjectWithTag("tipTitleLb").GetComponent<UILabel>().text = "提示";
                GameObject.FindGameObjectWithTag("tipContentLb").GetComponent<UILabel>().text = "登录失败";
            }
        }

    }

    /// <summary>
    /// 32位MD5加密
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string MD5Encrypt32(string password)
    {
        string cl = password;
        string pwd = "";
        MD5 md5 = MD5.Create(); //实例化一个md5对像
                                // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
        // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
        for (int i = 0; i < s.Length; i++)
        {
            // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
            pwd = pwd + s[i].ToString("X");
        }
        return pwd;
    }
    #region MD5加密

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="input">输入</param>
    /// <returns></returns>
    public static string Md5Encrypt(string input)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        var data = Encoding.UTF8.GetBytes(input);
        var encs = md5.ComputeHash(data);
        return BitConverter.ToString(encs).Replace("-", "");
    }

    #endregion
}
