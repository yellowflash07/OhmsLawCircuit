using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    public Vector2 origin;
    public Vector2 offset;
    public GameObject pointObject;

    private List<GameObject> points = new();

    public void PlotPoint()
    {
        var point = Instantiate(pointObject, this.transform).GetComponent<RectTransform>();
        points.Add(point.gameObject);
        point.anchoredPosition = new Vector2(origin.x + VoltmeterController.voltMeterReading * offset.x,
                                             origin.y + (AmmeterController.ammeterReading) * offset.y);
    }

    public void ClearGraph()
    {
        foreach (var point in points)
        {
            Destroy(point);
        }
        points.Clear();
    }
}
