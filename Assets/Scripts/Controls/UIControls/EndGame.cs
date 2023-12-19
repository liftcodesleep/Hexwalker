using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    [SerializeField]
    public TMPro.TMP_Text EndText;

    [SerializeField]
    public GameObject EndScreen;

    // Start is called before the first frame update
    void Start()
    {
        EndScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.players[Game.GetHumanPlayer()].Avatar.Health <= 0)
        {
            EndScreen.SetActive(true);
            EndText.text = "You LOOSE!!";
        }
        if (Game.players[(Game.GetHumanPlayer() + 1) % 2].Avatar.Health <= 0)
        {
            EndScreen.SetActive(true);
            EndText.text = "You Win!!";
        }
    }
}
