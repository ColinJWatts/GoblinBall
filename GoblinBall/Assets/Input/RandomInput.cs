using UnityEngine;
using System.Collections;

public class RandomInput : IInputInterface
{
    public string Id
    {
        get
        {
            return "RandomInput";
        }
    }

    public bool GetGrab()
    {
        return Random.Range(-1.0f, 100.0f) < 0;
    }

    public float GetHorizontal()
    {
        return Random.Range(-1.0f, 1.0f);
    }

    public bool GetJump()
    {
        return Random.Range(-1.0f, 100.0f) < 0;
    }

    public float GetVertical()
    {
        return Random.Range(-1.0f, 1.0f);
    }
}

