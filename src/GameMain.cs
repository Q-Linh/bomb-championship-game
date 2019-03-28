using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;
using System.Runtime.ExceptionServices;
using System.Security;

namespace MyGame
{
    static class GameMain
    {
        public static int Main ()
        {
            //Opens a new Graphics Window
            SwinGame.OpenGraphicsWindow ("Bomb Championship", 544, 544);

            //Load Resources
            GameResources.LoadResources ();

            //Game Loop
            do 
            {
                GameController.HandleUserInput ();
                GameController.DrawScreen();
            } 
            while (!(SwinGame.WindowCloseRequested () == true));        

            //Free Resources.        
            GameResources.FreeResources ();

            return 0;
        }
    }
}
