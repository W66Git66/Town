using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gfire : MonoBehaviour
{
    public Sprite lightFire;
    private SpriteRenderer sr;
    public GameObject light;
    private Vector3 BPoint;

    private GameObject enemyPos;
    public GameObject enemyArrow;
    private Vector3 APoint;
    public GameObject parent;

    private void Start()
    { 
        parent = GameObject.FindWithTag("Canvas");
        sr = GetComponent<SpriteRenderer>();

        APoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        enemyPos=Instantiate(enemyArrow, parent.transform,false);
        
       
    }

    private void Update()
    {
        BPoint = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log(BPoint);
        if (BPoint.x < Screen.width
       && BPoint.y < Screen.height
       && BPoint.x > 0 && BPoint.y > 0)
        { enemyPos.SetActive(false); return; }
        //����
        else
        {
            //enemyPosָ��ͷUI����  ��Start������̬���ɣ�ÿһֻ�������ʱ��������һ����Ӧ��ͷ
            //������Ϊ����������壬�Ҹ���Ϊ��icon��
            enemyPos.SetActive(true);
            //UILookAt����ʹ��ͷʼ��ָ�����  �������������·�
            UILookAt(enemyPos.transform, BPoint - APoint, Vector3.up);
            //OnLinearAlgebra����ȷ����ͷ��ǰλ��  �������������·�
            OnLinearAlgebra(APoint, BPoint);
        }
        //����ָʾ��ͷZ����ת��ʹUI��ȷ�������
        if (transform.Find("icon") != null)
        {
            transform.Find("icon").transform.LookAt(Camera.main.transform);
        }

    }
    public void ChangeSprite()
    {
        light.SetActive(true);
        sr.sprite = lightFire;
        GameManager.Instance.gfireNumber += 1;
    }

    Vector3 target;
    // �����  50  ��ָ��ͷ����߿������ �ɸ���ʵ��������е���
    Vector2 la = new Vector2(viewLerp, 50);//����   
    Vector2 lb = new Vector2(viewLerp, Screen.height - 50f);//����
    Vector2 ra = new Vector2(Screen.width - viewLerp, 50);//����
    Vector2 rb = new Vector2(Screen.width - viewLerp, Screen.height - 50f);//����
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
        // if (a * e == d * b)  //A1*B2==A2*B1  ƽ��
        //     return;
        if (x < 0 || y < 0 || x > Screen.width || y > Screen.height) return false;
        if (!GetOnLine(pos1, pos2, new Vector2(x, y))) return false;
        target = new Vector3(x, y);
        return true;
    }
    /*���߿�˵㣺lbP(0, 0) ��ltP(0, Screen.Height) ��
     �ײ��߿�˵㣺lbP(0, 0) ��rbP(Screen.Width, 0) ��
     �Ҳ�߿�˵㣺rbP(Screen.Width, 0) ��rtP(Screen.Width, Screen.Height) ��
     �����߿�˵㣺ltP(0, Screen.Height) ��rtP(Screen.Width, Screen.Height) ��
     */
    /// <summary>
    /// �ߴ���
    /// </summary>
    void OnLinearAlgebra(Vector3 pos1, Vector3 pos2)
    {
        //AB�߶ε�abc   //a=y2-y1  b=x1-x2  c=x2y1-x1y2
        Vector2 ua = lb;//����
        Vector2 ub = rb;//����
        Vector2 da = la;//����
        Vector2 db = ra;//����

        if (GetPoint(pos1, pos2, la, lb)) enemyPos.transform.position = target;
        else if (GetPoint(pos1, pos2, ra, rb)) enemyPos.transform.position = target;
        else if (GetPoint(pos1, pos2, ua, ub)) enemyPos.transform.position = target;
        else if (GetPoint(pos1, pos2, da, db)) enemyPos.transform.position = target;

    }

    /// <summary>
    /// �ж����ĸ���
    /// </summary>
    /// <param name="pos1">a</param>
    /// <param name="pos2">b</param>
    /// <param name="pos3">p</param>
    bool GetOnLine(Vector2 pos1, Vector2 pos2, Vector2 pos3)
    {
        float EPS = 1e-3f; //���
        float d1 = Vector2.Distance(pos1, pos3);
        float d2 = Vector2.Distance(pos2, pos3);
        float d3 = Vector2.Distance(pos1, pos2);
        if (Mathf.Abs(d1 + d2 - d3) <= EPS)
            return true;
        else
            return false;
    }


    /// <summary>����������ü�ͷָ������Ļ�м������������ͷ���������ķ���</summary>
    /// <param name="ctrlObj">���Ƶļ�ͷ</param>
    public void UILookAt(Transform ctrlObj, Vector3 dir, Vector3 lookAxis)
    {
        Quaternion q = Quaternion.identity;
        q.SetFromToRotation(lookAxis, dir);
        ctrlObj.eulerAngles = new Vector3(q.eulerAngles.x, 0, q.eulerAngles.z);
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
