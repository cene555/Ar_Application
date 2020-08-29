using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretButton : MonoBehaviour
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
    public List<GameObject> spawn;
    public GameObject projectile;
    public float force = 1f;
    public Material defaultMaterial;
    public Material targetMaterial;
    private bool isTrigger = false;
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
    }
    void Update()
    {
        HandInfo detectedHand = ManomotionManager.Instance.Hand_infos[0].hand_info;

        if (isTrigger)
        {
            if (detectedHand.gesture_info.mano_gesture_trigger == ManoGestureTrigger.CLICK)
            {
                foreach(GameObject p in spawn)
                {
                    GameObject shot = (GameObject)Instantiate(projectile, p.transform.position, p.transform.rotation);
                    shot.GetComponent<Rigidbody>().AddForce(p.transform.forward * force); ;                  
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        isTrigger = true;
        gameObject.GetComponent<Renderer>().material = targetMaterial;
    }
    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
    }
}
