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
        Set(this.ActivePiece);

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


}
