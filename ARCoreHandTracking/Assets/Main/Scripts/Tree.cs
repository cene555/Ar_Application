using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
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
    private bool isPrefabMove = false;
    private bool createPrefab = false;
    private TrackingInfo tracking;
    private Vector3 currentPosition;
    private bool isSpin = false;


    void Update()
    {
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        if (createPrefab)
        {
            if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.PICK)
            {
                isPrefabMove = true;
                isSpin = false;
            }
            if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous.ToString() == "POINTER_GESTURE")
            {
                isSpin = true;
                isPrefabMove = false;
            }
            else
            {
                isSpin = false;
            }
        }
        if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.DROP | detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.RELEASE_GESTURE)
        {
            isPrefabMove = false;
            isSpin = false;
        }
        PrefabMove();
        PrefabSpin();
    }

    private void OnTriggerEnter(Collider other)
    {
        createPrefab = true;
    }
    private void OnTriggerExit(Collider other)
    {
        createPrefab = false;
    }


    private void PrefabMove()
    {
        if (isPrefabMove)
        {
            tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
            currentPosition = Camera.main.ViewportToWorldPoint(new Vector3(tracking.palm_center.x, tracking.palm_center.y, tracking.depth_estimation));
            transform.position = currentPosition;
        }
    }
    private void PrefabSpin()
    {
        if (isSpin)
        {
            transform.Rotate(0, 2, 0);
        }
    }
}
