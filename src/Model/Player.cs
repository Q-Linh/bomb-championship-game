using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player : GameObject
    {
        private Stack<Bomb> _bombs = new Stack<Bomb>();

        private Direction _direction;

        private FieldGrid _grid;

        private bool _isKilled = false;

        public Player(string name,Direction direction) : base(name) 
        {
            _direction = direction;
        }

        public FieldGrid Grid { get => _grid; set => _grid = value; }

        
        public void GetBomb(Bomb bomb)
        {
            _bombs.Push(bomb);
        }
        
        public void LayBomb()
        {
            if (_bombs.Count > 0)
            {
                Bomb bomb = _bombs.Pop();
                Tile currentTile = Tile;
                MoveAfterLayingBomb();
                _grid.AddGameObject(bomb, currentTile.RowValue, currentTile.ColumnValue);
                bomb.Activate();
            }
        }

        public void MoveAfterLayingBomb()
        {
            bool moveSuccessful = Move(Direction);
            while (!moveSuccessful)
                moveSuccessful = MoveInRandomDirection();
        }

        public bool Move(Direction direction)
        {
            int x = Tile.RowValue;
            int y = Tile.ColumnValue;

            FindNextTile(direction, ref x, ref y);

            Direction = direction;

            return _grid.DeployGameObject(this, x, y);
        }

        public override void HitByExplosion()
        {
            _isKilled = true;
        }

        public void FindNextTile(Direction direction, ref int x, ref int y)
        {
            switch (direction)
            {
                case Direction.Up:
                    --y;
                    break;
                case Direction.Down:
                    ++y;
                    break;
                case Direction.Left:
                    --x;
                    break;
                case Direction.Right:
                    ++x;
                    break;
            }
        }

        public bool CheckUnactivatedBomb(Tile tile)
        {
            if (tile.GameObject is Bomb)
            {
                if (!(tile.GameObject as Bomb).IsActivated)
                {
                    GetBomb(tile.GameObject as Bomb);
                    return true;
                }
            }
            return false;
        }

        public bool MoveInRandomDirection()
        {
            Random random = new Random();
            return Move((Direction)random.Next(4));
        }

        public Direction Direction { get => _direction; set => _direction = value; }

        public Stack<Bomb> Bombs { get => _bombs; set => _bombs = value; }

        public bool IsKilled { get => _isKilled; set => _isKilled = value; }
    }
}
