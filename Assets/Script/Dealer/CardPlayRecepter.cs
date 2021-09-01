using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayRecepter : MonoBehaviour ,IHandPuttable
{
    //カードの使用を取り扱う
    //Rayかなんかで受け取る

    [SerializeField] private CardDealer dealer = null;
    [SerializeField] private Stage stage = null;

    public void HandPut(HandCard c)
    {
        c.card.UseEffect(dealer);
    }
}
