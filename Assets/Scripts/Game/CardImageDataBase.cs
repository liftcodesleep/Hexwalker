using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CardImageDataBase : MonoBehaviour
{
    [System.Serializable]
    public struct ImageDict
    {
        public string Name;
        public Sprite Image;
    }


    
    [SerializeField]
    private ImageDict[] images;






    public Sprite GetImage(string name)
    {
        foreach (ImageDict currentItem in images)
        {

            if (currentItem.Name.Equals(name))
            {
                return currentItem.Image;
            }
        }

        return null;
    }
}
