using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputInterface
{
    float GetHorizontal();
    float GetVertical();
    bool GetJump();
}
