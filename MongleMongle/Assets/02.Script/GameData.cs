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
}
