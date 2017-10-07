using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControll : MonoBehaviour {

    public ETCJoystick controlETCJoystick;
    private Animator animator;
    public Sprite[] idleSprite;
    private string lastStatus = "n";

    private SpriteRenderer sr;
    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        animator.SetTrigger("n");
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        changeAnim();
        }

    void changeAnim()
    {
        //获取摇杆中心偏移的坐标    
        float joyPositionX = controlETCJoystick.axisX.axisValue;
        float joyPositionY = controlETCJoystick.axisY.axisValue;
        float xmin = 0.25f;
        float ymin = 0.25f;
        float xmax = 3;
        float ymax = 3;
        //    Debug.Log(joyPositionX+","+ joyPositionY);

        //根据滑杆坐标设置动画x,y从-0.75到0.75检测每份长宽0.25 ,-0.75;-0.5;-0.25;0.25;0.5;0.75
        if (joyPositionX > -xmin && joyPositionX < xmin && joyPositionY > -ymax && joyPositionY < -ymin)//下
        {
            animator.SetTrigger("d");
            lastStatus = "d";
            if (sr.flipX)
            {
                sr.flipX = false;
            }
            //这是通过Translate移动的方法，我们可以看出controlETCJoystick.axisX.axisValue代表X方向的轴向，后者则是Y轴方向
            transform.Translate(new Vector3(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue, 0) * Time.deltaTime * 0.5f, Space.Self);
        }
        else if (joyPositionX > -xmin && joyPositionX < xmin && joyPositionY > ymin && joyPositionY < ymax)//上
        {
            animator.SetTrigger("u");
            lastStatus = "u";
            if (sr.flipX)
            {
                sr.flipX = false;
            }
            //这是通过Translate移动的方法，我们可以看出controlETCJoystick.axisX.axisValue代表X方向的轴向，后者则是Y轴方向
            transform.Translate(new Vector3(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue, 0) * Time.deltaTime * 0.5f, Space.Self);
        }
        else if (joyPositionX > xmin && joyPositionX < xmax && joyPositionY > -ymin && joyPositionY < ymin)//右
        {
            animator.SetTrigger("r");
            lastStatus = "r";
            if (sr.flipX)
            {
                sr.flipX = false;
            }
            //这是通过Translate移动的方法，我们可以看出controlETCJoystick.axisX.axisValue代表X方向的轴向，后者则是Y轴方向
            transform.Translate(new Vector3(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue, 0) * Time.deltaTime * 0.5f, Space.Self);
        }
        else if (joyPositionX > -xmax && joyPositionX < -xmin && joyPositionY > -ymin && joyPositionY < ymin)//左
        {
            animator.SetTrigger("r");
            lastStatus = "l";
            if (!sr.flipX)
            {
                sr.flipX = true;
            }
            //这是通过Translate移动的方法，我们可以看出controlETCJoystick.axisX.axisValue代表X方向的轴向，后者则是Y轴方向
            transform.Translate(new Vector3(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue, 0) * Time.deltaTime * 0.5f, Space.Self);
        }
        else if (joyPositionX > -xmax && joyPositionX < -xmin && joyPositionY > ymin && joyPositionY < ymax)//左上
        {
            animator.SetTrigger("ur");
            lastStatus = "ul";
            if (!sr.flipX)
            {
                sr.flipX = true;
            }
            //这是通过Translate移动的方法，我们可以看出controlETCJoystick.axisX.axisValue代表X方向的轴向，后者则是Y轴方向
            transform.Translate(new Vector3(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue, 0) * Time.deltaTime * 0.5f, Space.Self);
        }
        else if (joyPositionX > -xmax && joyPositionX < -xmin && joyPositionY > -ymax && joyPositionY < -ymin)//左下
        {
            animator.SetTrigger("dr");
            lastStatus = "dl";
            if (!sr.flipX)
            {
                sr.flipX = true;
            }
            //这是通过Translate移动的方法，我们可以看出controlETCJoystick.axisX.axisValue代表X方向的轴向，后者则是Y轴方向
            transform.Translate(new Vector3(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue, 0) * Time.deltaTime * 0.5f, Space.Self);
        }
        else if (joyPositionX > xmin && joyPositionX < xmax && joyPositionY > -ymax && joyPositionY < -ymin)//右下
        {
            animator.SetTrigger("dr");
            lastStatus = "dr";
            if (sr.flipX)
            {
                sr.flipX = false;
            }
            //这是通过Translate移动的方法，我们可以看出controlETCJoystick.axisX.axisValue代表X方向的轴向，后者则是Y轴方向
            transform.Translate(new Vector3(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue, 0) * Time.deltaTime * 0.5f, Space.Self);
        }
        else if (joyPositionX > xmin && joyPositionX < xmax && joyPositionY > ymin && joyPositionY < ymax)//右上
        {

            animator.SetTrigger("ur");
            lastStatus = "ur";
            if (sr.flipX)
            {
                sr.flipX = false;
            }
            //这是通过Translate移动的方法，我们可以看出controlETCJoystick.axisX.axisValue代表X方向的轴向，后者则是Y轴方向
            transform.Translate(new Vector3(controlETCJoystick.axisX.axisValue, controlETCJoystick.axisY.axisValue, 0) * Time.deltaTime * 0.5f, Space.Self);
        }
        else {
            switch (lastStatus)
            {
                case "d":
                    animator.SetTrigger("n");
                    if (sr.flipX)
                    {
                        sr.flipX = false;
                    }
                    break;

                case "u":
                    animator.SetTrigger("uIdle");
                    if (sr.flipX)
                    {
                        sr.flipX = false;
                    }
                    break;
                case "r":
                    animator.SetTrigger("rIdle");
                    if (sr.flipX)
                    {
                        sr.flipX = false;
                    }
                    break;
                case "l":
                    animator.SetTrigger("rIdle");
                    if (!sr.flipX)
                    {
                        sr.flipX = true;
                    }
                    break;
                case "dr":
                    animator.SetTrigger("drIdle");
                    if (sr.flipX)
                    {
                        sr.flipX = false;
                    }
                    break;
                case "ur":
                    animator.SetTrigger("urIdle");
                    if (sr.flipX)
                    {
                        sr.flipX = false;
                    }
                    break;
                case "dl":
                    animator.SetTrigger("drIdle");
                    if (!sr.flipX)
                    {
                        sr.flipX = true;
                    }
                    break;
                case "ul":
                    animator.SetTrigger("urIdle");
                    if (!sr.flipX)
                    {
                        sr.flipX = true;
                    }
                    break;
                default:

                    break;
            }
        }
       
        //float currentAngle = CalculaAngle(joyPositionX, joyPositionY);
        //Debug.Log(currentAngle);
        //if (currentAngle==90)
        //{
        //    animator.SetTrigger("n");
        //}
        //else if (currentAngle <= 22.5f && currentAngle >= 0f || currentAngle <= 360f && currentAngle >= 337.5f)//0;左
        //{
        //    animator.SetTrigger("r");
        //    lastStatus = "r";
        //    if (!sr.flipX)
        //    {
        //        sr.flipX = true;
        //    }
        //}
        //else if (currentAngle <= 67.5f && currentAngle >= 22.5f)//45;左上
        //{
        //    animator.SetTrigger("ur");
        //    lastStatus = "ur";
        //    if (!sr.flipX)
        //    {
        //        sr.flipX = true;
        //    }
        //}
        //else if (currentAngle <= 112.5f && currentAngle >= 67.5f)//90;上
        //{
        //    animator.SetTrigger("u");
        //    lastStatus = "u";
        //    if (sr.flipX)
        //    {
        //        sr.flipX = false;
        //    }
        //}
        //else if (currentAngle <= 157.5f && currentAngle >= 112.5f)//135;右上
        //{
        //    animator.SetTrigger("ur");
        //    lastStatus = "ur";
        //    if (sr.flipX)
        //    {
        //        sr.flipX = false;
        //    }
        //}
        //else if (currentAngle <= 202.5f && currentAngle >= 157.5f)//180;右
        //{
        //    animator.SetTrigger("r");
        //    lastStatus = "r";
        //    if (sr.flipX)
        //    {
        //        sr.flipX = false;
        //    }
        //}
        //else if (currentAngle <= 247.5f && currentAngle >= 202.5f)//225;右下
        //{
        //    animator.SetTrigger("dr");
        //    lastStatus = "dr";
        //    if (sr.flipX)
        //    {
        //        sr.flipX = false;
        //    }
        //}
        //else if (currentAngle <= 292.5f && currentAngle >= 247.5f)//270;下
        //{
        //    animator.SetTrigger("d");
        //    lastStatus = "d";
        //    if (sr.flipX)
        //    {
        //        sr.flipX = false;
        //    }
        //}
        //else if (currentAngle <= 337.5f && currentAngle >= 292.5f)//315;左下
        //{
        //    animator.SetTrigger("dr");
        //    lastStatus = "dr";
        //    if (!sr.flipX)
        //    {
        //        sr.flipX = true;
        //    }
        //}




    }

    public void joyStickEvent()
    {



    }
    public void joyStickEventEnd()
    {
      
    }

    /// 计算摇杆角度 <summary>
      /// 计算摇杆角度
     /// </summary>
      /// <param name="_joyPositionX">摇杆X轴</param>
      /// <param name="_joyPositionY">摇杆Y轴</param>
      /// <returns>返回当前摇杆旋转多少度</returns>
     private float CalculaAngle(float _joyPositionX, float _joyPositionY)
     {
         float currentAngleX = _joyPositionX * 90f + 90f;//X轴 当前角度
         float currentAngleY = _joyPositionY * 90f + 90f;//Y轴 当前角度
 
         //下半圆
        if (currentAngleY< 90f)
         {
             if (currentAngleX< 90f)
             {
                return 270f + currentAngleY;
             }
            else if (currentAngleX > 90f)
             {
                 return 180f + (90f - currentAngleY);
            }
        }
         return currentAngleX;
     }
}
