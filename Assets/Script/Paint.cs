
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// お絵描き機能　適当なオブジェクトにアタッチ
/// </summary>
public class Paint : MonoBehaviour
{
    [SerializeField]
    private Transform _paintObjParent;

    [SerializeField]
    private GameObject _paintObjPrefab;

    [SerializeField]
    private GameObject _vertCube;

    private GameObject _tmpPaintObj;
    bool check = true;

    private void Update()
    {
        //スクリーン座標をワールド座標に変換
        Vector3 worldClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward);

        //マウス押した瞬間
        if (Input.GetMouseButtonDown(0))
        {
            _tmpPaintObj = Instantiate(_paintObjPrefab, worldClickPos, Quaternion.identity);
            _tmpPaintObj.transform.parent = _paintObjParent;
            check = true;
        }

        //マウス押し続けている間
        if (Input.GetMouseButton(0))
        {
            _tmpPaintObj.transform.position = worldClickPos;
        }
    }

    /// <summary>
    /// 全てのTrailRendererの全頂点を取得 Buttonに登録
    /// </summary>
    public void GetVert()
    {
        foreach (Transform child in _paintObjParent.transform)
        {
            TrailRenderer tr = child.GetComponent<TrailRenderer>();
            int posCount = tr.positionCount;
            Vector3[] posArray = new Vector3[posCount];

            //全ての頂点を取ってくる
            int vertCount = tr.GetPositions(posArray);

            //描画した頂点座標を確認
            for (int i = 0; i < vertCount; i++)
            {
                Debug.Log(posArray[i]);
                Instantiate(_vertCube, posArray[i], Quaternion.identity);
            }

            string shapes = "Triangles";
            switch (shapes)
            {
               case "Triangles":
                   if (check && vertCount == 3)
                   {
                       Debug.Log(vertCount);
                       Triangles(posArray[0], posArray[1], posArray[2]);
                       check = false;
                   }
                   break;
               case "Squares":
                   if (check && vertCount == 4)
                   {
                       Squares(posArray[0], posArray[1], posArray[2], posArray[3], "Squares");
                       check = false;
                   }
                   break;
               case "Pentagon":
                   if (check && vertCount == 5)
                   {
                       Pentagon(posArray[0], posArray[1], posArray[2], posArray[3], posArray[4]);
                       check = false;
                   }
                   break;
               case "Hexagon":
                   if (check && vertCount == 6)
                   {
                       Hexagon(posArray[0], posArray[1], posArray[2], posArray[3], posArray[4], posArray[5]);
                       check = false;
                   }
                   break;
            }
        }
    }

    //三角形
    void Triangles(Vector3 a, Vector3 b, Vector3 c)
    {
        var angleA = Vector3.Angle(b - a, c - a);
        var angleB = Vector3.Angle(a - b, c - b);
        var angleC = Vector3.Angle(b - c, a - c);
        
        var allAngle = angleA + angleB + angleC;
        
        Debug.Log((int) allAngle == 180 ? "正解" : "不正解");
    }

    //四角形
    void Squares(Vector3 a, Vector3 b, Vector3 c, Vector3 d, string shapes)
    {
        var angleA = Vector3.Angle(b - a, d - a);
        var angleB = Vector3.Angle(a - b, c - b);
        var angleC = Vector3.Angle(b - c, d - c);
        var angleD = Vector3.Angle(c - d, a - d);
        
        var allAngle = angleA + angleB + angleC + angleD;

        switch (shapes)
        {
            case "Squares":
                Debug.Log((int) allAngle == 360 ? "正解" : "不正解");
                break;
            case "rhombus": //ひし形
                Debug.Log((int) allAngle == 360 && Difference(angleA, angleC) && Difference(angleB, angleD)? "正解" : "不正解");
                break;
            case "Trapezoid": //台形
                Debug.Log((int) allAngle == 360 && Difference(a.y, d.y) && Difference(b.y, c.y) ? "正解" : "不正解");
                break;
        }
    }

    bool Difference(float a, float b)
    {
        if (a > 0 && b < 0 || a < 0 && b > 0 ) return false;
        
        var ans = Mathf.Abs(a - b);
        if (ans < 5 && ans > -5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

    //五角形
    void Pentagon(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e)
    {
        var angleA = Vector3.Angle(b - a, e - a);
        var angleB = Vector3.Angle(a - b, c - b);
        var angleC = Vector3.Angle(b - c, d - c);
        var angleD = Vector3.Angle(c - d, e - d);
        var angleE = Vector3.Angle(d - e, a - e);

        var allAngle = angleA + angleB + angleC + angleD + angleE;
        
        Debug.Log((int) allAngle == 540 ? "正解" : "不正解");
    }

    //六角形
    void Hexagon(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e, Vector3 f)
    {
        var angleA = Vector3.Angle(b - a, f - a);
        var angleB = Vector3.Angle(a - b, c - b);
        var angleC = Vector3.Angle(b - c, d - c);
        var angleD = Vector3.Angle(c - d, e - d);
        var angleE = Vector3.Angle(d - e, f - e);
        var angleF = Vector3.Angle(e - f, a - f);

        var allAngle = angleA + angleB + angleC + angleD + angleE + angleF;

        Debug.Log((int) allAngle == 720 ? "正解" : "不正解");
    }
    
}