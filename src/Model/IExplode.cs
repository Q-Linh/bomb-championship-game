using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Interface used for objects which can explode. 
/// </summary>
namespace MyGame
{
    public interface IExplode
    {
        IExplode Explode();

        Tile Tile
        {
            get;
        }
    }
}
