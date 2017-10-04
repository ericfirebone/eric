﻿using UnityEngine;
using System.Collections;

public class JoyStickDragObject : MonoBehaviour
{
    public Transform target;
    public Transform targetParent;
    public float parentWidthRadiu=392;
    public float parentHeightRadiu= 344;
    Vector3 mTargetPos;                                         // 目标当前位置
    Vector3 mLastPos;

    int mTouchID = 0;

    bool mStarted = false;
    bool mPressed = false;

    [SerializeField]
    protected Vector3 scale = new Vector3(1f, 1f, 0f);
     protected Vector3 originPos = Vector3.zero;
 
    protected Vector3 offsetFromOrigin = Vector3.zero;          // 原点到拖拽位置的向量

    public Vector3 OffsetFromOrigin
    {
        set { offsetFromOrigin = value; }
        get { return offsetFromOrigin; }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnPress(bool pressed)
    {
        if (enabled && NGUITools.GetActive(gameObject) && target != null)
        {
            if (pressed)
            {
                if (!mPressed)
                {
                    mTouchID = UICamera.currentTouchID;
                    mPressed = true;
                    mStarted = false;
                    CancelMovement();
                }
            }
            else if (mPressed && mTouchID == UICamera.currentTouchID)
            {
                mPressed = false;
                target.position = targetParent.position;
            }
        }

    }
    void OnDrag(Vector2 delta)
    {
        Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
        float dist = 0f;

        Vector3 currentPos = ray.GetPoint(dist);
        ///< 更新当前坐标到上一时刻坐标的向量
        Vector3 offset = currentPos - mLastPos;
        ///< 更新当前坐标到原点的向量
        UpdateVector3FromOrigin(currentPos);
        mLastPos = currentPos;

        if (!mStarted)
        {
            mStarted = true;
            offset = Vector3.zero;
        }

       // Debug.Log("offset: " + offset + " OffsetFromOrigin: " + OffsetFromOrigin);
        Move(offset);
    }

  
    void Move(Vector3 moveDelta)
    {
        mTargetPos += moveDelta;

        target.position = mTargetPos;
        checkPositionLimit();
      //  Debug.Log("mTargetPos: " + mTargetPos );
    }

    void CancelMovement()
    {
        if (target != null)
        {
            Vector3 pos = target.localPosition;
            target.localPosition = pos;
        }
        //  mTargetPos = (target != null) ? target.position : Vector3.zero;
        mTargetPos = (target != null) ? target.position : targetParent.position;
        
    }

    ///< 更新当前坐标到原点的向量
    void UpdateVector3FromOrigin(Vector3 pos)
    {
        OffsetFromOrigin = pos - originPos;
    }

    /// 限制内部
    void checkPositionLimit()
    {
        if (target.transform.position.x > target.transform.position.x + parentWidthRadiu)
        {
            target.transform.SetPositionAndRotation(new Vector3(target.transform.position.x + parentWidthRadiu, target.transform.position.y, 0), Quaternion.identity);

        }
        if (target.transform.position.x > target.transform.position.x - parentWidthRadiu)
        {
            target.transform.SetPositionAndRotation(new Vector3(target.transform.position.x - parentWidthRadiu, target.transform.position.y, 0), Quaternion.identity);
        }
        if (target.transform.position.y > target.transform.position.y + parentHeightRadiu)
        {
            target.transform.SetPositionAndRotation(new Vector3(target.transform.position.x, target.transform.position.y + parentHeightRadiu, 0), Quaternion.identity);
        }
        if (target.transform.position.y > target.transform.position.y - parentHeightRadiu)
        {
            target.transform.SetPositionAndRotation(new Vector3(target.transform.position.x, target.transform.position.y - parentHeightRadiu, 0), Quaternion.identity);
        }
        //if (target.transform.position.x > target.transform.position.x + parentWidthRadiu)
        //{
        //    target.transform.SetPositionAndRotation(targetParent.position, Quaternion.identity);

        //}
        //if (target.transform.position.x > target.transform.position.x - parentWidthRadiu)
        //{
        //    target.transform.SetPositionAndRotation(targetParent.position, Quaternion.identity);
        //}
        //if (target.transform.position.y > target.transform.position.y + parentHeightRadiu)
        //{
        //    target.transform.SetPositionAndRotation(targetParent.position, Quaternion.identity);
        //}
        //if (target.transform.position.y > target.transform.position.y - parentHeightRadiu)
        //{
        //    target.transform.SetPositionAndRotation(targetParent.position, Quaternion.identity);
        //}
    }
}
