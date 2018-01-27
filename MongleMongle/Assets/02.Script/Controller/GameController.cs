using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
