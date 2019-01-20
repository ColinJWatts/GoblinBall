using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : CharacterBase
{
    public override void CharacterUpdate()
    {
        if (_health > 0)
        {
            _health--;
        }
    }
}
