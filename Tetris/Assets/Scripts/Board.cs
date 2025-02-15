using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Board : MonoBehaviour
{
    public Tilemap TileMap { get; private set; }
    public TetrominoData[] tetrominos;
    public Pieces ActivePiece { get; private set; }

    [SerializeField] Vector3Int SpawnPosition;

    public Vector2Int BOARD_SIZE = new Vector2Int(10, 20);

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.BOARD_SIZE.x/2, -this.BOARD_SIZE.y/2);
            return new RectInt(position, BOARD_SIZE);
        }
    }

    private void Awake()
    {
        this.TileMap = GetComponentInChildren<Tilemap>();
        this.ActivePiece = GetComponentInChildren<Pieces>();

        for(int i = 0; i < tetrominos.Length; i++)
        {
            this.tetrominos[i].Initialize();
        }
            
    }

    private void Start()
    {
        SpawnPiece();
    }

    public void SpawnPiece()
    {
        int random = Random.Range(0,this.tetrominos.Length);
        TetrominoData data = this.tetrominos[random];

        this.ActivePiece.Inititalze(this, this.SpawnPosition, data);

        if(IsValidPosition(ActivePiece,SpawnPosition))
        {
            Set(this.ActivePiece);
        }
        else
        {
            GameOver();
        }

    }

    public void GameOver()
    {
        this.TileMap.ClearAllTiles();
    }

    public void Set(Pieces piece)
    {
        for(int i = 0; i < piece.Cells.Length;i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + piece.Position;
            this.TileMap.SetTile(tilePosition,piece.TetroData.tiles);
        }
    }

    public void Clear(Pieces piece)
    {
        for(int i = 0;  i < piece.Cells.Length;i++)
        {
            Vector3Int position = piece.Cells[i] + piece.Position;
            this.TileMap.SetTile(position, null);
        }
    }

    public bool IsValidPosition(Pieces piece,Vector3Int position)
    {

        RectInt bound = this.Bounds;

        for(int i = 0; i < piece.Cells.Length;i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + position;

            if(!bound.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            if (this.TileMap.HasTile(tilePosition))
            {
                return false;
            }

        }
        return true;

    }
    public void ClearLines()
    {
        RectInt bounds = Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
            }
            else
            {
                row++;
            }
        }
    }

    public bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            if (!TileMap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }

    public void LineClear(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            TileMap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = TileMap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                TileMap.SetTile(position, above);
            }

            row++;
        }
    }


}
