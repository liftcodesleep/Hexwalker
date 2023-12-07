using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class EffectStack 
{


    

    private readonly List<Effect> effects;

    

    private Dictionary<Player, List<string>> playerNames;

    private static readonly int secondsToAct = 10;
    private static readonly int secondsForError = 2;

    public EffectStack() 
    { 
    
        effects = new List<Effect>();

    
    }


    public void Tick()
    {

    }

    public void Push(Effect effect)
    {
        
        effects.Add( effect );
    }

    public void UpdateStack()
    {
        if(effects.Count  == 0) { return; }

        //this.effects[1].Time = 1f; //e -= 1f; ;
        effects[^1].ResolveTime--;
        if (effects[^1].ResolveTime < 0)
        {
            Debug.Log("NOOOOT YETTT");
            Pop();
        }

    }

    public void Pop()
    {
        if (effects.Count == 0) { return; } 

        effects[^ 1].ImmediateEffect();
        effects.RemoveAt(effects.Count - 1);
        
    }

    public Effect Peek() 
    {
        if (effects.Count == 0) return null;
        return effects[^1];
    
    }
}
