using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveSensetivitySlider : MonoBehaviour
{
    public enum SLIDERTYPE { CamaraPan, ZoomSpeed }
    [SerializeField]
    public SLIDERTYPE type;

    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case SLIDERTYPE.CamaraPan:
                KeyControls.speed = slider.value;
                break;
            case SLIDERTYPE.ZoomSpeed:
                KeyControls.zoomSpeed = slider.value;
                break;
        }
        
        
    }
}
