using System.Collections.Generic;

public class ChangeQiPan:BaseZoufa
{
    private List<Qizi[]> moves = new List<Qizi[]>();

    public ChangeQiPan(FenData fen) : base(fen)
    {
       
    }
    public void Move()
    {
        MoveData move = fen[0];
        Qizi[] qizis = new Qizi[2];
        qizis[0] = fen[move.start];
        qizis[1] = fen[move.end];
        fen[move.start] = Qizi.KONGZI;
        fen[move.end] = qizis[0];
        moves.Insert(0, qizis);
     }

    public void GoBack()
    {
        MoveData move = fen[0];
        foreach (Qizi[] qizis in moves)
        {
            fen[move.start] = qizis[0];
            fen[move.end] = qizis[1];
        }
    }
}
