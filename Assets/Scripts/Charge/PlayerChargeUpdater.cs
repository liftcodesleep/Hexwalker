using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeUpdater : MonoBehaviour
{

    [SerializeField]
    public TMPro.TMP_Text[] manaText;

    

    // Start is called before the first frame update
    void Start()
    {


        

    }

    // Update is called once per frame
    void Update()
    {

        manaText[0].text = Game.players[0].Pool.Holy.ToString() ;
        manaText[1].text = Game.players[0].Pool.Unholy.ToString();
        manaText[2].text = Game.players[0].Pool.Slip.ToString();
        manaText[3].text = Game.players[0].Pool.Essence.ToString();
        manaText[4].text = Game.players[0].Pool.Holy.ToString();


    }

    

   
}
