using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCubeBehavior : MonoBehaviour
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
    private TrackingInfo tracking;
    private Vector3 currentPosition;
    private Camera cam;
    private bool canMove = false;
    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        DetectButtonMove();
    }
    private void UpdatePosition()
    {
    tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
    currentPosition = Camera.main.ViewportToWorldPoint(new Vector3(tracking.palm_center.x, tracking.palm_center.y, tracking.depth_estimation));
    transform.position = currentPosition;
    transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);
    
    

    }
    void DetectButtonMove()
    {
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.GRAB_GESTURE)
        {
            canMove = true;
        }
        else if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.DROP | detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.RELEASE_GESTURE | detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.PICK)
        {
            canMove = false;
        }
        else if(ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous.ToString() == "POINTER_GESTURE")
        {
           canMove = false;
        }
            if (canMove)
        {
            UpdatePosition();
        }
            /*
            if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous.ToString() == "CLOSED_HAND_GESTURE")
            {
                UpdatePosition();
            }
            */
        }
}