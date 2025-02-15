using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public Tile tile;
    public Board mainBoard;
    public Pieces trackingPiece;

    public Tilemap tilemap { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public Vector3Int Position { get; private set; }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        Cells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }
    public void Clear()
    {
        for (int i = 0; i < this.Cells.Length; i++)
        {
            Vector3Int position = this.Cells[i] + this.Position;
            this.tilemap.SetTile(position, null);
        }
    }

    public void Set()
    {
        for (int i = 0; i < this.Cells.Length; i++)
        {
            Vector3Int tilePosition = this.Cells[i] + this.Position;
            this.tilemap.SetTile(tilePosition, this.tile);
        }
    }
    private void Copy()
    {
        for (int i = 0; i < this.Cells.Length; i++)
        {
            Cells[i] = trackingPiece.Cells[i];
        }
    }

    private void Drop()
    {
        Vector3Int position = trackingPiece.Position;

        int current = position.y;
        int bottom = -mainBoard.BOARD_SIZE.y / 2 - 1;

        mainBoard.Clear(trackingPiece);

        for (int row = current; row >= bottom; row--)
        {
            position.y = row;

            if (mainBoard.IsValidPosition(trackingPiece, position))
            {
                this.Position = position;
            }
            else
            {
                break;
            }
        }

        mainBoard.Set(trackingPiece);
    }


}
