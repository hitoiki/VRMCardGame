using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardFactory
{
    //StageCardViewerのオブジェクト生成を担うクラス
    //こいつが表示用のオブジェクトを生成するので、それをViewerが並べて、表示する

    //生成して、指定の位置において、渡す
    //今後、FactoryがDeckを読んで生成する仕組みに変える
    //多分CardMake、EraceAtは要らなそうなので消して、Deck購読用のメソッドを立てる
    ICardPrintable CardMake(ICard card, Vector3 position);
    void CardEraceAt(int index);
    List<ICardPrintable> GetCards();
}
