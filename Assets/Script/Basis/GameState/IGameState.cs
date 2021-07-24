using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public interface IGameState
{
    //これから書く全てのオブジェクトはこれを継承する
    //Stateクラスデザインによる革新的なゲーム体験を齎してやるぜ

    //基本的にGameStateが大量にこのクラスを保有、管轄する
    //通常のUpdate、Startは書かない事

    //Start
    void CrankIn();
    //Update
    void StateUpdate();
    //End
    void CrankUp();

    /*
        //Start
        public void CrankIn() { }
        //Update
        public void StateUpdate() { }
        //End
        public void CrankOut() { }
    */
}
