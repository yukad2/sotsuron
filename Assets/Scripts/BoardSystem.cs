using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSystem : MonoBehaviour
{
    private GameObject leftTopAnchor;
    private GameObject rightBottomAnchor;

    private GameObject camObj;
    private GameObject board;

    [SerializeField] private GameObject lController;
    [SerializeField] private GameObject rController;
    [SerializeField] private GameObject pen;


    // Start is called before the first frame update
    void Start()
    {
        leftTopAnchor = this.transform.Find("LeftTopAnchor").gameObject;
        rightBottomAnchor = this.transform.Find("RightBottomAnchor").gameObject;
        board = this.transform.Find("Board").gameObject;
        camObj = board.transform.Find("Camera").gameObject;
        boardSetup();
    }

    // Update is called once per frame
    void Update()
    {
        //左手手前Y・右下
        if (OVRInput.Get(OVRInput.RawButton.Y, OVRInput.Controller.LTouch)) {
            Vector3 lControllerPos = lController.transform.position;
            rightBottomAnchor.transform.position = lControllerPos;
            boardSetup();
            pen.SetActive(true);
        }
        
        //左手奥X・左上
        if (OVRInput.Get(OVRInput.RawButton.X, OVRInput.Controller.LTouch)) {
            Vector3 lControllerPos = lController.transform.position;
            leftTopAnchor.transform.position = lControllerPos;
        }

    }

    void boardSetup(){
        Vector3 center = (leftTopAnchor.transform.position + rightBottomAnchor.transform.position) / 2;
        float width = Mathf.Sqrt(Mathf.Pow((leftTopAnchor.transform.position.x - rightBottomAnchor.transform.position.x),2) + Mathf.Pow((leftTopAnchor.transform.position.z - rightBottomAnchor.transform.position.z),2));
        float height = Mathf.Abs(leftTopAnchor.transform.position.y - rightBottomAnchor.transform.position.y);
        Debug.Log("width:"+width);
        Debug.Log("height:"+height);
        board.transform.position = center;
        var scale = board.transform.localScale;
        scale.z =  width/10;
        scale.x = height/10;
        board.transform.localScale = scale;

        
        Vector3 camPos = camObj.transform.localPosition;
        camPos.y = width/2;
        camObj.transform.localPosition = camPos;
    }
}
