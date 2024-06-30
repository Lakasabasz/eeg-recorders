using System.Numerics;
using SFML.System;

namespace Recorders.Push.Extensions;

static class VectorExtensions
{
    public static float Angle(this Vector2f vector, Vector2f? orientation)
    {
        orientation ??= new Vector2f(0, 1);
        var a = new Vector2(vector.X, vector.Y);
        var b = new Vector2(orientation.Value.X, orientation.Value.Y);

        return -(float)((Math.Atan2(b.Y, b.X) - Math.Atan2(a.Y, a.X)) * 180 / Math.PI);
    }
}