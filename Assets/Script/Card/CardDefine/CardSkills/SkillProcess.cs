using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Cardの処理を示すデリゲート
public delegate void SkillProcess(CardFacade dealer);
public delegate bool IsSkillable(CardFacade dealer);