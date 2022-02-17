using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStagingDeck : IDeck
{
    //Stageに乗っける用のDeck
    void Init(DeckType deck);
}
