
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
        }
    }
}