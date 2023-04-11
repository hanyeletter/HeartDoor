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
    private LineRenderer lineRenderer; // LineRenderer���
    private int vertexCount = 2; // �߶ζ�����
    private Vector2 direction; // ���߷���
    public float maxDistance = 100f; // ����������
    public int reflectTime = 10; // �������
    public LayerMask reflectLayer; // ����ͼ��
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

        // ����LineRenderer��λ��
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.positionCount = vertexCount;

        // ������߷���
        direction = new Vector2(1, 0);

        // �������
        DrawRay(transform.position, direction, reflectTime); 
    }

    // ������
    void DrawRay(Vector2 origin, Vector2 direction, int reflectTime)
    {
        //lineRenderer.SetPosition(0, origin);
        // ʣ�෴�����С��0��ֹͣ�ݹ�
        if (reflectTime < 0)
        {
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, origin);
            return;
        }
        // ���߼��
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, reflectLayer);
        if (hit) //if hit
        {
            //������н��յ�
            if (hit.collider.tag == "LightReceiver")
            {
                if (isAllActive() && isClose(mirrorList[0], posRightUp, 0.3f) && isClose(mirrorList[1], posLeftDown, 0.3f)
                    && isClose(mirrorList[2], posRightDown, 0.3f)) //ÿ�����Ӽ�����λ�û���׼ȷ
                {
                    Vector2 endPoint = hit.point;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, endPoint);
                    if (!isGameFinished)
                    {
                        GameFinish();
                    }
                }
                else //����������
                {
                    Vector2 endPoint = origin + direction * maxDistance;
                    lineRenderer.SetPosition(lineRenderer.positionCount-1, endPoint);
                }
            }
            else
            {
                hit.collider.gameObject.GetComponent<ReflectLightMirror>().isActive = true;

                //�����߶���
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 2, hit.point);

                // ���㷴�䷽��
                Vector2 reflectedDirection = Vector2.Reflect(direction, hit.normal);

                // ���������
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
        Debug.Log("����������߳ǣ����ã�");
        foreach(ReflectLightMirror mirror in mirrorList)
        {
            mirror.isInteractive = false;
        }
        StartCoroutine(GameFinishShow());
    }
    
    IEnumerator GameFinishShow()
    {
        // �ȴ�һ�����ٿ�ʼ����
        yield return new WaitForSeconds(0.7f);
        spriteToFade.enabled = true;

        // ���ÿ�ʼ��͸����
        Color color = spriteToFade.color;
        color.a = startAlpha;
        spriteToFade.color = color;

        // ����
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration);
            color.a = alpha;
            spriteToFade.color = color;
            yield return null;
        }

        // ���ý�����͸����
        color.a = endAlpha;
        spriteToFade.color = color;

        yield return new WaitForSeconds(1.5f);
        smallGameManager.FinishASmallGame("ReflectLight");
    }

    /// <summary>
    /// �ж�����Transform�Ƿ����
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
