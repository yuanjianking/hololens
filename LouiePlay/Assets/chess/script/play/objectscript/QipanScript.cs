using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

//棋盘对象脚本
public class QipanScript : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        //初始化棋盘路线
        ChessManager.GetNewInstant().HidenRoad();
    }

    private void Update()
    {    
        FenData fen = ChessManager.GetInstant().GetFenData();
        if (fen.player1 == fen.current)
        {
            //什么都不干
        }
        else
        {
            //锁定棋盘
            //没完 lock游戏画面
            //电脑走棋
            ChessManager.GetInstant().DianNaoZouQi();
        }
    
    }
    public void test()
    {
        //锁定棋盘，电脑走棋。
        ChessManager.GetInstant().DianNaoZouQi();
    }

    public void Reset()
    {
        //锁定棋盘，电脑走棋。
        ChessManager.GetInstant().ResetQiPan();
    }
}
