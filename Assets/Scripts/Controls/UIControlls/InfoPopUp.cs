using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopUp : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI m_TextMeshPro;
    [SerializeField]
    private RawImage TimerBar;

    [SerializeField]
    private GameObject Window;


    private void Start()
    {
        m_TextMeshPro.text = "Testing";
        StartCoroutine(stackControll());
    }

    // Update is called once per frame
    void Update()
    {
        if(Game.stack.Peek() == null)
        {
            Window.SetActive(false);
            m_TextMeshPro.text = "Nothing is on the stack";
            return;
        }
        Window.SetActive(true);
        
        
        m_TextMeshPro.text = Game.stack.Peek().Desctiption;

        TimerBar.transform.localScale = new Vector3(  Game.stack.Peek().ResolveTime  / Game.stack.Peek().maxResolveTime, 1, 1);


    }


    IEnumerator stackControll()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            Game.stack.UpdateStack();
        }
    }
}
