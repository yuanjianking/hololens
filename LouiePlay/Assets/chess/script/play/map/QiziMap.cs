using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//棋盘坐标与棋子映射类
public class QiziMap {
    private static Dictionary<string, GameObject> gameobjects = new Dictionary<string, GameObject>();

    public static void AddGameObject(string point, GameObject game)
    {
        gameobjects.Add(point, game);
    }

    public static void ReplaceGameObject(string point1, string point2, GameObject game)
    {
        gameobjects.Remove(point1);
        AddGameObject(point2, game);
    }

    public static GameObject GetGameObject(string point)
    {
        if (gameobjects.ContainsKey(point))
            return gameobjects[point];
        else
            return null;
    }


    public static void RemoveGameObject(string point)
    {
        gameobjects.Remove(point);
    }
    public static void RemoveAllGameObject()
    {
        gameobjects = new Dictionary<string, GameObject>();
    }
}
