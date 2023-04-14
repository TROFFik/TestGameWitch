using System.Collections.Generic;
using UnityEngine;

public static class BezierCalculation
{
    public static List<Vector3> GetPoint(List<Vector3> points, float t)
    {
        List<Vector3> tempPoints = new List<Vector3>();

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 bezierPoint = Vector3.zero;

            bezierPoint = Vector3.Lerp(points[i], points[i + 1], t);

            tempPoints.Add(bezierPoint);
        }

        if (tempPoints.Count == 1)
        {
            return tempPoints;
        }
        else
        {
            return BezierCalculation.GetPoint(tempPoints, t);
        }
    }
}
