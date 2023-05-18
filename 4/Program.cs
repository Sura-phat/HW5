using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string[,] chessboard = new string[8, 8];
        chessboard[0, 0] = "Q"; // หมากควีนเริ่มต้นอยู่ที่ช่อง A1

        Stack<int> undoStack = new Stack<int>(); // รายการการย้อนกลับหลัก
        Stack<int> redoStack = new Stack<int>(); // รายการการย้อนกลับรอง

        while (true)
        {
            PrintChessboard(chessboard);

            Console.WriteLine("กรุณากรอกคำสั่ง (1-8: เดินหมาก, 9: ย้อนกลับ, 10: ย้อนกลับกลับ, 11: สิ้นสุด):");

            int command = int.Parse(Console.ReadLine());

            if (command >= 1 && command <= 8)
            {
                int previousMoveRow;
                int previousMoveColumn;
                FindQueenPosition(chessboard, out previousMoveRow, out previousMoveColumn);

                if (MoveQueen(chessboard, command, previousMoveRow, previousMoveColumn))
                {
                    undoStack.Push(command); // เพิ่มคำสั่งการเดินหมากในรายการการย้อนกลับหลัก
                    redoStack.Clear(); // ล้างรายการการย้อนกลับรอง
                }
                else
                {
                    Console.WriteLine("ไม่สามารถเดินหมากได้");
                }
            }
            else if (command == 9) // ย้อนกลับ
            {
                if (undoStack.Count > 0)
                {
                    int previousMove = undoStack.Pop(); // ดึงคำสั่งการเดินหมากล่าสุดออกมาจาก Stack undoStack

                    int currentRow;
                    int currentColumn;
                    FindQueenPosition(chessboard, out currentRow, out currentColumn);

                    if (UndoMoveQueen(chessboard, previousMove, currentRow, currentColumn))
                    {
                        redoStack.Push(previousMove); // เพิ่มคำสั่งการเดินหมากที่ถูกย้อนกลับเข้าไปในรายการการย้อนกลับรอง
                    }
                }
                else
                {
                    Console.WriteLine("ไม่มีคำสั่งการเดินหมากที่ย้อนกลับได้");
                }
            }
            else if (command == 10) // ย้อนกลับกลับ
            {
                if (redoStack.Count > 0)
                {
                    int previousMove = redoStack.Pop(); // ดึงคำสั่งการเดินหมากที่ถูกย้อนกลับรองออกมาจาก Stack redoStack

                    int currentRow;
                    int currentColumn;
                    FindQueenPosition(chessboard, out currentRow, out currentColumn);

                    if (MoveQueen(chessboard, previousMove, currentRow, currentColumn))
                    {
                        undoStack.Push(previousMove); // เพิ่มคำสั่งการเดินหมากที่ถูกย้อนกลับรองเข้าไปในรายการการย้อนกลับหลัก
                    }
                }
                else
                {
                    Console.WriteLine("ไม่มีคำสั่งการย้อนกลับรอง");
                }
            }
            else if (command == 11) // สิ้นสุด
            {
                if (command == 11) // สิ้นสุด
                {
                    int currentRow;
                    int currentColumn;
                    FindQueenPosition(chessboard, out currentRow, out currentColumn);

                    Console.WriteLine("ตำแหน่งปัจจุบันของหมากควีนคือ: " + (char)(currentColumn + 'A') + (currentRow + 1));
                    Console.WriteLine("โปรแกรมสิ้นสุดการทำงาน");
                    break;
                }

            }
            else
            {
                Console.WriteLine("คำสั่งไม่ถูกต้อง");
            }
        }
    }

    static void PrintChessboard(string[,] chessboard)
    {
        Console.WriteLine("กระดานหมากรุก");
        Console.WriteLine("---------------");
        for (int r = 7; r >= 0; r--)
        {
            for (int c = 0; c < 8; c++)
            {
                string piece = chessboard[r, c];
                if (string.IsNullOrEmpty(piece))
                {
                    piece = "-";
                }
                Console.Write(piece + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static bool MoveQueen(string[,] chessboard, int move, int currentRow, int currentColumn)
    {
        int newRow = currentRow;
        int newColumn = currentColumn;

        switch (move)
        {
            case 1: // เดินขึ้น
                newRow++;
                break;
            case 2: // เดินขึ้นแบบเฉียงไปทางซ้าย
                newRow++;
                newColumn--;
                break;
            case 3: // เดินซ้าย
                newColumn--;
                break;
            case 4: // เดินลงแบบเฉียงไปทางซ้าย
                newRow--;
                newColumn--;
                break;
            case 5: // เดินลง
                newRow--;
                break;
            case 6: // เดินลงแบบเฉียงไปทางขวา
                newRow--;
                newColumn++;
                break;
            case 7: // เดินขวา
                newColumn++;
                break;
            case 8: // เดินขึ้นแบบเฉียงไปทางขวา
                newRow++;
                newColumn++;
                break;
            default:
                return false;
        }

        if (newRow >= 0 && newRow < 8 && newColumn >= 0 && newColumn < 8 && chessboard[newRow, newColumn] == null)
        {
            chessboard[currentRow, currentColumn] = null;
            chessboard[newRow, newColumn] = "Q";
            return true;
        }

        return false;
    }

    static bool UndoMoveQueen(string[,] chessboard, int move, int currentRow, int currentColumn)
    {
        int newRow = currentRow;
        int newColumn = currentColumn;

        switch (move)
        {
            case 1: // เดินขึ้น
                newRow--;
                break;
            case 2: // เดินขึ้นแบบเฉียงไปทางซ้าย
                newRow--;
                newColumn++;
                break;
            case 3: // เดินซ้าย
                newColumn++;
                break;
            case 4: // เดินลงแบบเฉียงไปทางซ้าย
                newRow++;
                newColumn++;
                break;
            case 5: // เดินลง
                newRow++;
                break;
            case 6: // เดินลงแบบเฉียงไปทางขวา
                newRow++;
                newColumn--;
                break;
            case 7: // เดินขวา
                newColumn--;
                break;
            case 8: // เดินขึ้นแบบเฉียงไปทางขวา
                newRow--;
                newColumn--;
                break;
            default:
                return false;
        }

        if (newRow >= 0 && newRow < 8 && newColumn >= 0 && newColumn < 8 && chessboard[newRow, newColumn] == null)
        {
            chessboard[currentRow, currentColumn] = null;
            chessboard[newRow, newColumn] = "Q";
            return true;
        }

        return false;
    }

    static void FindQueenPosition(string[,] chessboard, out int row, out int column)
    {
        row = -1;
        column = -1;

        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                if (chessboard[r, c] == "Q")
                {
                    row = r;
                    column = c;
                    return;
                }
            }
        }
    }
}
