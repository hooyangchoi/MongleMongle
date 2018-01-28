using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField]
    private Image m_imgBG;

    private StageVO m_stage;

    private void OnDestroy()
    {
        instance = null;
    }

    private void Start()
    {
        m_nHitablePointerId = 0;
        m_pointerLineDrawer = GameObject.FindObjectOfType<PointLineDrawer>();

        m_pointerLineDrawer.OnPointerHit += PointerHitChecker;

        ThemeVO theme = GameData.Inst.ThemeList.Find(a => a.Id == GameController.Inst.ThemeId);
        StageVO stage = theme.StageList.Find(a => a.StageId == GameController.Inst.StageId);

        m_stage = stage;


        m_imgBG.sprite = GameData.Inst.GetStageSpriteList(GameController.Inst.ThemeId).Find(a => a.name == stage.BackgroundName + "_bg");
                        
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
        obj.GetComponent<Renderer>().material.color = new Color(164.0f/255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        //obj.transform.DOShakeScale(0.3f, 1, 1);
    }

    public void RecognizeNextPointer(int nId)
    {
        //TODO 뭐 찍어야 하는지 알려주야징ㅁㄻㅈㄷ

        GameObject obj = m_objListPointer.Find(a => a.name.Split(':')[1] == nId.ToString());
        obj.GetComponent<Renderer>().material.color = Color.black;
    }

    private void StageWin()
    {
        StartCoroutine(StageWinRoutine());
        //TODO WIN!!!!!!!!!!!
    }
    private IEnumerator StageWinRoutine()
    {
        yield return new WaitForSeconds(0.3f);

        GameController.Inst.ClearStage(m_stage.Id);
    }
}
