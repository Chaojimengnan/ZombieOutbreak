using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ani : MonoBehaviour
{
    public Text zimu;
    public Text item;
    public Image myimage;
    public Image zhunxin;
    public void setZimu(string thetext)
    {
        zimu.text = thetext;
    }
    public void setItem(string thetext)
    {
        item.text = thetext;
    }
    public void setImage(Color color)
    {
        myimage.color = color;
        //myimage.CrossFadeColor(new Color(1,0,0),0.1f, false, false);
        //myimage.CrossFadeAlpha(1, 0.1f, false);
    }
    public void setzhunxin(bool active)
    {
        zhunxin.enabled = active;
    }
}
