using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurnButton : MonoBehaviour
{
    [SerializeField]
    private GameObject light;

    [SerializeField]
    private CardPanelComponent cardPanel;

    private bool _nextTurnPlaying;

    private Quaternion startingRotation;
    public float dayCycleDuration = 0.01f;  // Duration of one full day cycle in seconds
    float angle = 1;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        startingRotation = light.transform.rotation;
        _nextTurnPlaying = false;
        count = 0;
    }

    

    void Update()
    {

        if(_nextTurnPlaying  )
        {
            // Calculate the rotation angle based on time
            //float angle = (Time.time / dayCycleDuration) * 360.0f;

            // Set the light's rotation
            light.transform.rotation *= Quaternion.Euler(0, angle, 0);
            count++;
        }

        if(count > 360 && _nextTurnPlaying)
        {
            _nextTurnPlaying = false;
            light.transform.rotation = startingRotation;

            
        }

        
    }


    public void NextTurnButtonPress()
    {
        count = 0;
        //_nextTurnPlaying = true;

        //Game.GetCurrentPlayer().Draw(1);
        Game.NextTurn();
        cardPanel.updateCards();
    }
}
