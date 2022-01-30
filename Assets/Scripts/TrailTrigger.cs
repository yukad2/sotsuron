using UnityEngine;

public class TrailTrigger : MonoBehaviour
{
    [SerializeField] GameObject trailPrefab;
    private TrailRenderer trail;
    private GameObject trailObj;
    private GameObject sphere;

    [SerializeField] private GameObject board;
    private GameObject raycaster;

    void Start(){
        sphere = this.transform.Find("Sphere").gameObject;
        instantiateTrail();
        
        raycaster = this.transform.Find("raycaster").gameObject;

    }
    

    void instantiateTrail(){
        trailObj = Instantiate(trailPrefab, sphere.transform);
        trailObj.transform.SetParent(sphere.transform);
        trail = trailObj.GetComponent<TrailRenderer>();
    }

    void moveTrail(){
        trailObj.transform.SetParent(board.transform);
        trailObj.layer = 8;
        trail = null;
    }

    void Update(){
        if(OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("トリガーを話しました");
            moveTrail();
        }

        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("トリガーを押しました");
            instantiateTrail();
            trail.enabled = true;
        }

        if(OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)&&OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            Debug.Log("削除");
            foreach ( Transform n in board.transform )
            {
                GameObject.Destroy(n.gameObject);
            }
        }
    }
}
