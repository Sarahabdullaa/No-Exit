using System.Collections.Generic;
using UnityEngine;

public static class PlayerInventory
{
    public static Dictionary<string, bool> CollectedPieces = new Dictionary<string, bool>();

    public static void AddPiece(string pieceName)
    {
        if (!CollectedPieces.ContainsKey(pieceName))
            CollectedPieces.Add(pieceName, true);
        else
            CollectedPieces[pieceName] = true;
    }

    public static bool HasPiece(string pieceName)
    {
        return CollectedPieces.ContainsKey(pieceName) && CollectedPieces[pieceName];
    }

    public static void UsePiece(string pieceName)
    {
        if (CollectedPieces.ContainsKey(pieceName))
            CollectedPieces[pieceName] = false;
    }
}