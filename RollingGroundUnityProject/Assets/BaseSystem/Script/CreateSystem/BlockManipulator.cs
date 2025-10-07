using UnityEngine;

/// <summary>
/// ブロックの設置・削除を行う機能の実装
/// </summary>
public class BlockManipulator : MonoBehaviour
{
    private float targetY = 0f;
    private Camera mainCamera;
    private Vector3 prePosition;

    //マウスポインターからレイキャストを飛ばし、指定したY座標に到達した際にX, Z座標の数値を四捨五入し、整数に丸め込む。
    //Y座標はspaceで+1, Lshift or Lctrlで-1. Planeも連動して上下する。
    //あと設置場所を見やすいように半透明でブロックをおす
    //プレイヤーの事を考え、四則演算(丸め込み)方式とレイキャスト方式を用意し、切り替えられるようにする。

    private void Update()
    {
        GetSnappedPoint();
    }

    /// <summary>
    /// 丸め込んだ座標の取得
    /// </summary>
    /// <returns></returns>
    private Vector3? GetSnappedPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float t = (targetY - ray.origin.y) / ray.direction.y;
        if (ray.direction.y == 0f || t < 0f)
        {
            return null;
        }

        Vector3 hitPoint = ray.origin + ray.direction * t;

        Vector3 snapped = new Vector3(Mathf.Round(hitPoint.x), targetY, Mathf.Round(hitPoint.z));
        return snapped;
    }

    private void SetBlock()
    {
        Vector3? nowMousePosition = GetSnappedPoint();
        if(prePosition != nowMousePosition)
        {

        }
    }
}
