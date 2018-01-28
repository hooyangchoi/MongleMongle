using System.Linq;
using UnityEngine;
using System.Collections.Generic;
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
        List<Sprite> sprtList = GameData.Inst.GetStageSpriteList(theme.Id);

        var cellData = Enumerable.Range(0, theme.StageCount)
            .Select(i => new Example03CellDto { Message = theme.StageList[i].StageName, StageId = theme.StageList[i].StageId
            , GreySprite = sprtList.Find(a=>a.name == theme.StageList[i].BackgroundName + "_640_g"), ColorSprite = sprtList.Find(a => a.name == theme.StageList[i].BackgroundName + "_640")})
           .ToList();

        scrollView.UpdateData(cellData);
    }
}
