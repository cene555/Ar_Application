using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionButton : MonoBehaviour
{
    #region Singleton
    private static HandCollider _instance;
    public static HandCollider Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }
    #endregion
    public GameObject currentObject;
    public GameObject nextObject;
    public Material defaultMaterial;
    public Material targetMaterial;
    public float timeClick = 1.5f;
    private bool canClick = true;
    private TrackingInfo tracking;
    private Vector3 currentPosition;
    private bool triggerCreateButton = false;
    // Update is called once per frame
    void Update()
    {
        tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
        //currentPosition = Camera.main.ViewportToWorldPoint(new Vector3(tracking.palm_center.x, tracking.palm_center.y, tracking.depth_estimation));
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        if (triggerCreateButton)
        {
            if (canClick)
            {
                if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.CLICK)
                {
                    GameObject prefab = (GameObject)Instantiate(nextObject, currentObject.transform.position, currentObject.transform.rotation);
                    Destroy(currentObject);
                    canClick = false;
                }
            }
        }
        if (canClick == false)
        {
            Invoke("CanClick", timeClick);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        triggerCreateButton = true;
        gameObject.GetComponent<Renderer>().material = targetMaterial;
    }
    private void OnTriggerExit(Collider other)
    {
        triggerCreateButton = false;
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
    }
    private void CanClick()
    {
        canClick = true;
    }
}

