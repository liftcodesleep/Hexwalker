using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI m_TextMeshPro;

    void Start()
    {
        m_TextMeshPro = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Game.players[Game.GetHumanPlayer()].Avatar != null)
        {

            
            m_TextMeshPro.text = Game.players[Game.GetHumanPlayer()].Avatar.Health.ToString();
        }
        else
        {
            m_TextMeshPro.text = "0";
        }
        
    }
}
