using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions.Examples;

public class InfinitiButton : MonoBehaviour {

    [SerializeField]
    Example03ScrollView scrollView;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        ThemeVO theme = GameData.Inst.ThemeList.Find(a => a.Id == GameController.Inst.ThemeId);

        var cellData = Enumerable.Range(0, theme.StageCount)
            .Select(i => new Example03CellDto { Message = theme.StageList[i].StageName })
            .ToList();

        scrollView.UpdateData(cellData);
    }
}
