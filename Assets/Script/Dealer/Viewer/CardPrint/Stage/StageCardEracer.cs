using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCardEracer : MonoBehaviour, IGameState
{
    //CrankInかCrankUpでViewerの表示を消す
    [SerializeField] private List<StageCardViewer> viewers;
    [SerializeField] private StateMethodKind kind;

    public void CrankIn()
    {
        if (kind == StateMethodKind.CrankIn)
        {
            foreach (StageCardViewer viewer in viewers)
            {
                viewer.CardReset();
            }
        }
    }
    //Update
    public void StateUpdate()
    {

    }
    //End
    public void CrankUp()
    {
        if (kind == StateMethodKind.CrankUp)
        {
            foreach (StageCardViewer viewer in viewers)
            {
                viewer.CardReset();
            }
        }
    }
    private enum StateMethodKind
    {
        CrankIn, CrankUp
    }
}


