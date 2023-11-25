using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStack 
{
  private readonly List<Effect> effects;

  public EffectStack() { 
    effects = new List<Effect>();
  }

  public void Push(Effect effect) {
    effects.Add( effect );
  }

  public void UpdateStack() {
    if(effects.Count  == 0) { return; }
    //this.effects[1].Time = 1f; //e -= 1f; ;
    effects[^1].ResolveTime--;
    if (effects[^1].ResolveTime < 0) {
      Pop();
    }
  }

  public void Pop() {
    if (effects.Count == 0) { return; } 
    effects[^ 1].ImmediateEffect();
    effects.RemoveAt(effects.Count - 1);
  }
}
