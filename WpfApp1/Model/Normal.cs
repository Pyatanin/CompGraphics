using System;
using OpenTK.Mathematics;

namespace WpfApp1.Model;

public class Normal
{
    public readonly Vector3 EndPoint;
    public readonly Vector3 EndPointNormirovan;

    public readonly Vector3 StartPoint;
    public readonly float XAxisAngle;
    public readonly float YAxisAngle;
    public readonly float ZAxisAngle;


    public Normal(Vector3 rightHandPoint, Vector3 middlePoint, Vector3 leftHandPoint)
    {
        var CrossVect = Vector3.Cross(rightHandPoint - middlePoint, leftHandPoint - middlePoint);
        EndPoint = middlePoint + CrossVect;
        StartPoint = middlePoint;

        EndPointNormirovan = middlePoint + new Vector3(CrossVect.X / CrossVect.Length, CrossVect.Y / CrossVect.Length,
            CrossVect.Z / CrossVect.Length);
        var dot = Vector3.Dot(new Vector3(1.0f, 0.0f, 0.0f), Vector3.Normalize(EndPoint - StartPoint));
        // мейби +90?
        XAxisAngle = (float)(Math.Acos(dot) * 180.0 / Math.PI) - 90;
        dot = Vector3.Dot(new Vector3(0.0f, 1.0f, 0.0f), Vector3.Normalize(EndPoint - StartPoint));
        YAxisAngle = (float)(Math.Acos(dot) * 180.0 / Math.PI) - 90;
        dot = Vector3.Dot(new Vector3(0.0f, 0.0f, 1.0f), Vector3.Normalize(EndPoint - StartPoint));
        ZAxisAngle = (float)(Math.Acos(dot) * 180.0 / Math.PI);
    }
}