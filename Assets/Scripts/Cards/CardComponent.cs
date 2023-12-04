using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CardComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  public Card card;
  [SerializeField]
  public  GameObject PreFabs;
  [SerializeField]
  public TMPro.TMP_Text Title;
  [SerializeField]
  public TMPro.TMP_Text Type;
  private Vector3 scaleStaring = new Vector3(0.00730778277f,0.0270729158f,0.0539752766f);
  private bool Shrinking = false;

  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() {
  }

  public void SetCard(Card card) {
    this.card = card;
    Title.text = card.Name;
    Type.text = card.type.ToString();
  }
  
  public void OnPointerEnter(PointerEventData eventData) {
    StartCoroutine(GrowCard());
  }

  public void OnPointerExit(PointerEventData eventData) {
    StartCoroutine(ShrinkCard());
  }

  IEnumerator GrowCard() {
    Shrinking = false;
    //Debug.Log("Growing");
    while(this.transform.localScale.magnitude < scaleStaring.magnitude*1.7f) {
      if (Shrinking) break;
      yield return new WaitForSeconds(.005f);
      this.transform.localScale *= 1.1f;
    }
  }

  IEnumerator ShrinkCard() {
    Shrinking = true;
    //Debug.Log("Getting smaller");
    while (this.transform.localScale.magnitude > scaleStaring.magnitude) {
      yield return new WaitForSeconds(.005f);
      this.transform.localScale *= .9f;
    }
  }
}
