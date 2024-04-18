using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TransformSceneManager : Singleton<TransformSceneManager>
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;

    private bool isFade;

    protected override void Awake()//��ʼ��
    {
        base.Awake();
    }

    protected override void OnDestroy()//����
    {
        base.OnDestroy();
    }

    private void OnEnable()
    {
        EventCenter.AddListener<string, string>(EventType.teleport, TransformScene);
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener<string, string>(EventType.teleport, TransformScene);
    }

    /// <summary>
    /// ����ת���߼�
    /// </summary>
    /// <param name="from">ж�صĳ���</param>
    /// <param name="to">���صĳ���</param>
    public void TransformScene(string from, string to)
    {
        if (!isFade)
        {
            StartCoroutine(TranstionToScene(from, to));
        }
    }

    private IEnumerator TranstionToScene(string from, string to)
    {
        yield return Fade(1);
        yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        //�����³���Ϊ�����
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);


        yield return Fade(0);
    }

    //���䣬ת����ָ��͸����
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }
}
