using System;
using OpenTK.Mathematics;

namespace WpfApp1.Model;

public class Normal
{
    public readonly Vector3 EndPoint;
    public readonly Vector3 StartPoint;
    public readonly float XAxisAngle;
    public readonly float YAxisAngle;
    public readonly float ZAxisAngle;


    public Normal(Vector3 rightHandPoint, Vector3 middlePoint, Vector3 leftHandPoint)
    {
        // Разница шде? (с-а)х(б-а)

        EndPoint = Vector3.Cross(rightHandPoint - middlePoint, leftHandPoint - middlePoint);
        StartPoint = middlePoint;
        var dot = Vector3.Dot(new Vector3(1.0f, 0.0f, 0.0f), Vector3.NormalizeFast(StartPoint));
        XAxisAngle = (float)(Math.Acos(dot) * 180.0 / Math.PI);
        dot = Vector3.Dot(new Vector3(0.0f, 1.0f, 0.0f), Vector3.NormalizeFast(StartPoint));
        YAxisAngle = (float)(Math.Acos(dot) * 180.0 / Math.PI);
        dot = Vector3.Dot(new Vector3(0.0f, 0.0f, 1.0f), Vector3.NormalizeFast(StartPoint));
        ZAxisAngle = (float)(Math.Acos(dot) * 180.0 / Math.PI);
    }
}