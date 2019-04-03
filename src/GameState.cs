
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;

/// <summary>
/// The GameStates represent the state of the Bomb Championship game play.
/// This is used to control the actions and view displayed to
/// the player.
/// </summary>
namespace MyGame
{
    public enum GameState
    {
        Menu,
        Game,
        Ending
    }
}
