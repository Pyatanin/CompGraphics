using OpenTK.Mathematics;

namespace WpfApp1.Model;

public class Normal
{
    public readonly Vector3 EndPoint;
    public readonly Vector3 StartPoint;
    
    public Normal(Vector3 rightHandPoint, Vector3 middlePoint, Vector3 leftHandPoint)
    {
        var crossVect = Vector3.Cross(rightHandPoint - middlePoint, leftHandPoint - middlePoint);
        StartPoint = middlePoint;

        EndPoint = middlePoint + Vector3.Normalize(crossVect);
    }
}