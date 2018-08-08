using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

//象棋管理类
public class ChessManager
{
    #region 局部变量
    //全局fen数据
    private FenData Fen = new FenData();
    private static ChessManager chessManager = null;

    //棋盘间距
    private float xoffset = 1.06f;
    private float yoffset = 1.0f;
    private float zoffset = 0.98f;
    private float heffset = 1.0f;
    //棋盘路线
    private GameObject roads = null;
    private static ILogger log = Debug.unityLogger;
    #endregion

    #region 类初始化
    private ChessManager()
    {
        roads = GameObject.Find("roads");
    }

    public static ChessManager GetNewInstant()
    {
        chessManager = new ChessManager();
        return chessManager;
    }

    public static ChessManager GetInstant()
    {
        if (chessManager == null)
            GetNewInstant();
        return chessManager;
    }
    #endregion

    #region FEN
    public FenData GetFenDataClone()
    {
        return (FenData)Fen.Clone();
    }

    public FenData GetFenData()
    {
        return Fen;
    }

    public void SetFenData(FenData fen)
    {
        Fen = fen;
    }
    #endregion

    #region 棋子相关
    //初始化棋子的位置
    public void InitQizi(int current, PointData point, Transform transform)
    {
        if (current == Constant.RED)
        {
            transform.localPosition = new Vector3((point.x - 4) * xoffset, yoffset,
                                   (5 - point.y) * zoffset);
        }
        else
        {
            transform.localPosition = new Vector3((point.x - 4) * xoffset, yoffset,
                                  (4 - point.y) * zoffset);
        }
    }

    //电脑走棋
    public void DianNaoZouQi()
    {
        ResultData data = ChineseChessHandler.GetInstant().GetZoufa(Fen);
        log.Log(data.result);
        log.Log(data.caneat);
        log.Log(data.moves.Count);
        if (data.result)
        {
            Fen.noteatcount++;
            MoveData move = data.moves[0];
            if (data.caneat)
            {
                GameObject obj = QiziMap.GetGameObject(move.end.x.ToString() + move.end.y.ToString());
                obj.SetActive(false);
                QiziMap.RemoveGameObject(move.end.x.ToString() + move.end.y.ToString());
                Fen.noteatcount = 0;
            }
            MoveQizi(move);
            Fen.moves.Insert(0, move);
            Fen[move.end] = Fen[move.start];
            Fen[move.start] = Qizi.KONGZI;
            Fen.current = Fen.current ^ 0x0003;
            Fen.count++;
        }
    }

    //检查走棋的位置，如果没有问题，执行走棋
    public void CheckAndMove(PointData start, Vector3 position)
    {
        MoveData move = new MoveData();
        move.start = start;
        move.end = ConvertQiPanZuobiao(position);
        if (move.end == PointData.NgData)
        {
            //恢复坐标
            move.end = start;
            MoveQizi(move);
            return;
        }

        FenData f = GetFenDataClone();
        
        f.moves.Insert(0, move);
        ResultData data = ChineseChessHandler.GetInstant().CheckZoufa(f);
        if (data.result)
        {
            Fen.noteatcount++;
            if (data.caneat)
            {
                GameObject obj = QiziMap.GetGameObject(move.end.x.ToString() + move.end.y.ToString());
                obj.SetActive(false);
                QiziMap.RemoveGameObject(move.end.x.ToString() + move.end.y.ToString());
                Fen.noteatcount = 0;
            }
            MoveQizi(move);
            Fen.moves.Insert(0, move);
            Fen[move.end] = Fen[move.start];
            Fen[move.start] = Qizi.KONGZI;
            Fen.current = Fen.current ^ 0x0003;
            Fen.count++;
        }
        else
        {
            //恢复坐标
            move.end = start;
            MoveQizi(move);
        }
    }

    //移动棋子
    public void MoveQizi(MoveData move)
    {
        GameObject game = QiziMap.GetGameObject(move.start.x.ToString() + move.start.y.ToString());
        if (game == null) return;

        QiziScript qiziobj = game.GetComponent<QiziScript>();
        float he = 0.0f;
        if (qiziobj.current == Constant.RED)
        {
            if (move.end.y < 5)
            {
                he = heffset;
                game.transform.localPosition = new Vector3((move.end.x - 4) * xoffset, yoffset,
                               he + (4 - move.end.y) * zoffset);
            }
            else
            {
                game.transform.localPosition = new Vector3((move.end.x - 4) * xoffset, yoffset,
                                 he + (5 - move.end.y) * zoffset);
            }
        }
        else
        {
            if (move.end.y > 4)
            {
                he = heffset;
                game.transform.localPosition = new Vector3((move.end.x - 4) * xoffset, yoffset,
                               -he + (5 - move.end.y) * zoffset);
            }
            else
            {
                game.transform.localPosition = new Vector3((move.end.x - 4) * xoffset, yoffset,
                                       he + (4 - move.end.y) * zoffset);
            }
        }
        QiziMap.ReplaceGameObject(move.start.x.ToString() + move.start.y.ToString(),
                                           move.end.x.ToString() + move.end.y.ToString(), game);
        qiziobj.x = move.end.x;
        qiziobj.y = move.end.y;
        qiziobj.z = move.end.z;
    }

    //获得棋子坐标
    public PointData ConvertQiPanZuobiao(Vector3 vector)
    {
        foreach (Transform road in roads.transform)
        {
            foreach (Transform d in road)
            {
                if ((d.position.x - 0.5) < vector.x && (d.position.x + 0.5) > vector.x &&
                     (d.position.y - 0.5) < vector.y && (d.position.y + 0.5) > vector.y)
                {
                    int x = int.Parse(d.name.Substring(1));
                    int y = int.Parse(road.name.Substring(4));
                    return new PointData(x, y);
                }
            }
        }
        return PointData.NgData;
    }
    #endregion

    #region 路线相关
    //隐藏路线
    public void HidenRoad()
    {
        foreach (Transform road in roads.transform)
        {
            foreach (Transform d in road)
            {
                d.gameObject.SetActive(false);
            }
        }
    }

    //显示路线
    public void ShowRoad(int current, PointData point)
    {
        FenData fen = GetFenDataClone();
        fen.current = current;
        fen.selected = point;
        ResultData data = ChineseChessHandler.GetInstant().GetMoveLine(fen);
        if (data.result)
        {
            foreach (MoveData move in data.moves)
            {
                roads.transform.Find("road" + move.end.y.ToString() + "/d" + move.end.x.ToString()).gameObject.SetActive(true);
            }
        }
    }

    #endregion

    #region 全局相关
    //场景重置
    public void ResetQiPan()
    {
        //清除棋子位置管理
        QiziMap.RemoveAllGameObject();

        //重置棋盘
        SceneManager.LoadScene(0);
    }
    #endregion
}
