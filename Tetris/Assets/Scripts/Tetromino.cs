using System.Numerics;
using UnityEngine.Tilemaps;
using UnityEngine;

public enum Tetromino
{
    shapeI,
    shapeO,
    shapeT,
    shapeJ,
    shapeL,
    shapeS,
    shapeZ
}

[System.Serializable]
public struct TetrominoData
{
    public Tetromino tetromino;
    public Tile tiles;
    public Vector2Int[] Cells { get; private set; } 
    public Vector2Int[,] WallKicks  { get; private set; }

    public void Initialize()
    {
        this.Cells = Data.cells[this.tetromino];
        this.WallKicks = Data.wallKicks[this.tetromino];
    }


}
