using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStateLinker : MonoBehaviour, IGameState
{
    public List<ISkillEffect> effects = new List<ISkillEffect>();
    [SerializeField] private int effectCount;

    private void Update()
    {
        effectCount = effects.Count;

    }
    public void CrankIn()
    {
        foreach (ISkillEffect effect in effects)
        {
            effect.Play();
        }
    }
    public void StateUpdate()
    {

    }
    public void CrankUp()
    {
        foreach (ISkillEffect effect in effects)
        {
            effect.Pause();
        }
    }
}
