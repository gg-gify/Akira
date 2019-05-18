using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AkiraInput
{
    public abstract Vector2 GetPad();
    public abstract bool GetButtonA();
    public abstract bool GetButtonB();
}
