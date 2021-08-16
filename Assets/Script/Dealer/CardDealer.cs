using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    //Cardが出来る処理を書く   
    //CardのEffect群にこいつが渡される
    [SerializeField] private Stage stage;

    //カードを引いて、適当な場所に移動
    public void cardDrow(FieldDecksEnum from, FieldDecksEnum to, int amount)
    {
        stage.DeckKey(to).Add(stage.DeckKey(from).Draw(amount));
    }
    //条件を満たすカードのリストを渡す
    public List<Card> DeckFilter(FieldDecksEnum f, System.Func<Card, bool> ch)
    {
        return stage.DeckKey(f).cards.Where(ch).ToList();
    }
    //あるデッキ全てにCoinを渡す

}

