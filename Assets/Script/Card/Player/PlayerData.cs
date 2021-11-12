using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class PlayerData : MonoBehaviour
{
    //Playerのデータ
    public PlayerCardData cardData;
    public Animator vrmPose;
    public Transform vrmTransform;

    //別枠でplayerのHpを定義
    //これらの値はFacadeのみ弄る
    private ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    private IReadOnlyReactiveProperty<int> hp => _hp;
    //勝利条件。スコアチックにしたいのでintで作る
    public ReactiveProperty<int> flag = new ReactiveProperty<int>();

    public void Damage(int amount)
    {
        _hp.Value -= amount;
        if (_hp.Value <= 0) GameOver();
    }

    public void Flag(int amount)
    {
        //本来はスコア用
        //間に合わせとして、クリア処理に
        dealer.ChangeState(gameClearState);
    }



    //GameOver用にGameStateを持っておく
    [SerializeField] private StateDealer dealer;
    [SerializeField] private string gameOverState;
    [SerializeField] private string gameClearState;
    private void GameOver()
    {
        dealer.ChangeState(gameOverState);
    }
}
