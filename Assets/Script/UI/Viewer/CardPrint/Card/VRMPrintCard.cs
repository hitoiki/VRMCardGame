using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Reflection;

#pragma warning disable 0649
public class VRMPrintCard : MonoBehaviour, ICardViewable, ICardObservable
{
    // VRMのカードをやる
    private ReactiveProperty<ICard> card = new ReactiveProperty<ICard>();
    [SerializeField] private Image BackImage;
    [SerializeField] private Image FrontImage;
    [SerializeField] private Text nameText;
    [SerializeField] private PlayerData player;
    public void Print(IPermanent card)
    {
        this.card.Value = card.GetCard();
        nameText.text = card.GetCard().GetCardData().textName;
        BackImage.sprite = card.GetCard().GetCardData().backSprite;
        FrontImage.sprite = card.GetCard().GetCardData().frontSprite;

        //ここでVRMのポーズを変える
        if (player.vrmAnimator == null) return;
        PoseItem cardPose = card.GetCard().GetCardData().poseItem;
        //Chestのrotetionの配列数で確認
        if (cardPose.chest.rotation.Length < 4) cardPose = player.defaultPoseItem;

        EnumSonicForeach<HumanBodyBones>.Exec(x =>
        {
            BoneItem boneItem = cardPose.SeekBone(x);
            if (boneItem == null) return;
            if (boneItem.rotation == null) return;
            var boneTrans = player.vrmAnimator.GetBoneTransform(x);
            float[] rotation = boneItem.rotation;
            if (boneTrans != null && rotation.Length >= 4)
            {
                boneTrans.localRotation = new Quaternion(-rotation[0], -rotation[1], rotation[2], rotation[3]);
            }
        });
    }

    public void UnPrint()
    {

    }

    public IReadOnlyReactiveProperty<ICard> ObservableCard()
    {
        return card;
    }

    public void Active(bool boo)
    {

    }
}