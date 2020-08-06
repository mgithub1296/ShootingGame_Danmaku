using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    /*
    public Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        //スタートから中継地点をつなぐベクトル上を走る点の位置
        var a = Vector3.Lerp(p0, p1, t); // 緑色の点1
        //中継地点からターゲットまでをつなぐベクトル上を走る点の位置
        var b = Vector3.Lerp(p1, p2, t); // 緑色の点2

        //上の二点をつなぐベクトル上を走る点（弾）の位置
        return Vector3.Lerp(a, b, t);    // 黒色の点
    }
    */
    public Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var a = Vector3.Lerp(p0, p1, t); // 緑色の点1
        var b = Vector3.Lerp(p1, p2, t); // 緑色の点2
        var c = Vector3.Lerp(p2, p3, t); // 緑色の点3

        var d = Vector3.Lerp(a, b, t);   // 青色の点1
        var e = Vector3.Lerp(b, c, t);   // 青色の点2

        return Vector3.Lerp(d, e, t);    // 黒色の点
    }
}
