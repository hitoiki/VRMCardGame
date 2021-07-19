using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
#pragma warning disable 0649
public class FieldViewer : MonoBehaviour, IGameStated
{
    //CardFieldを表示する
    //CardPrefabを用いてインスタンス化する
    //UI辺りの兼ね合いはよく分からん
    [SerializeField] private CardField cardField;
    [SerializeReference, SubclassSelector(true)] private List<ICardPrinted> fieldPrinted;

    //InterFaceをインスペクタで取り扱う時はMonoを使えないらしい
    //さてどうしたものか

    //Start
    public void CrankIn()
    {
        cardField.field.Subscribe(x =>
        {
            foreach (var i in x.cards.Select((Card card, int index) => new { card, index }))
            {
                fieldPrinted[i.index].Print(i.card);
            }
        });
    }
    //Update
    public void StateUpdate()
    {

    }
    //End
    public void CrankUp()
    {
        cardField.field.Dispose();
    }



}
