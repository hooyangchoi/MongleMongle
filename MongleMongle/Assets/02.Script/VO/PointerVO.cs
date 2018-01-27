using UnityEngine;

public class PointerVO
{
    public int Id;
    public Vector3 Position;

    public int StartPointerId;
    public int EndPointerId;

    public PointerVO(string sPointer)
    {
        string[] sArr = sPointer.Split(',');

        Id = int.Parse(sArr[0]);
        Position.x = float.Parse(sArr[2]);
        Position.y = float.Parse(sArr[3]);
        Position.z = float.Parse(sArr[4]);

        StartPointerId = int.Parse(sArr[5]);

        EndPointerId = int.Parse(sArr[6]);
    }
}
