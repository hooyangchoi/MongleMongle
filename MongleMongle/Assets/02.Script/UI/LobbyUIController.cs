using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LobbyUIController : MonoBehaviour
{
    private Transform m_trnfCanvas;
    private Transform m_trnfScrollView;
    private Transform m_trnfTheme;
    private Transform m_trnfLogo;

    private InfinitiButton m_infinityButton;
    private Image m_imgBG;
    private Text m_txtThemeName;
	// Use this for initialization
	void Start ()
    {
        m_trnfCanvas = GameObject.Find("Canvas").transform;
        m_imgBG = m_trnfCanvas.Find("BG").GetComponent<Image>();
        m_trnfTheme = m_trnfCanvas.Find("ThemeName");
        m_txtThemeName = m_trnfCanvas.Find("ThemeName/Text").GetComponent<Text>();
        m_trnfScrollView = m_trnfCanvas.Find("ScrollView");
        m_trnfLogo = m_trnfCanvas.Find("logo");
        m_infinityButton = m_trnfScrollView.GetComponent<InfinitiButton>();

        if (GameController.Inst.First == false)
        {
            StartGameRoutine();
        }

        GameController.Inst.First = false;
    }

    public void StartGame()
    {
        StartMultiply(StartGameRoutine);
    }

    private void StartMultiply(System.Action callback)
    {
        StartCoroutine(StartMultiplyRoutine(callback));
    }

    private IEnumerator StartMultiplyRoutine(System.Action callback)
    {
        Image img = m_trnfCanvas.Find("multiply").GetComponent<Image>();

        img.DOFade(1, 0.5f);

        yield return new WaitForSeconds(0.5f);

        if (callback != null)
            callback();

        img.DOFade(0, 1.5f);

        yield return new WaitForSeconds(1.5f);
    }

    private void StartGameRoutine()
    {
        m_trnfCanvas.Find("StartButton").gameObject.SetActive(false);

        m_trnfLogo.gameObject.SetActive(false);
        m_trnfTheme.gameObject.SetActive(true);
        SetTheme(1);
        m_trnfScrollView.gameObject.SetActive(true);
    }

    public void SetTheme(int nThemeId)
    {
        ThemeVO themevo = GameData.Inst.ThemeList.Find(a => a.Id == nThemeId);

        m_txtThemeName.text = themevo.ThemeName;

        SoundController.Inst.PlayBGM(themevo.BackgroundSoundName);
    }

    public void ThemeChange(bool bRight)
    {

        if(bRight)
        {
            GameController.Inst.ThemeId += 1;
        }
        else
        {
            GameController.Inst.ThemeId -= 1;
        }

        SetTheme(GameController.Inst.ThemeId);
        m_infinityButton.Init();
    }
}
