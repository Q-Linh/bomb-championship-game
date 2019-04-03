using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Different kinds of bomb based on the time it takes to explode after activated.
/// </summary>
namespace MyGame
{
    public enum BombKind
    {
        FastBomb,
        MediumBomb,
        SlowBomb,
    }
}
