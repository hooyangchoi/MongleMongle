using System.Linq;
using UnityEngine;

public class Example03Scene : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Extensions.Examples.Example03ScrollView scrollView;

    void Start()
    {
        var cellData = Enumerable.Range(0, 5)
            .Select(i => new UnityEngine.UI.Extensions.Examples.Example03CellDto { Message = "Cell " + i })
            .ToList();

        scrollView.UpdateData(cellData);
    }
}
