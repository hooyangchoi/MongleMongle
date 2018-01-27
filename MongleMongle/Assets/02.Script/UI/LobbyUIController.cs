using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    private Transform m_trnfCanvas;
    private Transform m_trnfScrollView;

    private Image m_imgBG;
    private Text m_txtThemeName;
	// Use this for initialization
	void Start ()
    {
        m_trnfCanvas = GameObject.Find("Canvas").transform;
        m_imgBG = m_trnfCanvas.Find("BG").GetComponent<Image>();
        m_txtThemeName = m_trnfCanvas.Find("ThemeName/Text").GetComponent<Text>();
        m_trnfScrollView = m_trnfCanvas.Find("ScrollView");

        SetTheme(1);
        m_trnfScrollView.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTheme(int nThemeId)
    {
        ThemeVO themevo = GameData.Inst.ThemeList.Find(a => a.Id == nThemeId);

        m_txtThemeName.text = themevo.ThemeName;
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
    }
}
