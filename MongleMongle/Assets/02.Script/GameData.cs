using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private static GameData instance = null;
    public static GameData Inst
    {
        get
        {
            if (instance == null)
                instance = new GameData();

            return instance;
        }
    }

    public List<ThemeVO> ThemeList;

    public GameData()
    {
        ThemeList = new List<ThemeVO>();

        for (int i = 0; i < CSVReader.GetThemeVOLength(); i++)
        {
            ThemeList.Add(new ThemeVO(CSVReader.GetThemeVO(i + 1)));
        }
    }

    public List<Sprite> GetStageSpriteList(int nThemeId)
    {
        ThemeVO theme = ThemeList.Find(a => a.Id == nThemeId);
        Sprite[] sprtArr = Resources.LoadAll<Sprite>("Image/" + theme.BackgroundSoundName);

        List<Sprite> sprtList = new List<Sprite>();

        for(int i=0;i<sprtArr.Length;i++)
        {
            sprtList.Add(sprtArr[i]);
        }

        return sprtList;
    }
}
