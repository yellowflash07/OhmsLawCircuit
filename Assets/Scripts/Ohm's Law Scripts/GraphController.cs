using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    public Vector2 origin;
    public Vector2 offset;
    public Vector2Int grid;
    public Vector2 graphStep;
    public GameObject pointObject, xLine, yLine, xParent, yParent;

    private Dictionary<GameObject, Vector2> points = new();
    private List<GameObject> xLines = new();
    private List<GameObject> yLines = new();


    private void Start()
    {
        GenerateGraph(grid.x, grid.y);
    }

    public void ChangeX(int val)
    {
        if (grid.x + val < 1) return;
        grid.x += val;
        GenerateGraph(grid.x, grid.y);
    }

    public void ChangeY(int val)
    {
        if (grid.y + val < 1) return;
        grid.y += val;
        GenerateGraph(grid.x, grid.y);
    }

    public void GenerateGraph(int x, int y)
    {
        if(xLines.Count > 0)
        {
            for (int i = 0; i < xLines.Count; i++)
            {
                Destroy(xLines[i]);
            }
            xLines.Clear();
        }
        if (yLines.Count > 0)
        {
            for (int i = 0; i < yLines.Count; i++)
            {
                Destroy(yLines[i]);
            }
            yLines.Clear();
        }

        for (int i = 0; i < x; i++)
        {
            var xl = Instantiate(xLine, xParent.transform);
            xl.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (graphStep.x * i).ToString();
            xLines.Add(xl);
        }
        for (int i = 0; i < y; i++)
        {
            var yl = Instantiate(yLine, yParent.transform);
            yl.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (graphStep.y * i).ToString();
            yLines.Add(yl);

        }

        UpdatePlottedPoints();
    }

    public void PlotPoint()
    {
        offset.x = 520 / (grid.x);
        offset.y = 520 / (grid.y);

        var vtReading = VoltmeterController.voltMeterReading;
        var amReading = AmmeterController.ammeterReading / 100;

        var vtPoint = origin.y + vtReading * offset.y * 2;
        var amPoint = origin.x + amReading * offset.x * 2;

        var point = Instantiate(pointObject, this.transform).GetComponent<RectTransform>();
        points.Add(point.gameObject, new Vector2(amReading, vtReading));

        point.anchoredPosition = new Vector2(amPoint, vtPoint);

        point.gameObject.SetActive(amPoint < grid.x * offset.x && vtPoint < grid.y * offset.y);

    }

    public void UpdatePlottedPoints()
    {
        if (points.Count <= 0) return;
        offset.x = 520 / (grid.x);
        offset.y = 520 / (grid.y);

        foreach (var point in points)
        {
            var vtPoint = origin.y + point.Value.y * offset.y * 2;
            var amPoint = origin.x + point.Value.x * offset.x * 2;
            point.Key.GetComponent<RectTransform>().anchoredPosition = new Vector2(amPoint,
                                                                                   vtPoint);
            //enable point if its in range, disable if not
            point.Key.SetActive(amPoint < grid.x * offset.x && vtPoint < grid.y * offset.y);

        }
    }

    public void ClearGraph()
    {
        foreach (var point in points)
        {
            Destroy(point.Key);
        }
        points.Clear();
    }
}
