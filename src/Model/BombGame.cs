using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class BombGame
    {
        private const int _NUMBER_OF_BOMB = 15;
        private const int _NUMBER_OF_DYNAMITE = 15;
        private const int _NUMBER_OF_TREE = 25;
        private const int _NUMBER_OF_WALL = 25;

        private Random _random = new Random();

        private FieldGrid _grid;

        private Player[] _players = new Player[2];

        private Explosion _explosion;

        private List<GameObject> _gameObjects = new List<GameObject>();

        public BombGame(FieldGrid grid, Player player1, Player player2)
        {
            _grid = grid;
            _explosion = new Explosion(grid);
            _players[0] = player1;
            _players[1] = player2;
            CreateGameObjects();
        }

        public Explosion Explosion { get => _explosion; }

        public void CreateGameObjects()
        {
            for (int i = 0; i < _NUMBER_OF_BOMB - 2; i++)
                _gameObjects.Add(new Bomb("UnactivatedBomb", (BombKind)_random.Next(3)));

            for (int i = 0; i < _NUMBER_OF_DYNAMITE; i++)
                _gameObjects.Add(new Dynamite());

            for (int i = 0; i < _NUMBER_OF_TREE; i++)
                _gameObjects.Add(new Obstacle("Tree", ObstacleKind.Tree));

            for (int i = 0; i < _NUMBER_OF_WALL; i++)
                _gameObjects.Add(new Obstacle("Wall", ObstacleKind.Wall));
        }

        public void GenerateMap()
        {
            _grid.AddGameObject(_players[0], 0, 0);
            _grid.AddGameObject(_players[1], _grid.Width - 1, _grid.Height - 1);
            
            _grid.AddGameObject(new Bomb("UnactivatedBomb", (BombKind)_random.Next(3)), 0, 1);
            _grid.AddGameObject(new Bomb("UnactivatedBomb", (BombKind)_random.Next(3)), _grid.Width - 1, _grid.Height - 2);
            foreach (GameObject gameObject in _gameObjects)
            {
                while (!_grid.AddGameObject(gameObject, _random.Next(1, 12), _random.Next(1, 12))) ;
            }
        }

        public void RedeployBombs()
        {
            foreach (IExplode explodingObject in _explosion.ExplodingObjects)
            {
                if (explodingObject is Bomb)
                {
                    (explodingObject as Bomb).IsActivated = false;
                    (explodingObject as Bomb).ChangeName();
                    while (!_grid.AddGameObject((explodingObject as Bomb), _random.Next(1, 12), _random.Next(1, 12))) ;
                }
            }
        }

        public void RemoveDestroyedObstacle()
        {
            foreach (GameObject gameObject in _grid.GameObjects.ToList())
            {
                if (gameObject is Obstacle)
                {
                    if ((gameObject as Obstacle).IsDestroyed == true)
                    {
                        (gameObject as Obstacle).IsDestroyed = false;
                        _grid.RemoveGameObject(gameObject);
                    }
                }
            }
        }

        public int CheckPlayerIsKilled()
        {
            int count = 0;
            int index = -1;
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].IsKilled)
                {
                    count++;
                    index = i;
                }
            }
            if (count < _players.Length)
                return index;
            else
                return -2;
        }

        public void StartGame()
        {
            GenerateMap();
        }

        
        public void EndGame()
        {
            foreach (Player p in _players)
            {
                p.IsKilled = false;
            }
            _grid.RemoveAllGameObjects();
        }
        
    }
}
