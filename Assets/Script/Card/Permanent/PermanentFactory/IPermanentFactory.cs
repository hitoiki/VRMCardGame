using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPermanentFactory
{
    //IPermanent作るだけっていう
    IPermanent CardMake(ICard cardData, IDeck deck);
}
