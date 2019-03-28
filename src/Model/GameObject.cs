using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public abstract class GameObject
    {
        private Tile _tile;

        private string _name;

        public GameObject(string name)
        {
            _name = name;
        }

        public virtual void HitByExplosion()
        { }

        public Tile Tile { get => _tile; set => _tile = value; }

        public string Name { get => _name; set => _name = value; }
    }
}
