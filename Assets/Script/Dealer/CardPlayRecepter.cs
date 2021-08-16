using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayRecepter : MonoBehaviour, IHandPuttable
{
    //カードの使用を取り扱う
    //Rayかなんかで受け取る

    [SerializeField] private CardDealer dealer;

    public void HandPut(HandCard c)
    {
        var useList = c.card.UseEffect();
        if (useList != null)
        {
            foreach (IUseEffect u in useList)
            {
                u.Effect(dealer);
            }
        }
        else Debug.Log("vanira");
    }
}
