using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shenshe : MonoBehaviour
{

    private Vector3 BPoint;

    private GameObject shenshePos;
    public GameObject shensheArrow;
    private Vector3 APoint;
    public GameObject parent;
    private void Start()
    {
        parent = GameObject.FindWithTag("Canvas");

        APoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        shenshePos = Instantiate(shensheArrow, parent.transform, false);


    }

    private void Update()
    {
        BPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (BPoint.x < Screen.width
       && BPoint.y < Screen.height
       && BPoint.x > 0 && BPoint.y > 0)
        { shenshePos.SetActive(false); return; }
        //否则
        else
        {
            //enemyPos指箭头UI物体  在Start函数动态生成，每一只怪物出生时都会生成一个对应箭头
            //并设置为怪物的子物体，且更名为”icon“
            shenshePos.SetActive(true);
            //UILookAt方法使箭头始终指向怪物  具体代码见博客下方
            UILookAt(shenshePos.transform, BPoint - APoint, Vector3.up);
            //OnLinearAlgebra方法确定箭头当前位置  具体代码见博客下方
            OnLinearAlgebra(APoint, BPoint);
        }
        //调整指示箭头Z轴旋转，使UI正确面向相机
        if (transform.Find("icon1") != null)
        {
            transform.Find("icon1").transform.LookAt(Camera.main.transform);
        }

    }

    Vector3 target;
    // 这里的  50  是指箭头距离边框的余量 可根据实际情况自行调整
    Vector2 la = new Vector2(viewLerp, 100);//左下   
    Vector2 lb = new Vector2(viewLerp, Screen.height - 100f);//左上
    Vector2 ra = new Vector2(Screen.width - viewLerp, 100);//右下
    Vector2 rb = new Vector2(Screen.width - viewLerp, Screen.height - 100f);//右上
    static float viewLerp = Screen.width * 0.12f;
    bool GetPoint(Vector2 pos1, Vector2 pos2, Vector2 pos3, Vector2 pos4)
    {
        float a = pos2.y - pos1.y;
        float b = pos1.x - pos2.x;
        float c = pos2.x * pos1.y - pos1.x * pos2.y;
        float d = pos4.y - pos3.y;
        float e = pos3.x - pos4.x;
        float f = pos4.x * pos3.y - pos3.x * pos4.y;
        float x = (f * b - c * e) / (a * e - d * b);
        float y = (c * d - f * a) / (a * e - d * b);
        // if (a * e == d * b)  //A1*B2==A2*B1  平行
        //     return;
        if (x < 0 || y < 0 || x > Screen.width || y > Screen.height) return false;
        if (!GetOnLine(pos1, pos2, new Vector2(x, y))) return false;
        target = new Vector3(x, y);
        return true;
    }
    /*左侧边框端点：lbP(0, 0) 、ltP(0, Screen.Height) ；
     底部边框端点：lbP(0, 0) 、rbP(Screen.Width, 0) ；
     右侧边框端点：rbP(Screen.Width, 0) 、rtP(Screen.Width, Screen.Height) ；
     顶部边框端点：ltP(0, Screen.Height) 、rtP(Screen.Width, Screen.Height) ；
     */
    /// <summary>
    /// 线代解
    /// </summary>
    void OnLinearAlgebra(Vector3 pos1, Vector3 pos2)
    {
        //AB线段的abc   //a=y2-y1  b=x1-x2  c=x2y1-x1y2
        Vector2 ua = lb;//左上
        Vector2 ub = rb;//右上
        Vector2 da = la;//左下
        Vector2 db = ra;//右下

        if (GetPoint(pos1, pos2, la, lb)) shenshePos.transform.position = target;
        else if (GetPoint(pos1, pos2, ra, rb)) shenshePos.transform.position = target;
        else if (GetPoint(pos1, pos2, ua, ub)) shenshePos.transform.position = target;
        else if (GetPoint(pos1, pos2, da, db)) shenshePos.transform.position = target;

    }

    /// <summary>
    /// 判断在哪根线
    /// </summary>
    /// <param name="pos1">a</param>
    /// <param name="pos2">b</param>
    /// <param name="pos3">p</param>
    bool GetOnLine(Vector2 pos1, Vector2 pos2, Vector2 pos3)
    {
        float EPS = 1e-3f; //误差
        float d1 = Vector2.Distance(pos1, pos3);
        float d2 = Vector2.Distance(pos2, pos3);
        float d3 = Vector2.Distance(pos1, pos2);
        if (Mathf.Abs(d1 + d2 - d3) <= EPS)
            return true;
        else
            return false;
    }


    /// <summary>这个方法是让箭头指向处于屏幕中间的玩家坐标与箭头坐标向量的方向</summary>
    /// <param name="ctrlObj">控制的箭头</param>
    public void UILookAt(Transform ctrlObj, Vector3 dir, Vector3 lookAxis)
    {
        Quaternion q = Quaternion.identity;
        q.SetFromToRotation(lookAxis, dir);
        ctrlObj.eulerAngles = new Vector3(q.eulerAngles.x, 0, q.eulerAngles.z);
    }

}
