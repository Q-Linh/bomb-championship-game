using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using SwinGameSDK;
using System.Runtime.ExceptionServices;
using System.Security;


/// <summary>
/// The GameController is responsible for controlling the game,
/// managing user input, and displaying the current state of the
/// game.
/// </summary>
namespace MyGame
{
    public static class GameController
    {
        private static GameState _gameState;
        private static Player _player, _player2;
        private static FieldGrid _grid;
        private static List<GameObject> _animatedObjects = new List<GameObject>();
        private static BombGame _game;
        private static int _loser;

        static GameController()
        {
            _grid = FieldGrid.GetFieldGrid();

            _player = new Player("Char1", Direction.Down);
            _player2 = new Player("Char2", Direction.Up);

            _player.Grid = _grid;
            _player2.Grid = _grid;

            _game = new BombGame(_grid, _player, _player2);

            _gameState = GameState.Menu;
        }

        public static Dictionary<string, Direction> CreateDirectionDictionary()
        {
            Dictionary<string, Direction> directions = new Dictionary<string, Direction>();
            AddDirections(directions);
            return directions;
        }

        public static void AddDirections(Dictionary<string, Direction> directions)
        {
            directions.Add("Up", Direction.Up);
            directions.Add("Down", Direction.Down);
            directions.Add("Left", Direction.Left);
            directions.Add("Right", Direction.Right);
        }
        
        public static void CheckBombs()
        { 
            _game.Explosion.CheckForExplosion();
            foreach (IExplode b in _game.Explosion.ExplodingObjects)
                UtilityFunctions.AddAnimation("ExplosionAnimation", b.Tile.RowValue, b.Tile.ColumnValue);
            _game.Explosion.Exploding();
        }

        public static void HandleUserInput ()
        {
            SwinGame.ProcessEvents();

            switch (_gameState)
            {
                case GameState.Menu:
                case GameState.Ending:
                    HandleMenuInput();
                    break;
                case GameState.Game:
                    HandleGameInput();
                    break;
            }
        }

        public static void HandleMenuInput()
        {
            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                if (UtilityFunctions.IsMouseInRectangle(UtilityFunctions.ButtonX, UtilityFunctions.ButtonY, UtilityFunctions.ButtonWidth, UtilityFunctions.ButtonHeight))
                {
                    _gameState = GameState.Game;
                    _game.StartGame();
                }
            }
        }

        public static void HandleGameInput()
        {
            if (SwinGame.KeyDown(KeyCode.WKey))
                MovePlayer(_player, Direction.Up);
            if (SwinGame.KeyDown(KeyCode.SKey))
                MovePlayer(_player, Direction.Down);
            if (SwinGame.KeyDown(KeyCode.AKey))
                MovePlayer(_player, Direction.Left);
            if (SwinGame.KeyDown(KeyCode.DKey))
                MovePlayer(_player, Direction.Right);
            if (SwinGame.KeyTyped(KeyCode.SpaceKey))
                _player.LayBomb();

            if (SwinGame.KeyDown(KeyCode.UpKey))
                MovePlayer(_player2, Direction.Up);
            if (SwinGame.KeyDown(KeyCode.DownKey))
                MovePlayer(_player2, Direction.Down);
            if (SwinGame.KeyDown(KeyCode.LeftKey))
                MovePlayer(_player2, Direction.Left);
            if (SwinGame.KeyDown(KeyCode.RightKey))
                MovePlayer(_player2, Direction.Right);
            if (SwinGame.KeyTyped(KeyCode.ReturnKey))
                _player2.LayBomb();

            UpdateGame();
        }

        private static void UpdateGame()
        {
            CheckBombs();
            if (UtilityFunctions.Animations.Count > 0)
            {
                UtilityFunctions.DrawAnimationSequence();
                _animatedObjects.Clear();
            }
            _game.RemoveDestroyedObstacle();
            _game.RedeployBombs();
            if ((_loser = _game.CheckPlayerIsKilled()) != -1)
            {
                _game.EndGame();
                _gameState = GameState.Ending;
            }
        }

        public static void AddAnimatedObject(GameObject gameObject)
        {
            _animatedObjects.Add(gameObject);
        }

        public static void MovePlayer(Player p, Direction direction)
        {
            int x = p.Tile.RowValue;
            int y = p.Tile.ColumnValue;
            p.FindNextTile(direction, ref x, ref y);
            if (_grid.CheckTileAvailable(p, x, y))
            {
                AddAnimatedObject(p);
                string imageName = p.Name + "Moving" + GetKey(direction);
                UtilityFunctions.AddAnimation(imageName, p.Tile.RowValue, p.Tile.ColumnValue, direction);   
            }
            p.Move(direction);
        }

        public static void DrawGameObjects()
        {
            foreach (GameObject gameObject in _grid.GameObjects)
            {
                if (!_animatedObjects.Exists(obj => obj == gameObject))
                {
                    string imageName = default(string);
                    float x = (gameObject.Tile.RowValue + 2) * 32;
                    float y = (gameObject.Tile.ColumnValue + 2) * 32;
                    if (gameObject is Player)
                        imageName = gameObject.Name + GetKey((gameObject as Player).Direction);
                    else
                        imageName = gameObject.Name;

                    SwinGame.DrawBitmap(GameResources.GameImage(imageName), x, y);
                }
            }
        }

        public static string GetKey(Direction direction)
        {
            foreach (string key in CreateDirectionDictionary().Keys)
            {
                if (CreateDirectionDictionary()[key] == direction)
                    return key;
            }
            return null;
        }

        public static void DrawScreen()
        {
            SwinGame.ClearScreen();

            UtilityFunctions.DrawBackground();

            if (_gameState == GameState.Game)
                DrawGameScreen();
            else
                DrawMenuScreen();

            SwinGame.RefreshScreen();
        }

        private static void DrawGameScreen()
        {
            DrawGameObjects();
            UtilityFunctions.DrawAnimations();
            UtilityFunctions.DrawBorders();
        }

        private static void DrawMenuScreen()
        {
            UtilityFunctions.DrawButton();
            UtilityFunctions.DrawObjects();
            if (_gameState == GameState.Menu)
                UtilityFunctions.DrawTitle();
            else
                UtilityFunctions.DrawWinningText(_loser);
        }
    }
}
