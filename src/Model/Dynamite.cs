using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Dynamite : GameObject, IExplode
    {
        private bool _isHit = false;

        public Dynamite() : base("Dynamite")
        {}

        public override void HitByExplosion()
        {
            _isHit = true;
        }

        public IExplode Explode()
        {
            if (_isHit == true)
            {
                _isHit = false;
                return this;
            }
            return null;
        }
    }
}
