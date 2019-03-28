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
        /*
        public const int FIELD_TOP = 122;
        public const int FIELD_LEFT = 349;
        public const int FIELD_WIDTH = 418;

        public const int FIELD_HEIGHT = 418;

        public const int MESSAGE_TOP = 548;
        public const int CELL_WIDTH = 40;
        public const int CELL_HEIGHT = 40;

        public const int CELL_GAP = 2;

        public const int SHIP_GAP = 3;
        private static readonly Color SMALL_SEA = SwinGame.RGBAColor(6, 60, 94, 255);
        private static readonly Color SMALL_SHIP = Color.Gray;
        private static readonly Color SMALL_MISS = SwinGame.RGBAColor(1, 147, 220, 255);

        private static readonly Color SMALL_HIT = SwinGame.RGBAColor(169, 24, 37, 255);
        private static readonly Color LARGE_SEA = SwinGame.RGBAColor(6, 60, 94, 255);
        private static readonly Color LARGE_SHIP = Color.Gray;
        private static readonly Color LARGE_MISS = SwinGame.RGBAColor(1, 147, 220, 255);

        private static readonly Color LARGE_HIT = SwinGame.RGBAColor(252, 2, 3, 255);
        private static readonly Color OUTLINE_COLOR = SwinGame.RGBAColor(5, 55, 88, 255);
        private static readonly Color SHIP_FILL_COLOR = Color.Gray;
        private static readonly Color SHIP_OUTLINE_COLOR = Color.White;

        private static readonly Color MESSAGE_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);
        public const int ANIMATION_CELLS = 7;

        public const int FRAMES_PER_CELL = 8;
        /// <summary>
        /// Determines if the mouse is in a given rectangle.
        /// </summary>
        /// <param name="x">the x location to check</param>
        /// <param name="y">the y location to check</param>
        /// <param name="w">the width to check</param>
        /// <param name="h">the height to check</param>
        /// <returns>true if the mouse is in the area checked</returns>
        

        /// <summary>
        /// Draws a large field using the grid and the indicated player's ships.
        /// </summary>
        /// <param name="grid">the grid to draw</param>
        /// <param name="thePlayer">the players ships to show</param>
        /// <param name="showShips">indicates if the ships should be shown</param>
        public static void DrawField(ISeaGrid grid, Player thePlayer, bool showShips)
        {
            DrawCustomField(grid, thePlayer, false, showShips, FIELD_LEFT, FIELD_TOP, FIELD_WIDTH, FIELD_HEIGHT, CELL_WIDTH, CELL_HEIGHT,
            CELL_GAP);
        }

        /// <summary>
        /// Draws a small field, showing the attacks made and the locations of the player's ships
        /// </summary>
        /// <param name="grid">the grid to show</param>
        /// <param name="thePlayer">the player to show the ships of</param>
        public static void DrawSmallField(ISeaGrid grid, Player thePlayer)
        {
            const int SMALL_FIELD_LEFT = 39;
            const int SMALL_FIELD_TOP = 373;
            const int SMALL_FIELD_WIDTH = 166;
            const int SMALL_FIELD_HEIGHT = 166;
            const int SMALL_FIELD_CELL_WIDTH = 13;
            const int SMALL_FIELD_CELL_HEIGHT = 13;
            const int SMALL_FIELD_CELL_GAP = 4;

            DrawCustomField(grid, thePlayer, true, true, SMALL_FIELD_LEFT, SMALL_FIELD_TOP, SMALL_FIELD_WIDTH, SMALL_FIELD_HEIGHT, SMALL_FIELD_CELL_WIDTH, SMALL_FIELD_CELL_HEIGHT,
            SMALL_FIELD_CELL_GAP);
        }

        /// <summary>
        /// Draws the player's grid and ships.
        /// </summary>
        /// <param name="grid">the grid to show</param>
        /// <param name="thePlayer">the player to show the ships of</param>
        /// <param name="small">true if the small grid is shown</param>
        /// <param name="showShips">true if ships are to be shown</param>
        /// <param name="left">the left side of the grid</param>
        /// <param name="top">the top of the grid</param>
        /// <param name="width">the width of the grid</param>
        /// <param name="height">the height of the grid</param>
        /// <param name="cellWidth">the width of each cell</param>
        /// <param name="cellHeight">the height of each cell</param>
        /// <param name="cellGap">the gap between the cells</param>
        private static void DrawCustomField(ISeaGrid grid, Player thePlayer, bool small, bool showShips, int left, int top, int width, int height, int cellWidth, int cellHeight,
        int cellGap)
        {
            //SwinGame.FillRectangle(Color.Blue, left, top, width, height)

            int rowTop = 0;
            int colLeft = 0;

            //Draw the grid
            for (int row = 0; row <= 9; row++)
            {
                rowTop = top + (cellGap + cellHeight) * row;

                for (int col = 0; col <= 9; col++)
                {
                    colLeft = left + (cellGap + cellWidth) * col;

                    Color fillColor = default(Color);
                    bool draw = false;

                    draw = true;

                    switch (grid[row, col])
                    {
                        //case TileView.Ship:
                        //	draw = false;
                        //	break;
                        //If small Then fillColor = _SMALL_SHIP Else fillColor = _LARGE_SHIP
                        case TileView.Miss:
                            if (small)
                                fillColor = SMALL_MISS;
                            else
                                fillColor = LARGE_MISS;
                            break;
                        case TileView.Hit:
                            if (small)
                                fillColor = SMALL_HIT;
                            else
                                fillColor = LARGE_HIT;
                            break;
                        case TileView.Sea:
                        case TileView.Ship:
                            if (small)
                                fillColor = SMALL_SEA;
                            else
                                draw = false;
                            break;
                    }

                    if (draw)
                    {
                        SwinGame.FillRectangle(fillColor, colLeft, rowTop, cellWidth, cellHeight);
                        if (!small)
                        {
                            SwinGame.DrawRectangle(OUTLINE_COLOR, colLeft, rowTop, cellWidth, cellHeight);
                        }
                    }
                }
            }

            if (!showShips)
            {
                return;
            }

            int shipHeight = 0;
            int shipWidth = 0;
            string shipName = null;

            //Draw the ships
            foreach (Ship s in thePlayer)
            {
                if (s == null || !s.IsDeployed)
                    continue;
                rowTop = top + (cellGap + cellHeight) * s.Row + SHIP_GAP;
                colLeft = left + (cellGap + cellWidth) * s.Column + SHIP_GAP;

                if (s.Direction == Direction.LeftRight)
                {
                    shipName = "ShipLR" + s.Size;
                    shipHeight = cellHeight - (SHIP_GAP * 2);
                    shipWidth = (cellWidth + cellGap) * s.Size - (SHIP_GAP * 2) - cellGap;
                }
                else
                {
                    //Up down
                    shipName = "ShipUD" + s.Size;
                    shipHeight = (cellHeight + cellGap) * s.Size - (SHIP_GAP * 2) - cellGap;
                    shipWidth = cellWidth - (SHIP_GAP * 2);
                }

                if (!small)
                {
                    SwinGame.DrawBitmap(GameResources.GameImage(shipName), colLeft, rowTop);
                }
                else
                {
                    SwinGame.FillRectangle(SHIP_FILL_COLOR, colLeft, rowTop, shipWidth, shipHeight);
                    SwinGame.DrawRectangle(SHIP_OUTLINE_COLOR, colLeft, rowTop, shipWidth, shipHeight);
                }
            }
        }


        private static string _message;
        /// <summary>
        /// The message to display
        /// </summary>
        /// <value>The message to display</value>
        /// <returns>The message to display</returns>
        public static string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Draws the message to the screen
        /// </summary>
        public static void DrawMessage()
        {
            SwinGame.DrawText(Message, MESSAGE_COLOR, GameResources.GameFont("Courier"), FIELD_LEFT, MESSAGE_TOP);
        }

        /// <summary>
        /// Draws the background for the current state of the game
        /// </summary>
        /// <remarks>
        /// Isuru: Updated Draw frame rate function;
        /// </remarks>

        public static void DrawBackground()
        {
            switch (GameController.CurrentState)
            {
                case GameState.ViewingMainMenu:
                case GameState.ViewingGameMenu:
                case GameState.AlteringSettings:
                case GameState.ViewingHighScores:
                    SwinGame.DrawBitmap(GameResources.GameImage("Menu"), 0, 0);
                    break;
                case GameState.Discovering:
                case GameState.EndingGame:
                    SwinGame.DrawBitmap(GameResources.GameImage("Discovery"), 0, 0);
                    break;
                case GameState.Deploying:
                    SwinGame.DrawBitmap(GameResources.GameImage("Deploy"), 0, 0);
                    break;
                default:
                    SwinGame.ClearScreen();
                    break;
            }
            SwinGame.DrawFramerate(675, 585);
            //SwinGame.DrawFramerate(675, 585, GameResources.GameFont("CourierSmall"));
        }
        */

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