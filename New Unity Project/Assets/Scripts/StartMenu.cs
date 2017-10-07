using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {
    public MovieTexture movTexture;//视频纹理   
    public bool isDrawMov = true;//是否执行绘制
    public bool isShowMessage = false;//是否显示消息
	// Use this for initialization
	void Start () {
        movTexture.loop = false;
        movTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (isDrawMov)//当正在绘制纹理
        {
            //if (Input.GetMouseButtonDown(0) && isShowMessage == false)//当单击鼠标左键且没有显示消息时
            //{
            //    isShowMessage = true;//可以显示文字
            //} else if (Input.GetMouseButtonDown(0) && isShowMessage == true)//当单击鼠标左键且已经显示消息时
            //{
            //    StopMov();
            //}
            if (Input.GetMouseButtonDown(0))
            {
                StopMov();
            }


        }
        if (isDrawMov!=movTexture.isPlaying)//当自然播放完成
        {
            StopMov();
        }
	}

    void OnGUI()
    {
        if (isDrawMov)//当可以绘制纹理
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movTexture);
        }
        if (isShowMessage)//当可以显示文字
        {
            GUI.Label(new Rect(Screen.width/2-75,Screen.height/2-20,150,40),"再次点击退出");
        }
    }
    private void StopMov()
    {

        movTexture.Stop();
        isDrawMov = false;
    }
}
