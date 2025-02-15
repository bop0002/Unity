using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    public Board Board { get; private set; }
    public Vector3Int Position { get; private set; }

    public TetrominoData TetroData { get; private set; }

    public Vector3Int[] Cells { get; private set; }
    public int RotationIndex { get; private set; }

    public float stepDelay = 1f;
    public float moveDelay = 0.1f;
    public float lockDelay = 0.5f;

    private float stepTime;
    private float moveTime;
    private float lockTime;

    public void Inititalze(Board board, Vector3Int position, TetrominoData data)
    {
        this.Board = board;
        this.Position = position;
        this.TetroData = data;

        RotationIndex = 0;

        stepTime = Time.time + stepDelay;
        moveTime = Time.time + moveDelay;
        lockTime = 0f;

        if (this.Cells == null) this.Cells = new Vector3Int[data.Cells.Length];

        for (int i = 0; i < this.Cells.Length; i++)
        {
            this.Cells[i] = (Vector3Int)data.Cells[i];
        }
    }

    private void Update()
    {

        this.Board.Clear(this);

        lockTime += Time.deltaTime;


        if (Time.time > moveTime)
        {
            HandleInput();
        }
        if (Time.time > stepTime)
        {
            Step();
        }

        this.Board.Set(this);
    }

    private void HandleInput()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            Rotate(1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }
    }

    private void HardDrop()
    {
        while(Move(Vector2Int.down))
            {
                continue;
            }
    }

    private void Step()
    {
        stepTime = Time.time + stepDelay;

        Move(Vector2Int.down);

        if (lockTime >= lockDelay)
        {
            Lock();
        }
    }

    private void Lock()
    {
        Board.Set(this);
        Board.ClearLines();
        Board.SpawnPiece();
    }

    private bool Move(Vector2Int transalation)
    {
        Vector3Int newPosition = this.Position;
        newPosition.x += transalation.x;
        newPosition.y += transalation.y;

        bool valid = this.Board.IsValidPosition(this,newPosition);

        //Debug.Log(newPosition);

        if(valid)
        {
            this.Position = newPosition;
            moveTime = Time.time + moveDelay;
            lockTime = 0f;
        }
        return valid;

    }

    private void Rotate(int direction)
    {

        int originalRotation = RotationIndex;

        this.RotationIndex = Wrap(this.RotationIndex + direction, 0, 4);
        HandleRotationMatrix(direction);

        if (!TestWallKicks(RotationIndex, direction))
        {
            RotationIndex = originalRotation;
            HandleRotationMatrix(-direction);
        }
    }

    private void HandleRotationMatrix(int direction)
    {
        for (int i = 0; i < this.Cells.Length; i++)
        {
            Vector3 cell = this.Cells[i];
            int x; int y;

            switch (this.TetroData.tetromino)
            {
                case Tetromino.shapeI:
                case Tetromino.shapeO:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1]) * direction);
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3]) * direction);
                    break;

                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1]) * direction);
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3]) * direction);
                    break;
            }

            this.Cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < TetroData.WallKicks.GetLength(1); i++)
        {
            Vector2Int translation = TetroData.WallKicks[wallKickIndex, i];

            if (Move(translation))
            {
                return true;
            }
        }

        return false;
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;

        if (rotationDirection < 0)
        {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, TetroData.WallKicks.GetLength(0), 0);
    }


    private int Wrap(int input, int max,int min)
    {
        if(input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min +(input - min) % (max - min);
        }
    }
}
