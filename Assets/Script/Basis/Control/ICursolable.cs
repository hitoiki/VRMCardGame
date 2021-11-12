using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICursolable
{
    //カーソルを合わせる、クリックできる物

    //Clickされた時にやる
    //
    void Click(Vector3 point, ContactMode timing);
    //カーソルが合ってる間ずっと呼ばれっぱなしの奴
    //出た時入った時は内部状態とってなんかして
    void Cursol(Vector3 point, ContactMode timing);
}

public enum ContactMode
{
    Enter, Stay, Exit
}