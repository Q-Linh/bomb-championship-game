
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data;
using System.Diagnostics;

/// <summary>
/// The Tile knows its location on the grid and knows whether it is occupied and who is the occupant. 
/// </summary>
namespace MyGame
{
    public class Tile
    {
        private readonly int _rowValue;
        private readonly int _columnValue;
        private GameObject _gameObject = null;

        public Tile(int row, int col)
        {
            _rowValue = row;
            _columnValue = col;
        }

        public int RowValue
        {
            get { return _rowValue; }
        }

        public int ColumnValue
        {
            get { return _columnValue; }
        }

        public GameObject GameObject
        {
            get { return _gameObject; }
            set
            {
                if (_gameObject == null)
                {
                    _gameObject = value;
                    if (_gameObject != null)
                        _gameObject.Tile = this;
                }
            }
        }

        public void Remove()
        {
            _gameObject.Tile = null;
            _gameObject = null;
        }
    }
}
