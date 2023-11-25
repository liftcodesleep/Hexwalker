using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkingText : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI textComponent;
    [SerializeField]
    private string[] lines;
    [SerializeField]
    private float textSpeed;

    private int index;
    
    void Start() {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if(textComponent.text == lines[index])
        //    {
        //        NextLine();
        //    }
        //    else
        //    {
        //        StopAllCoroutines();
        //        textComponent.text = lines[index];
        //    }
        //}
    }

    

    void StartDialogue() {
        this.index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        foreach(char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        
    }

    public void NextLine() {
        if(index < lines.Length - 1) {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine (TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetText(string[] newText) {
        lines = newText;

    }
}
