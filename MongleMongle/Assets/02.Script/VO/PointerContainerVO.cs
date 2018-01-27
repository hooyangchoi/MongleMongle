using System.Collections;
using System.Collections.Generic;

public class PointerContainerVO
{
    public int Id;
    public int Theme;
    public int Stage;

    public List<PointerVO> PointerList;
    public int PointerCount;

    public PointerContainerVO(string sPointerContainer)
    {
        string[] sArr = sPointerContainer.Split(',');

        Id = int.Parse(sArr[0]);
        Theme = int.Parse(sArr[1]);
        Stage = int.Parse(sArr[2]);

        PointerList = new List<PointerVO>();

        List<string> sArrPointer = CSVReader.GetPointerList(Id);

        for (int i = 0; i < sArrPointer.Count; i++)
        {
            PointerList.Add(new PointerVO(sArrPointer[i]));
        }

        PointerCount = PointerList.Count;
    }
}
