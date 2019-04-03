using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

/// <summary>
/// The Bomb is a type of game object that will explode after a specific amount of time. 
/// </summary>
namespace MyGame
{
    public class Bomb : GameObject, IExplode
    {
        private BombKind _bombKind;

        private bool _isActivated = false;

        private Timer _timer = new Timer();

        private int _timeBeforeExploding;  

        public Bomb(string name, BombKind bombKind) : base(name)
        {
            _bombKind = bombKind;
            _timeBeforeExploding = ((int)_bombKind + 1) * 2;
        }

        public bool IsActivated { get => _isActivated; set => _isActivated = value; }

        public Timer Timer { get => _timer; set => _timer = value; }

        public void Activate()
        {
            _timer.Start();
            _isActivated = true;
            ChangeName();
        }

        public IExplode Explode()
        {
            if (_isActivated)
            {
                if (_timer.Ticks >= (_timeBeforeExploding * 1000))
                {
                    _timer.Stop();
                    return this;
                }
            }
            return null;
        }

        public void ChangeName()
        {
            if (_isActivated)
            {
                switch (_bombKind)
                {
                    case BombKind.FastBomb:
                        Name = "FastBomb";
                        break;
                    case BombKind.MediumBomb:
                        Name = "MediumBomb";
                        break;
                    case BombKind.SlowBomb:
                        Name = "SlowBomb";
                        break;
                }
            }
            else
                Name = "UnactivatedBomb";
        }
    }
}
