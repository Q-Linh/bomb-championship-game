using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The Obstacle is a type of game object that prevents the players from passing the tile. 
/// </summary>
namespace MyGame
{
    public class Obstacle : GameObject
    {
        private ObstacleKind _obstacleKind;

        private bool _isDestroyed = false;

        public Obstacle(string name, ObstacleKind obstacleKind) : base(name)
        {
            _obstacleKind = obstacleKind;
        }

        public bool IsDestroyed { get => _isDestroyed; set => _isDestroyed = value; }

        public override void HitByExplosion()
        {
            if (_obstacleKind == ObstacleKind.Tree)
            {
                _isDestroyed = true;
            }
        }
    }
}
