using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// The FieldGrid is the grid upon which all the game objects are laid.
/// </summary>
namespace MyGame
{
    public class FieldGrid
    {
        private static FieldGrid _grid;

        private const int _WIDTH = 13;

        private const int _HEIGHT = 13;

        private Tile[,] _tiles;

        private List<GameObject> _gameObjects = new List<GameObject>();

        private FieldGrid()
        {
            _tiles = new Tile[Width, Height];
            int i = 0;
            for (i = 0; i <= Width - 1; i++)
            {
                for (int j = 0; j <= Height - 1; j++)
                {
                    _tiles[i, j] = new Tile(i, j);
                }
            }
        }

        public static FieldGrid GetFieldGrid()
        {
            if (_grid == null)
                _grid = new FieldGrid();
            return _grid;
        }

        public bool AddGameObject(GameObject gameObject, int x, int y)
        {
            if (DeployGameObject(gameObject, x, y))
            {
                _gameObjects.Add(gameObject);
                return true;
            }
            return false;
        }


        public void RemoveGameObject(GameObject gameObject)
        {
            RemoveTile(gameObject.Tile);
            _gameObjects.Remove(gameObject);
        }

        public bool DeployGameObject(GameObject gameObject, int x, int y)
        {
            if (CheckTileAvailable(gameObject, x, y))
            {
                RemoveTile(gameObject.Tile);
                _tiles[x, y].GameObject = gameObject;
                return true;
            }
            return false;
        }

        public bool CheckTileAvailable(GameObject gameObject, int x, int y)
        {
            if ((x >= 0) && (x <= _WIDTH - 1) && (y >= 0) && (y <= _HEIGHT - 1))
            {
                if (_tiles[x, y].GameObject == null)
                    return true;
                else
                {
                    if (gameObject is Player)
                    {
                        if ((gameObject as Player).CheckUnactivatedBomb(_tiles[x, y]))
                        {
                            RemoveGameObject(_tiles[x, y].GameObject);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void RemoveTile(Tile tile)
        {
            if (tile != null)
                tile.Remove();
        }

        public void RandomlyDeployGameObject(GameObject gameObject)
        {
            Random random = new Random();
            int x = random.Next(12);
            int y = random.Next(12);
            while (!DeployGameObject(gameObject, x, y))
            {
                x = random.Next(12);
                y = random.Next(12);
            }               
        }

        public void RemoveAllGameObjects()
        {
            foreach (GameObject gameObject in _gameObjects.ToArray())
            {
                RemoveGameObject(gameObject);
            }
        }

        public int Width
        {
            get { return _WIDTH; }
        }
   
        public int Height
        {
            get { return _HEIGHT; }
        }

        public Tile[,] Tiles { get => _tiles; }

        public List<GameObject> GameObjects { get => _gameObjects; }
    }
}
