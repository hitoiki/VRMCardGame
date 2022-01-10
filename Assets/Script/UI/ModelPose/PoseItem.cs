using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PoseItem
{
    public BoneItem chest;
    public BoneItem head;
    public BoneItem hips;
    public BoneItem jaw;
    public BoneItem leftEye;
    public BoneItem leftFoot;
    public BoneItem leftHand;
    public BoneItem leftIndexDistal;
    public BoneItem leftIndexIntermediate;
    public BoneItem leftIndexProximal;
    public BoneItem leftLittleDistal;
    public BoneItem leftLittleIntermediate;
    public BoneItem leftLittleProximal;
    public BoneItem leftLowerArm;
    public BoneItem leftLowerLeg;
    public BoneItem leftMiddleDistal;
    public BoneItem leftMiddleIntermediate;
    public BoneItem leftMiddleProximal;
    public BoneItem leftRingDistal;
    public BoneItem leftRingIntermediate;
    public BoneItem leftRingProximal;
    public BoneItem leftShoulder;
    public BoneItem leftThumbDistal;
    public BoneItem leftThumbIntermediate;
    public BoneItem leftThumbProximal;
    public BoneItem leftToes;
    public BoneItem leftUpperArm;
    public BoneItem leftUpperLeg;
    public BoneItem neck;
    public BoneItem rightEye;
    public BoneItem rightFoot;
    public BoneItem rightHand;
    public BoneItem rightIndexDistal;
    public BoneItem rightIndexIntermediate;
    public BoneItem rightIndexProximal;
    public BoneItem rightLittleDistal;
    public BoneItem rightLittleIntermediate;
    public BoneItem rightLittleProximal;
    public BoneItem rightLowerArm;
    public BoneItem rightLowerLeg;
    public BoneItem rightMiddleDistal;
    public BoneItem rightMiddleIntermediate;
    public BoneItem rightMiddleProximal;
    public BoneItem rightRingDistal;
    public BoneItem rightRingIntermediate;
    public BoneItem rightRingProximal;
    public BoneItem rightShoulder;
    public BoneItem rightThumbDistal;
    public BoneItem rightThumbIntermediate;
    public BoneItem rightThumbProximal;
    public BoneItem rightToes;
    public BoneItem rightUpperArm;
    public BoneItem rightUpperLeg;
    public BoneItem spine;
    public BoneItem upperChest;

    //象さんにお任せ Enumの速度をどうにかするにはこれの他ない
    public BoneItem SeekBone(HumanBodyBones bodyBones)
    {
        if (bodyBones == HumanBodyBones.Chest) return chest;
        if (bodyBones == HumanBodyBones.Head) return head;
        if (bodyBones == HumanBodyBones.Hips) return hips;
        if (bodyBones == HumanBodyBones.Jaw) return jaw;
        if (bodyBones == HumanBodyBones.LeftEye) return leftEye;
        if (bodyBones == HumanBodyBones.LeftFoot) return leftFoot;
        if (bodyBones == HumanBodyBones.LeftHand) return leftHand;
        if (bodyBones == HumanBodyBones.LeftIndexDistal) return leftIndexDistal;
        if (bodyBones == HumanBodyBones.LeftIndexIntermediate) return leftIndexIntermediate;
        if (bodyBones == HumanBodyBones.LeftIndexProximal) return leftIndexProximal;
        if (bodyBones == HumanBodyBones.LeftLittleDistal) return leftLittleDistal;
        if (bodyBones == HumanBodyBones.LeftLittleIntermediate) return leftLittleIntermediate;
        if (bodyBones == HumanBodyBones.LeftLittleProximal) return leftLittleProximal;
        if (bodyBones == HumanBodyBones.LeftLowerArm) return leftLowerArm;
        if (bodyBones == HumanBodyBones.LeftLowerLeg) return leftLowerLeg;
        if (bodyBones == HumanBodyBones.LeftMiddleDistal) return leftMiddleDistal;
        if (bodyBones == HumanBodyBones.LeftMiddleIntermediate) return leftMiddleIntermediate;
        if (bodyBones == HumanBodyBones.LeftMiddleProximal) return leftMiddleProximal;
        if (bodyBones == HumanBodyBones.LeftRingDistal) return leftRingDistal;
        if (bodyBones == HumanBodyBones.LeftRingIntermediate) return leftRingIntermediate;
        if (bodyBones == HumanBodyBones.LeftRingProximal) return leftRingProximal;
        if (bodyBones == HumanBodyBones.LeftShoulder) return leftShoulder;
        if (bodyBones == HumanBodyBones.LeftThumbDistal) return leftThumbDistal;
        if (bodyBones == HumanBodyBones.LeftThumbIntermediate) return leftThumbIntermediate;
        if (bodyBones == HumanBodyBones.LeftThumbProximal) return leftThumbProximal;
        if (bodyBones == HumanBodyBones.LeftToes) return leftToes;
        if (bodyBones == HumanBodyBones.LeftUpperArm) return leftUpperArm;
        if (bodyBones == HumanBodyBones.LeftUpperLeg) return leftUpperLeg;
        if (bodyBones == HumanBodyBones.Neck) return neck;
        if (bodyBones == HumanBodyBones.RightEye) return rightEye;
        if (bodyBones == HumanBodyBones.RightFoot) return rightFoot;
        if (bodyBones == HumanBodyBones.RightHand) return rightHand;
        if (bodyBones == HumanBodyBones.RightIndexDistal) return rightIndexDistal;
        if (bodyBones == HumanBodyBones.RightIndexIntermediate) return rightIndexIntermediate;
        if (bodyBones == HumanBodyBones.RightIndexProximal) return rightIndexProximal;
        if (bodyBones == HumanBodyBones.RightLittleDistal) return rightLittleDistal;
        if (bodyBones == HumanBodyBones.RightLittleIntermediate) return rightLittleIntermediate;
        if (bodyBones == HumanBodyBones.RightLittleProximal) return rightLittleProximal;
        if (bodyBones == HumanBodyBones.RightLowerArm) return rightLowerArm;
        if (bodyBones == HumanBodyBones.RightLowerLeg) return rightLowerLeg;
        if (bodyBones == HumanBodyBones.RightMiddleDistal) return rightMiddleDistal;
        if (bodyBones == HumanBodyBones.RightMiddleIntermediate) return rightMiddleIntermediate;
        if (bodyBones == HumanBodyBones.RightMiddleProximal) return rightMiddleProximal;
        if (bodyBones == HumanBodyBones.RightRingDistal) return rightRingDistal;
        if (bodyBones == HumanBodyBones.RightRingIntermediate) return rightRingIntermediate;
        if (bodyBones == HumanBodyBones.RightRingProximal) return rightRingProximal;
        if (bodyBones == HumanBodyBones.RightShoulder) return rightShoulder;
        if (bodyBones == HumanBodyBones.RightThumbDistal) return rightThumbDistal;
        if (bodyBones == HumanBodyBones.RightThumbIntermediate) return rightThumbIntermediate;
        if (bodyBones == HumanBodyBones.RightThumbProximal) return rightThumbProximal;
        if (bodyBones == HumanBodyBones.RightToes) return rightToes;
        if (bodyBones == HumanBodyBones.RightUpperArm) return rightUpperArm;
        if (bodyBones == HumanBodyBones.RightUpperLeg) return rightUpperLeg;
        if (bodyBones == HumanBodyBones.Spine) return spine;
        if (bodyBones == HumanBodyBones.UpperChest) return upperChest;
        return null;
    }

}

[System.Serializable]
public class BoneItem
{
    // positionは必須でない
    // public double[] position;

    // 関節角度の四元数
    public float[] rotation;
}