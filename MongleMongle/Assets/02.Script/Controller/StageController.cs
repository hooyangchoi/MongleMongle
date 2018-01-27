using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private static StageController instance;
    public static StageController Inst
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<StageController>();

            return instance;
        }
    }

    private PointLineDrawer m_pointerLineDrawer;

    private PointerContainerVO m_pointerContainerVO;

    private List<GameObject> m_objListPointer = new List<GameObject>();
    private int m_nHitablePointerId;

    private void OnDestroy()
    {
        instance = null;
    }

    private void Start()
    {
        m_nHitablePointerId = 0;
        m_pointerLineDrawer = GameObject.FindObjectOfType<PointLineDrawer>();

        m_pointerLineDrawer.OnPointerHit += PointerHitChecker;
    }

    public void SetContainer(PointerContainerVO pointercontainerVO)
    {
        m_pointerContainerVO = pointercontainerVO;
    }

    public void SetObjectList(List<GameObject> objListPointer)
    {
        m_objListPointer = objListPointer;
    }

    public void PointerHitChecker(int nId)
    {
        PointerVO pointer = m_pointerContainerVO.PointerList.Find(a => a.Id == nId);

        if(pointer.StartPointerId == m_nHitablePointerId)
        {
            m_nHitablePointerId = pointer.Id;
            HitCorrectPointer(pointer.Id);

            if (pointer.EndPointerId == 0)
            {
                StageWin();
            }
            else
            {
                RecognizeNextPointer(pointer.EndPointerId);
            }
        }
 
    }

    public void HitCorrectPointer(int nId)
    {
        GameObject obj = m_objListPointer.Find(a => a.name.Split(':')[1] == nId.ToString());
        obj.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void RecognizeNextPointer(int nId)
    {
        //TODO 뭐 찍어야 하는지 알려주야징ㅁㄻㅈㄷ

        GameObject obj = m_objListPointer.Find(a => a.name.Split(':')[1] == nId.ToString());
        obj.GetComponent<Renderer>().material.color = Color.red;
    }

    private void StageWin()
    {
        //TODO WIN!!!!!!!!!!!
    }
}
