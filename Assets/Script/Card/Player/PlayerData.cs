using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UniRx;

public class PlayerData : MonoBehaviour
{
    //Playerのデータ
    public Animator vrmAnimator;
    [SerializeField] private CardData initPlayerCard;
    private ICard playerCard;


    //別枠でplayerのHpを定義
    //これらの値はFacadeのみ弄る
    [SerializeField] int initHP;
    private ReactiveProperty<int> _hp = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> hp => _hp;
    //DeckBuilding用
    public ReactiveProperty<int> instantMoney = new ReactiveProperty<int>();
    public ReactiveProperty<int> actionTimes = new ReactiveProperty<int>();
    [SerializeField] int initTurn;
    public ReactiveProperty<int> turn = new ReactiveProperty<int>();

    //デフォルトのポーズ
    [SerializeField, PathAttribute] private string defaultPoseFilePath = "";
    public PoseItem defaultPoseItem;


    private void Awake()
    {
        _hp.Value = initHP;
        turn.Value = initTurn;
        instantMoney.Value = 0;
        actionTimes.Value = 1;
        playerCard = new DefaultCard(initPlayerCard);
        PoseSet();
    }

    [ContextMenu("PoseSet")]
    private void PoseSet()
    {
        if (defaultPoseFilePath == "") return;
        string json = File.ReadAllText(defaultPoseFilePath);
        defaultPoseItem = JsonUtility.FromJson<PoseItem>(json);

    }

    public void Damage(int amount)
    {
        _hp.Value -= amount;
        if (_hp.Value <= 0) GameOver();
    }

    public void GameEnd()
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
