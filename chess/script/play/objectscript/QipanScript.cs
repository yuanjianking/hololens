using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;

//棋盘对象脚本
public class QipanScript : MonoBehaviour, IInputClickHandler
{

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

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //隐藏路线
        ChessManager.GetInstant().HidenRoad();
        ChessManager.GetInstant().ReleaseQizi();
        eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
    }

    public void ResetQiPan()
    {
        //锁定棋盘，电脑走棋。
        ChessManager.GetInstant().ResetQiPan();
    }
}
