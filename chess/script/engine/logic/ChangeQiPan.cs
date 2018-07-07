using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeQiPan : BaseZoufa
{
    public ChangeQiPan(FenData fen) : base(fen)
    {
    }

    public Qizi[,] Change()
    {
        Qizi[,] chess = fen.chess;
        MoveData move = fen[0];
        Qizi qizi = fen[move.start];
        chess[move.start.x, move.start.y] = Qizi.KONGZI;
        chess[move.end.x, move.end.y] = qizi;
        return chess;
    }
}
