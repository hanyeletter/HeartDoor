using UnityEngine;

public static class RayReflection
{
    public static Vector2 Reflect(Vector2 direction, Vector2 normal)
    {
        return direction - 2f * Vector2.Dot(direction, normal) * normal;
    }
}
