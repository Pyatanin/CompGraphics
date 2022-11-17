using System.Collections.Generic;
using System.Linq;

namespace WpfApp1.Model;

public class Figura3D
{
    public List<float> Footing;
    public List<List<float>> Grans = new List<List<float>>();
    public List<float> Replication;
    public List<float> Rotate;
    public List<float> Scale;
    public List<float> Tirag;

    public Figura3D(List<float> footing2D, List<float> tirag, List<float> rotate, List<float> scale)
    {
        Tirag = tirag;
        Rotate = rotate;
        Scale = scale;

        Footing = new List<float>();
        Replication = new List<float>();
        if (tirag.Last() < 0)
        {
            for (int i = 0; i < footing2D.Count; i += 2)
            {
                Footing.Add(scale[0] * footing2D[i]);
                Footing.Add(scale[1] * footing2D[i + 1]);
                Footing.Add(0);
            }

            for (int i = 0; i < footing2D.Count; i += 2)
            {
                Replication.Add(scale[0] * (footing2D[i] + tirag[0]));
                Replication.Add(scale[1] * (footing2D[i + 1] + tirag[1]));
                Replication.Add(scale[2] * tirag[2]);
            }
        }
        else
        {
            for (int i = -1 + footing2D.Count; i >= 0; i -= 2)
            {
                Footing.Add(scale[0] * footing2D[i]);
                Footing.Add(scale[1] * footing2D[i + 1]);
                Footing.Add(0);
            }

            for (int i = 0; i < footing2D.Count / 2; i += 2)
            {
                Replication.Add(scale[0] * (footing2D[i] + tirag[0]));
                Replication.Add(scale[1] * (footing2D[i + 1] + tirag[1]));
                Replication.Add(scale[2] * tirag[2]);
            }
        }

        for (int i = 0; i < footing2D.Count / 2 - 1; i++)
        {
            Grans.Add(new List<float>());
            Grans[i].Add(scale[0] * footing2D[2 * i]);
            Grans[i].Add(scale[1] * footing2D[2 * i + 1]);
            Grans[i].Add(0);

            Grans[i].Add(scale[0] * footing2D[2 * i + 2]);
            Grans[i].Add(scale[1] * footing2D[2 * i + 3]);
            Grans[i].Add(0);

            Grans[i].Add(scale[0] * footing2D[2 * i + 2] + tirag[0]);
            Grans[i].Add(scale[1] * footing2D[2 * i + 3] + tirag[1]);
            Grans[i].Add(scale[2] * tirag[2]);

            Grans[i].Add(scale[0] * footing2D[2 * i] + tirag[0]);
            Grans[i].Add(scale[1] * footing2D[2 * i + 1] + tirag[1]);
            Grans[i].Add(scale[2] * tirag[2]);
        }

        Grans.Add(new List<float>());
        Grans.Last().Add(scale[0] * footing2D[0]);
        Grans.Last().Add(scale[1] * footing2D[1]);
        Grans.Last().Add(0);

        Grans.Last().Add(scale[0] * footing2D[^2]);
        Grans.Last().Add(scale[1] * footing2D[^1]);
        Grans.Last().Add(0);

        Grans.Last().Add(scale[0] * footing2D[^2] + tirag[0]);
        Grans.Last().Add(scale[1] * footing2D[^1] + tirag[1]);
        Grans.Last().Add(scale[2] * tirag[2]);

        Grans.Last().Add(scale[0] * footing2D[0] + tirag[0]);
        Grans.Last().Add(scale[1] * footing2D[1] + tirag[1]);
        Grans.Last().Add(scale[2] * tirag[2]);
    }
}