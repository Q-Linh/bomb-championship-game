using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;
using SwinGameSDK;

/// <summary>
/// This includes a number of utility methods for
/// drawing and interacting with the Mouse.
/// </summary>
namespace MyGame
{
    static class UtilityFunctions
    {
        private static readonly int _buttonWidth = GameResources.GameImage("PlayButton").Width;
        private static readonly int _buttonHeight = GameResources.GameImage("PlayButton").Height;
        private static readonly int _buttonX = (int)((SwinGame.ScreenWidth() - _buttonWidth) / 2);
        private static readonly int _buttonY = 400;

        public static void DrawButton()
        {
            SwinGame.DrawBitmap(GameResources.GameImage("PlayButton"), _buttonX, _buttonY);
        }

        public static void DrawObjects()
        {
            DrawObjectBitmap("Char1Right", 2, 6);
            DrawObjectBitmap("Char2Left", 11, 9);
            DrawObjectBitmap("Tree", 3, 9);
            DrawObjectBitmap("Tree", 9, 5);
            DrawObjectBitmap("Tree", 4, 6);
            DrawObjectBitmap("Tree", 8, 7);
            DrawObjectBitmap("SlowBomb", 5, 9);
            DrawObjectBitmap("SlowBomb", 9, 8);
            DrawObjectBitmap("SlowBomb", 1, 8);
            DrawObjectBitmap("SlowBomb", 12, 5);
            DrawObjectBitmap("Dynamite", 3, 9);
            DrawObjectBitmap("Dynamite", 9, 7);
            DrawObjectBitmap("Dynamite", 5, 7);
            DrawObjectBitmap("Dynamite", 6, 5);
            DrawObjectBitmap("Wall", 12, 7);
            DrawObjectBitmap("Wall", 4, 6);
            DrawObjectBitmap("Wall", 8, 6);
            DrawObjectBitmap("Wall", 7, 8);
        }

        public static void DrawTitle()
        {
            SwinGame.DrawText("Bomb Championship", Color.White, GameResources.GameFont("Arial"), 90, 70);
        }

        public static void DrawWinningText(int number)
        {
            if (number != -1)
            {
                if (number == 0)
                    SwinGame.DrawText("Player 2 wins", Color.White, GameResources.GameFont("Arial"), 150, 70);
                else if (number == 1)
                    SwinGame.DrawText("Player 1 wins", Color.White, GameResources.GameFont("Arial"), 150, 70);
                else
                    SwinGame.DrawText("No player wins", Color.White, GameResources.GameFont("Arial"), 150, 70);
            }
        }

        public static void DrawObjectBitmap(string name, int rowValue, int columnValue)
        {
            int x = (rowValue + 1) * 32;
            int y = (columnValue + 1) * 32;
            SwinGame.DrawBitmap(GameResources.GameImage(name), x, y);
        }

        public static bool IsMouseInRectangle(int x, int y, int w, int h)
        {
            Point2D mouse = default(Point2D);
            bool result = false;

            mouse = SwinGame.MousePosition();

            //if the mouse is inline with the button horizontally
            if (mouse.X >= x & mouse.X <= x + w)
            {
                //Check vertical position
                if (mouse.Y >= y & mouse.Y <= y + h)
                {
                    result = true;
                }
            }

            return result;
        }



        public static void DrawBackground()
        {
            SwinGame.DrawBitmap(GameResources.GameImage("Field"), 0, 0);
        }

        public static void DrawBorders()
        {
            SwinGame.DrawBitmap(GameResources.GameImage("Horizontal"), 0, 0);
            SwinGame.DrawBitmap(GameResources.GameImage("Horizontal"), 0, 480);
            SwinGame.DrawBitmap(GameResources.GameImage("Vertical"), 0, 64);
            SwinGame.DrawBitmap(GameResources.GameImage("Vertical"), 480, 64);
        }

        private static List<Sprite> _animations = new List<Sprite>();

        public static List<Sprite> Animations { get => _animations; }

        public static int ButtonWidth => _buttonWidth;

        public static int ButtonHeight => _buttonHeight;

        public static int ButtonX => _buttonX;

        public static int ButtonY => _buttonY;

        public static void AddAnimation(string image, int rowValue, int columnValue, Direction direction)
        {
            Sprite s = default(Sprite);
            Bitmap imgObj = default(Bitmap);

            imgObj = GameResources.GameImage(image);
            imgObj.SetCellDetails(32, 32, 10, 1, 10);

            AnimationScript animation = default(AnimationScript);
            animation = SwinGame.LoadAnimationScript("moving.txt");

            s = SwinGame.CreateSprite(imgObj, animation);
            s.X = (rowValue + 2) * 32;
            s.Y = (columnValue + 2) * 32;
            switch (direction)
            {
                case Direction.Up:
                    s.DY = -1;
                    break;
                case Direction.Down:
                    s.DY = 1;
                    break;
                case Direction.Left:
                    s.DX = -1;
                    break;
                default:
                    s.DX = 1;
                    break;
            }

            s.StartAnimation("moving");
            _animations.Add(s);
        }

        public static void AddAnimation(string image, int rowValue, int columnValue)
        {
            Sprite s = default(Sprite);
            Bitmap imgObj = default(Bitmap);

            imgObj = GameResources.GameImage(image);
            imgObj.SetCellDetails(128, 32, 3, 1, 3);

            AnimationScript animation = default(AnimationScript);
            animation = SwinGame.LoadAnimationScript("exploding.txt");

            s = SwinGame.CreateSprite(imgObj, animation);
            s.X = rowValue * 32;
            s.Y = (columnValue + 2) * 32;


            s.StartAnimation("exploding");
            _animations.Add(s);
        }

        public static void UpdateAnimations()
        {
            List<Sprite> ended = new List<Sprite>();
            foreach (Sprite s in _animations)
            {
                SwinGame.UpdateSprite(s);
                if (s.AnimationHasEnded)
                {
                    ended.Add(s);
                }
            }

            foreach (Sprite s in ended)
            {
                _animations.Remove(s);
                SwinGame.FreeSprite(s);
            }
        }

        public static void DrawAnimations()
        {
            foreach (Sprite s in _animations)
            {
                if (s.animationName() == "exploding")
                {
                    if (s.CurrentCell == 2)
                        DrawVerticalExplosion(s.X + 64, s.Y - 32);
                }
                SwinGame.DrawSprite(s);
            }
        }

        public static void DrawAnimationSequence()
        {
            int ANIMATION_CELLS = 4;
            int FRAMES_PER_CELL = 8;
            int i = 0;
            for (i = 1; i <= ANIMATION_CELLS * FRAMES_PER_CELL; i++)
            {
                UpdateAnimations();
                GameController.DrawScreen();
            }

        }

        public static void DrawVerticalExplosion(float x, float y)
        {
            SwinGame.DrawBitmap(GameResources.GameImage("Explosion4"), x, y);            
        }
    }
}