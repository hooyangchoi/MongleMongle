﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeVO
{
    public int Id;
    public int ThemeType;

    public string ThemeName;
    public int StageCount;
    public string BackgroundSoundName;

    public List<StageVO> StageList;

    public ThemeVO(string sTheme)
    {
        string[] sArr = sTheme.Split(',');

        Id = int.Parse(sArr[0]);
        ThemeType = int.Parse(sArr[1]);
        ThemeName = sArr[2];
        StageCount = int.Parse(sArr[3]);
        BackgroundSoundName = "";
        
        for(int i=0;i<sArr[4].Length - 1;i++)
        {
            BackgroundSoundName += sArr[4][i].ToString();
        } 

        StageList = new List<StageVO>();

        List<string> sStageList = CSVReader.GetStageVOList(Id);

        for (int i = 0; i < sStageList.Count; i++)
        {
            StageList.Add(new StageVO(sStageList[i]));
        }

        
    }
}
