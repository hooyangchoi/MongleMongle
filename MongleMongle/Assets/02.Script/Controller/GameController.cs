using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController Inst
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<GameController>();

            return instance;
        }
    }

    private int m_nThemeId;
    private int m_nStageId;

    private List<int> m_listClearStage = new List<int>();

    [SerializeField]
    public Transform TrnfCanvas;
    private Image m_imgMultiplyGrey;
    private Image m_imgMultiplyWhite;

    public bool First = true;

    public int ThemeId
    {
        get { return m_nThemeId; }
        set
        {
            m_nThemeId = value;

            if (m_nThemeId == 0)
                m_nThemeId = GameData.Inst.ThemeList.Count;
            else if (m_nThemeId > GameData.Inst.ThemeList.Count)
                m_nThemeId = 1;
        }
    }

    public int StageId
    {
        get { return m_nStageId; }
        set { m_nStageId = value; }
    }

    public delegate void LoadingEnd();
    public event LoadingEnd OnLoadingEnd;

    // Use this for initialization
    void Awake ()
    {
        m_nThemeId = 1;
        m_nStageId = 1;

        m_imgMultiplyGrey = TrnfCanvas.Find("multiply").GetComponent<Image>();
        m_imgMultiplyWhite = TrnfCanvas.Find("white").GetComponent<Image>();
    }

    public void LoadScene(string sSceneName)
    {
        StartCoroutine(LoadSceneRoutine(sSceneName));
    }

    private IEnumerator LoadSceneRoutine(string sSceneName)
    {

        AsyncOperation async = SceneManager.LoadSceneAsync(sSceneName);

        yield return async;

        if (OnLoadingEnd != null)
            OnLoadingEnd();
    }

    public void LoadSceneFail(string sSceneName)
    {
        StartCoroutine(LoadSceneFailRoutine(sSceneName));
    }

    private IEnumerator LoadSceneFailRoutine(string sSceneName)
    {
        m_imgMultiplyGrey.gameObject.SetActive(true);
        m_imgMultiplyGrey.DOFade(1, 2.0f);

        yield return new WaitForSeconds(2.0f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sSceneName);

        yield return async;

        m_imgMultiplyGrey.DOFade(0, 1.5f);

        yield return new WaitForSeconds(1.5f);
        m_imgMultiplyGrey.gameObject.SetActive(false);
        if (OnLoadingEnd != null)
            OnLoadingEnd();
    }

    public void FailStage()
    {
        LoadSceneFail("Lobby");
    }

    public void ClearStage(int nId)
    {
        m_listClearStage.Add(nId);
        LoadSceneClear("Lobby");
    }

    public bool IsClearStage(int nId)
    {
        return m_listClearStage.Contains(nId);
    }

    public void LoadSceneClear(string sSceneName)
    {
        StartCoroutine(LoadSceneClearRoutine(sSceneName));
    }

    private IEnumerator LoadSceneClearRoutine(string sSceneName)
    {
        m_imgMultiplyWhite.gameObject.SetActive(true);
        m_imgMultiplyWhite.DOFade(1, 0.5f);

        yield return new WaitForSeconds(2.0f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sSceneName);

        yield return async;

        m_imgMultiplyWhite.DOFade(0, 1.5f);

        yield return new WaitForSeconds(1.5f);
        m_imgMultiplyWhite.gameObject.SetActive(false);
        if (OnLoadingEnd != null)
            OnLoadingEnd();
    }
}
