using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageVO
{
    public int Id;
    public int StageId;
    public int ThemeId;
    public string StageName;

    public StageVO(string sStage)
    {
        string[] sArr = sStage.Split(',');

        Id = int.Parse(sArr[0]);
        StageId = int.Parse(sArr[1]);
        ThemeId = int.Parse(sArr[2]);
        StageName = sArr[3];
    }
}
