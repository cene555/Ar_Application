using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarMenuObject : MonoBehaviour
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
    public Material defaultMaterial;
    public Material targetMaterial;
    public Material pickMaterial;
    public GameObject mainMenu;
    private bool targetScroll = false;
    private bool isMove = false;
    private Vector3 currentPosition;
    public Text myText;
    private TrackingInfo tracking;
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
    }
    void Update()
    {
        myText.text = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous.ToString();
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;
        if (targetScroll)
        {
            gameObject.GetComponent<Renderer>().material = targetMaterial;
            if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.PICK)
            {
                isMove = true;
            }

        }
        if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.DROP | detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.GRAB_GESTURE | detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.RELEASE_GESTURE)
        {
            gameObject.GetComponent<Renderer>().material = defaultMaterial;
            isMove = false;
        }
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        targetScroll = true;
    }
    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
        targetScroll = false;
    }
    private void Move()
    {
        if (isMove)
        {
            gameObject.GetComponent<Renderer>().material = pickMaterial;
            tracking = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info;
            currentPosition = Camera.main.ViewportToWorldPoint(new Vector3(tracking.palm_center.x, tracking.palm_center.y, tracking.depth_estimation));
            transform.position = new Vector3(transform.position.x, currentPosition.y, transform.position.z);
        }
    }
}
