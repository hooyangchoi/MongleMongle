using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CSVReader
{
    public static string GetPointerContainerVO(int nThemaId, int nStageId)
    {
        TextAsset ta = Resources.Load<TextAsset>("CSV/PointerContainer");

        string sPointerContainer = "";

        string str = ta.text;

        string[] sArr = str.Split('\n');

        for(int i=1;i<sArr.Length;i++)
        {
            if (string.IsNullOrEmpty(sArr[i]))
                break;

            string[] sArrRow = sArr[i].Split(',');

            if(sArrRow[0] == nThemaId.ToString() && sArrRow[1] == nStageId.ToString())
            {
                sPointerContainer = sArr[i];
                break;
            }
        }

        return sPointerContainer;
    }

    public static List<string> GetPointerList(int nContainerId)
    {
        TextAsset ta = Resources.Load<TextAsset>("CSV/Pointer");

        List<string> strList = new List<string>();

        string str = ta.text;

        string[] sArr = str.Split('\n');

        for (int i = 1; i < sArr.Length; i++)
        {
            if (string.IsNullOrEmpty(sArr[i]))
                break;

            string[] sArrRow = sArr[i].Split(',');

            if (sArrRow[1] == nContainerId.ToString())
            {
                strList.Add(sArr[i]);
            }
        }

        return strList;
    }

    public static string GetThemeVO(int nId)
    {
        TextAsset ta = Resources.Load<TextAsset>("CSV/Theme");

        string sTheme = "";

        string str = ta.text;

        string[] sArr = str.Split('\n');

        for (int i = 1; i < sArr.Length; i++)
        {
            if (string.IsNullOrEmpty(sArr[i]))
                break;

            string[] sArrRow = sArr[i].Split(',');

            if (sArrRow[0] == nId.ToString())
            {
                sTheme = sArr[i];
                break;
            }
        }

        return sTheme;
    }

    public static int GetThemeVOLength()
    {
        TextAsset ta = Resources.Load<TextAsset>("CSV/Theme");

        int nCnt = 0;

        string str = ta.text;

        string[] sArr = str.Split('\n');

        for (int i = 1; i < sArr.Length; i++)
        {
            if (string.IsNullOrEmpty(sArr[i]))
                break;

            nCnt++;
        }

        return nCnt;
    }

    public static List<string> GetStageVOList(int nThemeId)
    {
        List<string> stageList = new List<string>();

        TextAsset ta = Resources.Load<TextAsset>("CSV/Stage");

        string str = ta.text;

        string[] sArr = str.Split('\n');

        for (int i = 1; i < sArr.Length; i++)
        {
            if (string.IsNullOrEmpty(sArr[i]))
                break;

            string[] sArrRow = sArr[i].Split(',');

            if (sArrRow[2] == nThemeId.ToString())
            {
                stageList.Add(sArr[i]);
            }
        }

        return stageList;
    }
}
