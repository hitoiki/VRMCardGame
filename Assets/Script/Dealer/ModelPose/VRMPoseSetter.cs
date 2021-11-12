using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class VRMPoseSetter
{
    //外からちょちょっと参考にしてきた
    List<HumanBodyBones> BoneList;

    void OnEnable()
    {
        BoneList = new List<HumanBodyBones>();
        BoneList.Add(HumanBodyBones.Head);
        BoneList.Add(HumanBodyBones.Neck);
        BoneList.Add(HumanBodyBones.Hips);
        BoneList.Add(HumanBodyBones.Spine);
        BoneList.Add(HumanBodyBones.Chest);
        BoneList.Add(HumanBodyBones.UpperChest);
        BoneList.Add(HumanBodyBones.LeftUpperArm);
        BoneList.Add(HumanBodyBones.LeftLowerArm);
        BoneList.Add(HumanBodyBones.LeftHand);
        BoneList.Add(HumanBodyBones.RightUpperArm);
        BoneList.Add(HumanBodyBones.RightLowerArm);
        BoneList.Add(HumanBodyBones.RightHand);
        BoneList.Add(HumanBodyBones.LeftUpperLeg);
        BoneList.Add(HumanBodyBones.LeftLowerLeg);
        BoneList.Add(HumanBodyBones.LeftFoot);
        BoneList.Add(HumanBodyBones.RightUpperLeg);
        BoneList.Add(HumanBodyBones.RightLowerLeg);
        BoneList.Add(HumanBodyBones.RightFoot);
    }

    public void PoseSet(Animator anim, string poseText)
    {

        string[] pose = poseText.Replace("\n", ",").Split(',');
        int i = 0;
        foreach (HumanBodyBones Bone in BoneList)
        {
            anim.GetBoneTransform(Bone).localEulerAngles = GetVector3(pose, i);
            i++;
        }
        Debug.Log("Complete");

    }

    Vector3 GetVector3(string[] pose, int index)
    {
        index *= 3;
        float x = float.Parse(pose[index + 0]);
        float y = float.Parse(pose[index + 1]);
        float z = float.Parse(pose[index + 2]);
        var vec3 = new Vector3(x, y, z);
        return vec3;
    }
}