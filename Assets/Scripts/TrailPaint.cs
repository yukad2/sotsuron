using UnityEngine;

public class TrailPaint : MonoBehaviour
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

    void OnTriggerEnter(Collider other){
        Debug.Log("enter");
        instantiateTrail();
        trail.enabled = true;
    }
    void OnTriggerStay(){

        RaycastHit hit;
        int mask = 1 << 7;

        if(Physics.Raycast(raycaster.transform.position, transform.up, out hit, 0.5f, mask)){
            Debug.Log (hit.point);
            sphere.transform.position = hit.point;
        }
    }
    
    void OnTriggerExit(Collider other){
        Debug.Log("Exit");
        moveTrail();
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
}
