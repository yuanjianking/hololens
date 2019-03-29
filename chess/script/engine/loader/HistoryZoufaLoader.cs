using System;

//历史走法管理类
public class HistoryZoufaLoader : BaseLoader
{
     //添加一个走法
    public void AddZoufa(String key , MoveData move)
    {
    }
    //获得指定棋局的走法
    public MoveData GetZoufa(String key)
    {
        MoveData move = null;
        //SQLiteHelper sql = sql = new SQLiteHelper(connectstring);
        //SqliteDataReader reader = sql.ReadTable("History", new string[] { "startx", "starty", "endx", "endy" }, new string[] { "chess" }, new string[] { "=" }, new string[] { "'" + key + "'" });
        //while (reader.Read())
        //{
        //    int startx = reader.GetInt32(reader.GetOrdinal("startx"));
        //    int starty = reader.GetInt32(reader.GetOrdinal("starty"));
        //    int endx = reader.GetInt32(reader.GetOrdinal("endx"));
        //    int endy = reader.GetInt32(reader.GetOrdinal("endy"));
        //    move = new MoveData(new PointData(startx, starty), new PointData(endx, endy));
        //}
        ////关闭数据库连接
        //sql.CloseConnection();
        return move;

    }
    //移除所有走法
    public void RemoveAll ()
    {
    }
}
