using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureSelect : MonoBehaviour
{
    [SerializeField] private List<Texture2D> tex = new List<Texture2D>();
    private int i=0;
    private bool isdown = false;
    void Start()
    {
        this.gameObject.GetComponent<Image>().material.SetTexture("_MainTex", tex[0]);
    }

    // Update is called once per frame
    void Update()
    {
        //A
        if (OVRInput.Get(OVRInput.RawButton.A, OVRInput.Controller.RTouch)) {
            if(isdown == false){
                isdown = true;
                Debug.Log("A");
                i = (i+tex.Count-1)%tex.Count;
                Debug.Log(i);
                this.gameObject.GetComponent<Image>().material.SetTexture("_MainTex", tex[i]);
                this.gameObject.GetComponent<Image>().enabled = (false);
                this.gameObject.GetComponent<Image>().enabled = (true);
            }
        }else if (OVRInput.Get(OVRInput.RawButton.B, OVRInput.Controller.RTouch)) {
            if(isdown == false){
                isdown = true;
                Debug.Log("B");
                i = (i+1)%tex.Count;
                this.gameObject.GetComponent<Image>().material.SetTexture("_MainTex", tex[i]);
                this.gameObject.GetComponent<Image>().enabled = (false);
                this.gameObject.GetComponent<Image>().enabled = (true);
                Debug.Log(i);
                }
        }else{
            isdown = false;
        }

        
    }
}
