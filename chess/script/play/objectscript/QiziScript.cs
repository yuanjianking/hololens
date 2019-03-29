using HoloToolkit.Unity.InputModule;
using System.Threading;
using UnityEngine;

public class QiziScript : MonoBehaviour, IFocusable, IInputClickHandler
{
    public Qizi qizi = Qizi.KONGZI;
    public int current = Constant.RED;
    public int x = 0;
    public int y = 0;
    public int z = 0;
    
    // Use this for initialization
    void Start () {
        //保持棋子
        QiziMap.AddGameObject(x.ToString() + y.ToString(), gameObject);
        //初始化棋子
        ChessManager.GetInstant().InitQizi(current, new PointData(x, y, z), transform);
    }
    
    public void OnFocusEnter()
    {
        //显示路线
        //   ChessManager.GetInstant().ShowRoad(current, new PointData(x, y, z));
        if (current == ChessManager.GetInstant().GetFenData().player1)
            GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnFocusExit()
    {
        //隐藏路线
        //  ChessManager.GetInstant().HidenRoad();
        if (current == ChessManager.GetInstant().GetFenData().player1)
            GetComponent<Renderer>().material.color = Color.white;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (current == ChessManager.GetInstant().GetFenData().player1)
        {
            // Increase the scale of the object just as a response. 
            PointData point = new PointData(x, y, z);
            //隐藏路线
            ChessManager.GetInstant().HidenRoad();
            ChessManager.GetInstant().SetSelected(point);
            ChessManager.GetInstant().ShowRoad(current, point);
        }
        else {
            PointData point = new PointData(x, y, z);
            ChessManager.GetInstant().CheckAndMove(point);
        }
        eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
    }
}
