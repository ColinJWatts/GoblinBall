using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputInterface
{
    string Id { get; }
    float GetHorizontal();
    float GetVertical();
    bool GetJump();
}
