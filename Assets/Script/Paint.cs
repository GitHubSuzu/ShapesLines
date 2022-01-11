
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// お絵描き機能　適当なオブジェクトにアタッチ
/// </summary>
public class Paint : MonoBehaviour
{
    [SerializeField] private Transform _paintObjParent;

    [SerializeField] private GameObject _paintObjPrefab;

    [SerializeField] private GameObject _vertCube;

    private GameObject _tmpPaintObj;
    bool check = true;

    private void Update()
    {
        //スクリーン座標をワールド座標に変換
        Vector3 worldClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition +  Camera.main.transform.forward);

        //マウス押した瞬間
        if (Input.GetMouseButtonDown(0))
        {
            _tmpPaintObj = Instantiate(_paintObjPrefab, worldClickPos, Quaternion.identity);
            _tmpPaintObj.transform.parent = _paintObjParent;
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
            int vertCount =  tr.GetPositions(posArray);

            //描画した頂点座標を確認
            for (int i = 0; i < vertCount; i++)
            {
                Debug.Log(posArray[i]);
                Instantiate(_vertCube, posArray[i], Quaternion.identity);
            }

            if (check)
            {
                Triangles(posArray[0], posArray[1], posArray[2]);
                check = false;
            }
        }
    }

    //三角形
    void Triangles(Vector3 a, Vector3 b, Vector3 c)
    {
        var angleA = Vector3.Angle(b - a, c - a);
        var angleB = Vector3.Angle(a - b, c - b);
        var angleC = Vector3.Angle(b - c, a - c);

        Debug.Log(angleA);
        Debug.Log(angleB);
        Debug.Log(angleC);

        Debug.Log(angleA + angleB + angleC);
    }

    //四角形
    void Squares(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        var angleA = Vector3.Angle(b - a, d - a);
        var angleB = Vector3.Angle(a - b, c - b);
        var angleC = Vector3.Angle(b - c, d - c);
        var angleD = Vector3.Angle(c - d, a - d);
    }

    //五角形
    void Pentagon(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e)
    {
        var angleA = Vector3.Angle(b - a, e - a);
        var angleB = Vector3.Angle(a - b, c - b);
        var angleC = Vector3.Angle(b - c, d - c);
        var angleD = Vector3.Angle(c - d, e - d);
        var angleE = Vector3.Angle(d - e, a - e);
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
    }

    //ひし形
    void rhombus(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        var angleA = Vector3.Angle(b - a, d - a);
        var angleB = Vector3.Angle(a - b, c - b);
        var angleC = Vector3.Angle(b - c, d - c);
        var angleD = Vector3.Angle(c - d, a - d);
    }

    //台形
    void Trapezoid(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        var angleA = Vector3.Angle(b - a, d - a);
        var angleB = Vector3.Angle(a - b, c - b);
        var angleC = Vector3.Angle(b - c, d - c);
        var angleD = Vector3.Angle(c - d, a - d);
    }
    
}