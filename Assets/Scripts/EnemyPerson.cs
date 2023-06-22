using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPerson : PersonStateMachine
{
    public override void Initialize()
    {
        base.Initialize();
        _isPlayer = false;
        SetupStateMachine();
    }
}
