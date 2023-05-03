using System;
using System.Collections.Generic;

class Board
{
    private Piece[,] squares;
    private List<Piece> pieces;

    public Board()
    {
        squares = new Piece[8, 8];
        pieces = new List<Piece>();
    }

    public void Draw()
    {
        Console.WriteLine("  a b c d e f g h ");
        Console.WriteLine(" +-----------------+");
        for (int i = 0; i < 8; i++)
        {
            Console.Write((8 - i) + "|");
            for (int j = 0; j < 8; j++)
            {
                if (squares[i, j] == null)
                {
                    Console.Write(" |");
                }
                else
                {
                    Console.Write(squares[i, j].Symbol + "|");
                }
            }
            Console.WriteLine(" " + (8 - i));
            Console.WriteLine(" +-----------------+");
        }
        Console.WriteLine("  a b c d e f g h ");
    }


    public bool Move(string from, string to)
    {
        // Get the source and destination positions from input strings
        int fromRow = from[0] - 'A';
        int fromCol = from[1] - '1';
        int toRow = to[0] - 'A';
        int toCol = to[1] - '1';

        // Check if the source and destination positions are valid
        if (!IsPositionValid(fromRow, fromCol) || !IsPositionValid(toRow, toCol))
        {
            Console.WriteLine("Invalid position.");
            return false;
        }

        // Check if there is a piece at the source position
        Piece piece = pieces[fromRow, fromCol];
        if (piece == null)
        {
            Console.WriteLine("No piece at the source position.");
            return false;
        }

        // Check if it is the correct player's turn
        if (piece.IsWhite() != whiteTurn)
        {
            Console.WriteLine("It is not your turn.");
            return false;
        }

        // Check if the piece can move to the destination position
        if (!piece.CanMove(toRow, toCol, pieces))
        {
            Console.WriteLine("Invalid move for this piece.");
            return false;
        }

        // Check if the destination position is occupied by the same color piece
        Piece destPiece = pieces[toRow, toCol];
        if (destPiece != null && destPiece.IsWhite() == piece.IsWhite())
        {
            Console.WriteLine("Destination position is occupied by your own piece.");
            return false;
        }

        // Check if the piece can make this move without exposing the king to check
        if (IsCheckAfterMove(fromRow, fromCol, toRow, toCol))
        {
            Console.WriteLine("Cannot make this move because it exposes the king to check.");
            return false;
        }

        // Move the piece to the destination position
        pieces[fromRow, fromCol] = null;
        pieces[toRow, toCol] = piece;
        piece.SetPosition(toRow, toCol);

        // Change the player's turn
        whiteTurn = !whiteTurn;

        return true;
    }
}
