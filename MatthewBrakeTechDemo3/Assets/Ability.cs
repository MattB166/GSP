using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public float manaCost;
    public float castingTime;


    public abstract void ExecuteAbility();
    

}
