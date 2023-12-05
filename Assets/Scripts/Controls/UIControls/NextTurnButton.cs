using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurnButton : MonoBehaviour {
  [SerializeField]
  private GameObject light;
  [SerializeField]
  private CardPanelComponent cardPanel;
  private bool _nextTurnPlaying;
  private Quaternion startingRotation;
  public float dayCycleDuration = 0.01f;  // Duration of one full day cycle in seconds
  float angle = 1;
  int count;
  Button thisButton;

  // Start is called before the first frame update
  void Start() {
    startingRotation = light.transform.rotation;
    _nextTurnPlaying = false;
    count = 0;
    thisButton = GetComponent<Button>();
  }

  

  void Update() {

    if(_nextTurnPlaying) {
      // Calculate the rotation angle based on time
      //float angle = (Time.time / dayCycleDuration) * 360.0f;
      // Set the light's rotation
      light.transform.rotation *= Quaternion.Euler(0, angle, 0);
      count++;
    }
    if(count > 360 && _nextTurnPlaying) {
      _nextTurnPlaying = false;
      light.transform.rotation = startingRotation;
    }
    if (Game.GetCurrentPlayer() != Game.players[0]) {
      thisButton.interactable = false;
    }
    else {
      thisButton.interactable = true;
    }
  }

  public void NextTurnButtonPress() {
    Game.map.UpdateVisible();
    //count = 0;
    //_nextTurnPlaying = true;
    //Game.GetCurrentPlayer().Draw(1);
    Game.NextTurn();
    //cardPanel.updateCards();
  }
}
