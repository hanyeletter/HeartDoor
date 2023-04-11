using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLine : MonoBehaviour
{
    private SmallGameManager smallGameManager = new SmallGameManager();
    public Transform posRightUp;
    public Transform posLeftDown;
    public Transform posRightDown;


    public SpriteRenderer spriteToFade;
    public float fadeDuration = 3f;
    private float startAlpha = 0f;
    private float endAlpha = 1f;

    public bool isGameFinished = false;
    public List<ReflectLightMirror> mirrorList = new List<ReflectLightMirror>();
    private LineRenderer lineRenderer; // LineRenderer组件
    private int vertexCount = 2; // 线段顶点数
    private Vector2 direction; // 光线方向
    public float maxDistance = 100f; // 光线最大距离
    public int reflectTime = 10; // 反射次数
    public LayerMask reflectLayer; // 反射图层
    public Material myLineMaterial;
    public float width = 0.1f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        foreach (ReflectLightMirror mirror in mirrorList)
            mirror.isActive = false;

        lineRenderer.material = myLineMaterial;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        // 设置LineRenderer的位置
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.positionCount = vertexCount;

        // 计算光线方向
        direction = new Vector2(1, 0);

        // 发射光线
        DrawRay(transform.position, direction, reflectTime); 
    }

    // 画射线
    void DrawRay(Vector2 origin, Vector2 direction, int reflectTime)
    {
        //lineRenderer.SetPosition(0, origin);
        // 剩余反射次数小于0，停止递归
        if (reflectTime < 0)
        {
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, origin);
            return;
        }
        // 射线检测
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, reflectLayer);
        if (hit) //if hit
        {
            //如果击中接收点
            if (hit.collider.tag == "LightReceiver")
            {
                if (isAllActive() && isClose(mirrorList[0], posRightUp, 0.3f) && isClose(mirrorList[1], posLeftDown, 0.3f)
                    && isClose(mirrorList[2], posRightDown, 0.3f)) //每个镜子激活且位置基本准确
                {
                    Vector2 endPoint = hit.point;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, endPoint);
                    if (!isGameFinished)
                    {
                        GameFinish();
                    }
                }
                else //不满足条件
                {
                    Vector2 endPoint = origin + direction * maxDistance;
                    lineRenderer.SetPosition(lineRenderer.positionCount-1, endPoint);
                }
            }
            else
            {
                hit.collider.gameObject.GetComponent<ReflectLightMirror>().isActive = true;

                //处理线顶点
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 2, hit.point);

                // 计算反射方向
                Vector2 reflectedDirection = Vector2.Reflect(direction, hit.normal);

                // 画反射光线
                DrawRay(hit.point + reflectedDirection.normalized * 0.01f, reflectedDirection, reflectTime - 1);
            }
        }
        else // if not hit
        {
            Vector2 endPoint = origin + direction * maxDistance;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, endPoint);
        }
    }

    private bool isAllActive()
    {
        foreach (ReflectLightMirror mirror in mirrorList)
        {
            if (!mirror.isActive)
                return false;
        }
        return true;
    }

    private void GameFinish()
    {
        isGameFinished = true;
        smallGameManager.SetTheCloseBtn("ReflectLight", false);
        Debug.Log("到达世界最高城，礼堂！");
        foreach(ReflectLightMirror mirror in mirrorList)
        {
            mirror.isInteractive = false;
        }
        StartCoroutine(GameFinishShow());
    }
    
    IEnumerator GameFinishShow()
    {
        // 等待一秒钟再开始渐变
        yield return new WaitForSeconds(0.7f);
        spriteToFade.enabled = true;

        // 设置开始的透明度
        Color color = spriteToFade.color;
        color.a = startAlpha;
        spriteToFade.color = color;

        // 渐变
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration);
            color.a = alpha;
            spriteToFade.color = color;
            yield return null;
        }

        // 设置结束的透明度
        color.a = endAlpha;
        spriteToFade.color = color;

        yield return new WaitForSeconds(1.5f);
        smallGameManager.FinishASmallGame("ReflectLight");
    }

    /// <summary>
    /// 判断两个Transform是否相近
    /// </summary>
    /// <param name="dist"></param>
    /// <returns></returns>
    private bool isClose(ReflectLightMirror mirror, Transform pos, float _dist)
    {
        float dist = Vector3.Distance(mirror.transform.position, pos.position);
        if (dist <= _dist)
            return true;
        return false;
    }
}
