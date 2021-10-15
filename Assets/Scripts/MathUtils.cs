using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{
    public static float RayPointDistance(Ray2D ray, Vector2 point, out float projectionOnRay)
    {
        projectionOnRay = Vector2.Dot(ray.direction, point - ray.origin);
        return (ray.GetPoint(projectionOnRay) - point).magnitude;
    }

    public static float GetLinearSpeed(float anglePerSecond, float radius)
    {
        return anglePerSecond * radius;
    }
}
