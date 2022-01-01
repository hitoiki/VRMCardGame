using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

public class PlayerData : MonoBehaviour
{
    //Playerのデータ
    public Transform vrmTransform;
    [SerializeField] private CardData initPlayerCard;
    [SerializeField] private GameObject initPrintable;
    [SerializeField] private ICardPrintable printable;
    private ICard playerCard;


    //別枠でplayerのHpを定義
    //これらの値はFacadeのみ弄る
    [SerializeField] int initHP;
    private ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> hp => _hp;
    //勝利条件。スコアチックにしたいのでintで作る
    public ReactiveProperty<int> flag = new ReactiveProperty<int>();

    private void Awake()
    {
        _hp.Value = initHP;
        playerCard = new DefaultCard(initPlayerCard);
        printable = initPrintable.GetComponent<ICardPrintable>();
    }

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
