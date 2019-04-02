using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCube : MonoBehaviour {


    public Material[] mats;//表面颜色
    public Texture2D[] textures;//方向箭头

    private int level = 3;//阶层

    private List<List<Material>> edgeMats = null;
    private List<List<GameObject>> superArr = null;

    private readonly float s = 0.48f;
    private readonly float rotTime = 0.5f;

    //旋转用
    private GameObject rotationContainer = null;
    private GameObject rotationDummy1 =null;
    private GameObject rotationDummy2 =null;
    private float tStart = -1;

 
    //边角集合
    private List<Vector3> corners;
    private List<Vector3> edges;

    // Use this for initialization
    void Start() {

        rotationContainer = new GameObject();
        rotationContainer.name = "rotationContainer";
        rotationDummy1 = new GameObject();
        rotationDummy1.name = "rotationDummy1";
        rotationDummy2 = new GameObject();
        rotationDummy2.name = "rotationDummy2";

        InitData();
        CreateMoFang();

    }

    // Update is called once per frame
    void Update() {
        rotationContainer.transform.rotation = Quaternion.Slerp(rotationDummy1.transform.rotation, rotationDummy2.transform.rotation, (Time.time - tStart) / rotTime);
    }
    private void InitData()
    {
        //所有角抽成集合
        corners = new List<Vector3>();
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                for (var k = 0; k < 2; k++)
                {
                    corners.Add(new Vector3(i * (level - 1), j * (level - 1), k * (level - 1)));
                }
            }
        }

        //所有边抽成集合
        edges = new List<Vector3>();
        for (var i1 = 1; i1 < level - 1; i1++)
        {
            for (var j1 = 0; j1 < 2; j1++)
            {
                for (var k1 = 0; k1 < 2; k1++)
                {
                    edges.Add(new Vector3(i1, j1 * (level - 1), k1 * (level - 1)));
                    edges.Add(new Vector3(j1 * (level - 1), i1, k1 * (level - 1)));
                    edges.Add(new Vector3(j1 * (level - 1), k1 * (level - 1), i1));
                }
            }
        }

        //创建材质集合
        String[] mnames = { "L", "R", "D", "UP", "TL", "TR", "BR", "BL" };
        edgeMats = new List<List<Material>>();
        for (var i3 = 0; i3 < 6; i3++)
        {
            mats[i3].SetTextureScale("_MainTex", new Vector2(1, 1));
        }

        for (var i2 = 0; i2 < 6; i2++)
        {
            var arr4 = new List<Material>();
            for (var j2 = 0; j2 < 8; j2++)
            {
                var mat1 = Instantiate(mats[i2]);
                mat1.shader = Shader.Find("Self-Illumin/Specular");
                mat1.SetTexture("_Illum", textures[j2]);
                mat1.name = mnames[j2];
                arr4.Add(mat1);
            }
            for (var j3 = 0; j3 < 4; j3++)
            {//cornerMaterials
                var mat2 = Instantiate(mats[i2]);
                mat2.name = mnames[j3 + 4];
                arr4.Add(mat2);
            }
            edgeMats.Add(arr4);
        }

    }

    private void CreateMoFang()
    {
        superArr = new List< List< GameObject > > ();//verification Array 
        for (var i1 = 0; i1 < 6; i1++)
        {

            var sideArray = new List< GameObject > ();//side controll arrays
            superArr.Add(sideArray);

        }

        //生成方块
        for (var i = 0; i < level; i++)
        {
            for (var j = 0; j < level; j++)
            {
                for (var k = 0; k < level; k++)
                {
                    GameObject cube = new GameObject();
                    if (i == 0 || j == 0 || k == 0 || i == level - 1 || j == level - 1 || k == level - 1)
                    {
                        AddWalls(cube, i, j, k);
                    }
                  
                    cube.name = "cube" + i + j + k;
                    cube.transform.parent = transform;
                    cube.transform.localPosition = new Vector3(-level/2 + i + 0.5f , -level/2 + j + 0.5f, -level/2 + k + 0.5f);
                    if (i == 0) { superArr[0].Add(cube); }
                    if (i == level - 1) { superArr[1].Add(cube); }
                    if (j == 0) { superArr[2].Add(cube); }
                    if (j == level - 1) { superArr[3].Add(cube); }
                    if (k == 0) { superArr[4].Add(cube); }
                    if (k == level - 1) { superArr[5].Add(cube); }
                }
            }
        }
    }


    private void AddWalls(GameObject box, int u, int v, int w)
    {
        GameObject edge = null;
        GameObject cornerEdgeU = null;
        GameObject cornerEdgeV = null;
        GameObject cornerEdgeW = null;

        if (corners.Contains(new Vector3(u, v, w)))
        {
            cornerEdgeU = new GameObject();
            cornerEdgeV = new GameObject();
            cornerEdgeW = new GameObject();
            cornerEdgeU.transform.parent = box.transform;
            cornerEdgeV.transform.parent = box.transform;
            cornerEdgeW.transform.parent = box.transform;
        }

        if (edges.Contains(new Vector3(u, v, w)))
        {
            edge = new GameObject
            {
                name = "edge" + u + v + w
            };
            edge.transform.parent = box.transform;
        }

        //添加六面墙纸
        if (w == level - 1)
        {//back
            if ((new Vector2(u, v)).Equals(new Vector2(0, 0)) || (new Vector2(u, v)).Equals(new Vector2(level - 1, 0)) || (new Vector2(u, v)).Equals(new Vector2(0, level - 1)) || (new Vector2(u, v)).Equals(new Vector2(level - 1, level - 1)))
            {
                CornerWall(0, new Vector2(u, v), cornerEdgeU, cornerEdgeV);
            }
            else
            {
                if (u == 0 || u == level - 1 || v == 0 || v == level - 1) { Wall(0, "outer", u, v, edge, true); } else { Wall(0, "outer", u, v, box, false); }
            }
        }
        else { Wall(0, "inner", u, v, box, false); }
        if (u == level - 1)
        {//right
            if ((new Vector2(w, v)).Equals(new Vector2(0, 0)) || (new Vector2(w, v)).Equals(new Vector2(level - 1, 0)) || (new Vector2(w, v)).Equals(new Vector2(0, level - 1)) || (new Vector2(w, v)).Equals(new Vector2(level - 1, level - 1)))
            {
                CornerWall(1, new Vector2(w, v), cornerEdgeW, cornerEdgeV);
            }
            else
            {
                if (w == 0 || w == level - 1 || v == 0 || v == level - 1) { Wall(1, "outer", level - 1 - w, v, edge, true); } else { Wall(1, "outer", level - 1 - w, v, box, false); }
            }
        }
        else { Wall(1, "inner", w, v, box, false); }
        if (w == 0)
        {//front
            if ((new Vector2(u, v)).Equals(new Vector2(0, 0)) || (new Vector2(u, v)).Equals(new Vector2(level - 1, 0)) || (new Vector2(u, v)).Equals(new Vector2(0, level - 1)) || (new Vector2(u, v)).Equals(new Vector2(level - 1, level - 1)))
            {
                CornerWall(2, new Vector2(u, v), cornerEdgeU, cornerEdgeV);
            }
            else
            {
                if (u == 0 || u == level - 1 || v == 0 || v == level - 1) { Wall(2, "outer", level - 1 - u, v, edge, true); } else { Wall(2, "outer", level - 1 - u, v, box, false); }
            }
        }
        else { Wall(2, "inner", -u, v, box, false); }
        if (u == 0)
        {//left
            if ((new Vector2(w, v)).Equals(new Vector2(0, 0)) || (new Vector2(w, v)).Equals(new Vector2(level - 1, 0)) || (new Vector2(w, v)).Equals(new Vector2(0, level - 1)) || (new Vector2(w, v)).Equals(new Vector2(level - 1, level - 1)))
            {
                CornerWall(3, new Vector2(w, v), cornerEdgeW, cornerEdgeV);
            }
            else
            {
                if (w == 0 || w == level - 1 || v == 0 || v == level - 1) { Wall(3, "outer", w, v, edge, true); } else { Wall(3, "outer", w, v, box, false); }
            }
        }
        else { Wall(3, "inner", w, v, box, false); }
        if (v == 0)
        {//bottom
            if ((new Vector2(w, u)).Equals(new Vector2(0, 0)) || (new Vector2(w, u)).Equals(new Vector2(level - 1, 0)) || (new Vector2(w, u)).Equals(new Vector2(0, level - 1)) || (new Vector2(w, u)).Equals(new Vector2(level - 1, level - 1)))
            {
                CornerWall(4, new Vector2(u, w), cornerEdgeU, cornerEdgeW);
            }
            else
            {
                if (w == 0 || w == level - 1 || u == 0 || u == level - 1) { Wall(4, "outer", u, w, edge, true); } else { Wall(4, "outer", u, w, box, false); }
            }
        }
        else { Wall(4, "inner", w, u, box, false); }
        if (v == level - 1)
        {//top
            if ((new Vector2(w, u)).Equals(new Vector2(0, 0)) || (new Vector2(w, u)).Equals(new Vector2(level - 1, 0)) || (new Vector2(w, u)).Equals(new Vector2(0, level - 1)) ||  (new Vector2(w, u)).Equals(new Vector2(level - 1, level - 1)))
            {
                CornerWall(5, new Vector2(u, w), cornerEdgeU, cornerEdgeW);
            }
            else
            {
                if (w == 0 || w == level - 1 || u == 0 || u == level - 1) { Wall(5, "outer", u, level - 1 - w, edge, true); } else { Wall(5, "outer", u, level - 1 - w, box, false); }
            }
        }
        else { Wall(5, "inner", w, u, box, false); }
    }

    private void CornerWall(int pos, Vector2 vect, GameObject parentObject1, GameObject parentObject2)
    {
        GameObject[] parentObjects =  { parentObject1, parentObject2 };
        int k1 = 1;
        //角墙纸有两个三角组成
        foreach (GameObject po in parentObjects)
        {
            bool reverseX = false;
            bool reverseY = false;
            var tri = new GameObject();
            var a1 = 2 * s * vect.x / (level - 1);
            var b1 = 2 * s * vect.y / (level - 1);
            Mesh mesh = new Mesh();
            tri.transform.parent = po.transform;
            tri.AddComponent<MeshFilter>();
            tri.AddComponent<MeshRenderer>();
            tri.GetComponent<MeshFilter>().mesh = mesh;
            switch (pos)
            {
                case 0: //back
                    mesh.vertices = new Vector3[] { k1 * new Vector3(s - a1, s - b1, 0), k1 * new Vector3(s - a1, -s + b1, 0), k1 * new Vector3(-s + a1, -s + b1, 0) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(0, 1 - b1 / s), k1 * new Vector2(0, 0), k1 * new Vector2(1 - a1 / s, 0) };/////
                    tri.transform.position = new Vector3(tri.transform.position.x, tri.transform.position.y, s);
                    break;
                case 1://right
                    mesh.vertices = new Vector3[] { k1 * new Vector3(0, -s + b1, -s + a1), k1 * new Vector3(0, -s + b1, s - a1), k1 * new Vector3(0, s - b1, s - a1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(-1 + a1 / s, 0), new Vector2(0, 0), k1 * new Vector2(0, 1 - b1 / s) };
                    tri.transform.position = new Vector3(s, tri.transform.position.y, tri.transform.position.z);
                    reverseX = true;
                    break;
                case 2://front
                    mesh.vertices = new Vector3[] { k1 * new Vector3(-s + a1, -s + b1, 0), k1 * new Vector3(s - a1, -s + b1, 0), k1 * new Vector3(s - a1, s - b1, 0) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(-1 + a1 / s, 0), new Vector2(0, 0), k1 * new Vector2(0, 1 - b1 / s) };
                    tri.transform.position = new Vector3(tri.transform.position.x, tri.transform.position.y, -s);
                    reverseX = true;
                    break;
                case 3://left
                    mesh.vertices = new Vector3[] { k1 * new Vector3(0, s - b1, s - a1), k1 * new Vector3(0, -s + b1, s - a1), k1 * new Vector3(0, -s + b1, -s + a1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(0, 1 - b1 / s), k1 * new Vector2(0, 0), k1 * new Vector2(1 - a1 / s, 0) };
                    tri.transform.position = new Vector3(-s, tri.transform.position.y, tri.transform.position.z);
                    break;
                case 4://bottom
                    mesh.vertices = new Vector3[] { k1 * new Vector3(s - a1, 0, s - b1), k1 * new Vector3(s - a1, 0, -s + b1), k1 * new Vector3(-s + a1, 0, -s + b1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(0, 1 - b1 / s), k1 * new Vector2(0, 0), k1 * new Vector2(1 - a1 / s, 0) };
                    tri.transform.position = new Vector3(tri.transform.position.x, -s, tri.transform.position.z);
                    break;
                case 5://top
                    mesh.vertices = new Vector3[] { k1 * new Vector3(-s + a1, 0, -s + b1), k1 * new Vector3(s - a1, 0, -s + b1), k1 * new Vector3(s - a1, 0, s - b1) };
                    mesh.uv = new Vector2[] { k1 * new Vector2(1 - a1 / s, 0), k1 * new Vector2(0, 0), k1 * new Vector2(0, -1 + b1 / s) };
                    tri.transform.position = new Vector3(tri.transform.position.x, s, tri.transform.position.z);
                    reverseY = true;
                    break;
            }

            int mm = 0; ;
            if (vect == new Vector2(0, level - 1))
            {
                //top-left	
                mm = 5;
                if (reverseX) { mm = 4; }
                if (reverseY) { mm = 6; }
            }
            else if (vect == new Vector2(0, 0))
            { //bottom-left	
                mm = 6; if (reverseX) { mm = 7; }
                if (reverseY) { mm = 5; }
            }
            else if(vect == new Vector2(level - 1, 0))
            {
                //bottom-left	
                mm = 7; if (reverseX) { mm = 6; }
                if (reverseY) { mm = 4; }
            }
            else if(vect == new Vector2(level - 1, level - 1))
            {
                //bottom-left	
                mm = 4; if (reverseX) { mm = 5; }
                if (reverseY) { mm = 7; }
            }

            if (vect.x == vect.y) { mesh.triangles = new int[]{ 0, 2, 1}; } else { mesh.triangles = new int[]{ 0, 1, 2}; }
            mesh.RecalculateNormals();
            tri.AddComponent<MeshCollider>();
            tri.AddComponent<MouseInteract>(); tri.GetComponent<MouseInteract>().level = level;
           tri.GetComponent<MouseInteract>().mat = edgeMats[pos][mm + 4];
            tri.GetComponent< Renderer > ().material = edgeMats[pos][mm + 4];
            tri.GetComponent<MouseInteract>().matOver = edgeMats[pos][mm];

            k1 *= -1;
        }
    }

    private void Wall(int pos, String inner, int x1, int y1, GameObject parentObject, bool interactive)
    {
        //角以外墙纸由一个面构成
        Material matHighlight = null;
        GameObject square = new GameObject();
        Mesh mesh = new Mesh();
        square.transform.parent = parentObject.transform;
        square.AddComponent<MeshFilter>();
        square.AddComponent<MeshRenderer>();
        square.GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = new Vector3[] { new Vector3(s, s, 0), new Vector3(s, -s, 0), new Vector3(-s, -s, 0), new Vector3(-s, s, 0) };
        mesh.uv = new Vector2[] { new Vector2(-x1 - 0.5f - s, y1 + 0.5f + s) / level, new Vector2(-x1 - 0.5f - s, y1 + 0.5f - s) / level, new Vector2(-x1 - 0.5f + s, y1 + 0.5f - s) / level, new Vector2(-x1 - 0.5f + s, y1 + 0.5f + s) / level };
        mesh.triangles = new int[] { 0, 2, 1, 0, 3, 2 };
        mesh.RecalculateNormals();
        square.transform.position = new Vector3(square.transform.position.x, square.transform.position.y, s);
        switch (pos)
        {
            case 0: //back
                break;
            case 1://right
                square.transform.RotateAround(Vector3.zero, Vector3.up, 90);
                break;
            case 2://front
                square.transform.RotateAround(Vector3.zero, Vector3.up, 180);
                break;
            case 3://left
                square.transform.RotateAround(Vector3.zero, Vector3.up, 270);
                break;
            case 4://bottom
                square.transform.RotateAround(Vector3.zero, Vector3.right, 90);
                break;
            case 5://top
                square.transform.RotateAround(Vector3.zero, Vector3.right, 270);
                break;
        }

        if (inner.Equals("inner"))
        {
            square.GetComponent<Renderer>().material = mats[6];
        }
        else
        {
            square.GetComponent<Renderer>().material = mats[pos];
            if (x1 == 0)
            {
                matHighlight = edgeMats[pos][1];
            }
            else if (x1 == level - 1)
            {
                matHighlight = edgeMats[pos][0];
            }

            if (y1 == 0)
            {
                matHighlight = edgeMats[pos][2];
            }
            else if (y1 == level - 1)
            {
                matHighlight = edgeMats[pos][3];
            }
          
            square.AddComponent<MeshCollider>();
            square.AddComponent<MouseInteract>(); square.GetComponent<MouseInteract>().level = level;
            square.GetComponent<MouseInteract>().mat = mats[pos];
            square.GetComponent<MouseInteract>().matOver = matHighlight;
          
        }
    }
    //旋转魔方
    private void SelectRotate(String version1, Vector3 axis, int ixx, int iyy, int izz, Boolean noSmoothing)
    {
        List<Transform> rotationArr = new List< Transform > ();//
        foreach (Transform box in transform)
        {
            switch (version1)
            {
                case "x":
                    if (Mathf.Round(box.transform.position.x + 0.5f * (level - 1)) == ixx)
                    {
                        rotationArr.Add(box);
                    }
                    break;
                case "y":
                    if (Mathf.Round(box.transform.position.y + 0.5f * (level - 1)) == iyy)
                    {
                        rotationArr.Add(box);
                    }
                    break;
                case "z":
                    if (Mathf.Round(box.transform.position.z + 0.5f * (level - 1)) == izz)
                    {
                        rotationArr.Add(box);
                    }
                    break;
            }
        }
        int m = rotationArr.Count;

        for (var i = 0; i < m; i++)
        {
            rotationArr[i].transform.parent = rotationContainer.transform;
        }

        rotationDummy2.transform.RotateAround(Vector3.zero, -axis, 90);
        tStart = Time.time;
        rotationDummy1.transform.RotateAround(Vector3.zero, -axis, 90);
        
        for (var j = 0; j < m; j++)
        {
            rotationArr[j].transform.parent = transform;
        }      
    }
}