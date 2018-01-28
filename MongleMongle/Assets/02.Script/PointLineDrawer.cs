using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLineDrawer : MonoBehaviour
{
    private LineRenderer m_lineRenderer;

    private bool m_bDrawing = true;

    private bool m_bMousBtnDown = false;

    public LineRenderer lineRenderer
    {
        get
        {
            return m_lineRenderer;
        }
    }

    public delegate void PointerHit(int nId);
    public event PointerHit OnPointerHit;

    public delegate void LineHit();
    public event LineHit OnLineHit;

    void Start()
    {
        m_lineRenderer = GameObject.FindObjectOfType<LineRenderer>();

        m_lineRenderer.positionCount = 0;

        StartCoroutine(Drawing());
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_bMousBtnDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_bMousBtnDown = false;
        }
    }

    private IEnumerator Drawing()
    {
        Vector3 prePos = new Vector3(10000, 10000, 10000);
        while (m_bDrawing)
        {
            yield return new WaitForEndOfFrame();
            if (m_bMousBtnDown)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.point == prePos)
                        continue;

                    m_lineRenderer.positionCount++;

                    m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, hit.point);
                    prePos = hit.point;
                    if (hit.collider.CompareTag("pointer"))
                    {
                        if (OnPointerHit != null)
                            OnPointerHit(int.Parse( hit.collider.name.Split(':')[1]));
                    }

                    if(LineHitChecker())
                    {
                        GameController.Inst.FailStage();
                    }
                }
            }

            
        }

        yield return null;
    }

    private bool LineHitChecker()
    {
        int nCnt = m_lineRenderer.positionCount;

        for (int i = 0; i < nCnt - 3; i++)
        {
            Vector2 originStart = (Vector2)m_lineRenderer.GetPosition(nCnt - 1);
            Vector2 originEnd = (Vector2)m_lineRenderer.GetPosition(nCnt - 2);
            Vector2 checkStart = (Vector2)m_lineRenderer.GetPosition(i);
            Vector2 checkEnd = (Vector2)m_lineRenderer.GetPosition(i + 1);

            Vector2 hitPoint = HitPoint(originStart, originEnd, checkStart, checkEnd);

            if (hitPoint.x >= Mathf.Min(originStart.x, originEnd.x) && hitPoint.x <= Mathf.Max(originStart.x, originEnd.x))
            {
                if (hitPoint.x >= Mathf.Min(checkStart.x, checkEnd.x) && hitPoint.x <= Mathf.Max(checkStart.x, checkEnd.x))
                {
                    if (hitPoint.y >= Mathf.Min(originStart.y, originEnd.y) && hitPoint.y <= Mathf.Max(originStart.y, originEnd.y))
                    {
                        if (hitPoint.y >= Mathf.Min(checkStart.y, checkEnd.y) && hitPoint.y <= Mathf.Max(checkStart.y, checkEnd.y))
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private Vector2 RemoveZ(Vector3 vc)
    {
        return (Vector2)vc;
    }

    private Vector2 HitPoint(Vector2 originStart, Vector2 originEnd, Vector2 checkStart, Vector2 checkEnd)
    {
        float x = (originStart.x * originEnd.y - originStart.y * originEnd.x) * (checkStart.x - checkEnd.x) 
                    - (originStart.x - originEnd.x) * (checkStart.x * checkEnd.y - checkStart.y * checkEnd.x);

        x /= (originStart.x - originEnd.x) * (checkStart.y - checkEnd.y) - (checkStart.x - checkEnd.x) * (originStart.y - originEnd.y);

        float y = (originStart.x * originEnd.y - originStart.y * originEnd.x) * (checkStart.y - checkEnd.y)
                    - (originStart.y - originEnd.y) * (checkStart.x * checkEnd.y - checkStart.y * checkEnd.x);

        y /= (originStart.x - originEnd.x) * (checkStart.y - checkEnd.y) - (checkStart.x - checkEnd.x) * (originStart.y - originEnd.y);

        return new Vector2(x, y);
    }
}

