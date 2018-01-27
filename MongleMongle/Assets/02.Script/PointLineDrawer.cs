using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLineDrawer : MonoBehaviour
{
    private LineRenderer m_lineRenderer;

    private bool m_bDrawing = true;

    private bool m_bMousBtnDown = false;

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
        while (m_bDrawing)
        {
            if (m_bMousBtnDown)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    m_lineRenderer.positionCount++;

                    m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, hit.point);

                    if(hit.collider.CompareTag("pointer"))
                    {
                        if (OnPointerHit != null)
                            OnPointerHit(int.Parse( hit.collider.name.Split(':')[1]));
                    }
                }
            }

            yield return null;
        }

        yield return null;
    }
}
