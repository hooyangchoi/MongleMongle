using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMaker : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
    {
        PointerContainerVO pointerContainer = new PointerContainerVO(CSVReader.GetPointerContainerVO(GameController.Inst.ThemeId, GameController.Inst.StageId));

        List<GameObject> objList = new List<GameObject>();

        Debug.Log(pointerContainer.PointerCount);

        for (int i = 0; i < pointerContainer.PointerCount; i++)
        {
            GameObject obj = Resources.Load<GameObject>("Pointer/" + pointerContainer.Theme.ToString());

            obj = Instantiate(obj);
            obj.name = "pointer:" + pointerContainer.PointerList[i].Id;
            obj.transform.position = pointerContainer.PointerList[i].Position;

            objList.Add(obj);
        }

        StageController.Inst.SetContainer(pointerContainer);
        StageController.Inst.SetObjectList(objList);

        StageController.Inst.RecognizeNextPointer(pointerContainer.PointerList.Find(a => a.StartPointerId == 0).Id);
    }
}
