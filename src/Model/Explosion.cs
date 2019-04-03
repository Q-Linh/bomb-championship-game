using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

/// <summary>
/// The Explosion controls the explosions of the bombs and dynamite.
/// </summary>
namespace MyGame
{
    public class Explosion
    {
        private FieldGrid _grid;

        private List<IExplode> _explodingObjects = new List<IExplode>();

        public Explosion(FieldGrid grid)
        {
            _grid = grid;
        }

        public void CheckForExplosion()
        {
            _explodingObjects.Clear();
            IExplode explodingObject;
            foreach (GameObject gameObject in _grid.GameObjects)
            {
                if (gameObject is IExplode)
                {
                    if ((explodingObject = (gameObject as IExplode).Explode()) != null)
                        _explodingObjects.Add(explodingObject);
                }
            }
        }

        public void Exploding()
        {
            foreach (IExplode explodingObject in _explodingObjects)
            {
                int x = explodingObject.Tile.RowValue;
                int y = explodingObject.Tile.ColumnValue;
                _grid.RemoveGameObject((GameObject)explodingObject);
                HitGameObjects(x, y);
            }
        }

        public void HitGameObjects(int x, int y)
        {
            foreach (Tile t in CalculateRange(x, y))
            {
                if (t.GameObject != null)
                    t.GameObject.HitByExplosion();
            }
        }

        public List<Tile> CalculateRange(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();

            for (int i = x - 2; i <= x + 1; i++)
            {
                if ((i >= 0) && (i <= _grid.Width - 1))
                    tiles.Add(_grid.Tiles[i, y]);
            }

            for (int i = y - 1; i <= y + 1; i++)
            {
                if ((i >= 0) && (i <= _grid.Height - 1))
                    tiles.Add(_grid.Tiles[x, i]);
            }

            return tiles;
        }

        public FieldGrid Grid { get => _grid; set => _grid = value; }
        public List<IExplode> ExplodingObjects { get => _explodingObjects;}
    }
}
