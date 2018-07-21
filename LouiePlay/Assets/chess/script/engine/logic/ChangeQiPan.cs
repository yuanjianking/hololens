using System.Collections.Generic;

public class ChangeQiPan:BaseZoufa
{
    private List<Qizi[]> moves = new List<Qizi[]>();

    public ChangeQiPan(FenData fen) : base(fen)
    {
       
    }

    public void Move()
    {
        MoveData move = fen.moves[0];
        Qizi[] qizis = new Qizi[2];
        qizis[0] = fen[move.start];
        qizis[1] = fen[move.end];
        fen[move.start] = Qizi.KONGZI;
        fen[move.end] = qizis[0];
        fen.current = fen.current ^ 0x0003;
        moves.Insert(0, qizis);
    }

    public void AddMove(MoveData move)
    {
        Qizi[] qizis = new Qizi[2];
        qizis[0] = fen[move.start];
        qizis[1] = fen[move.end];
        fen[move.start] = Qizi.KONGZI;
        fen[move.end] = qizis[0];
        fen.current = fen.current ^ 0x0003;
        fen.moves.Insert(0, move);
        moves.Insert(0, qizis);
    }


    public void GoBack()
    {
        MoveData move = fen.moves[0];
        Qizi[] qizis = moves[0];
        fen[move.start] = qizis[0];
        fen[move.end] = qizis[1];
        fen.current = fen.current ^ 0xFF;
        moves.RemoveAt(0);
        fen.moves.RemoveAt(0);
    }
}
