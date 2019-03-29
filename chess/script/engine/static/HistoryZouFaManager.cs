using System;

public class HistoryZouFaManager
{
    private static HistoryZoufa history = new HistoryZoufa();
    public static void AddZouFa(FenData fen)
    {
        history.ZouFa.Add(Utility.ConvertQiPanToString(fen),fen.moves[0]);
    }
    public static MoveData GetZouFa(String key)
    {
       return history.ZouFa[key];
    }
    public static void RemoveAll ()
    {
        history.ZouFa.Clear();
    }
}
