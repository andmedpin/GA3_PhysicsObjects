using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject PlayerControllerTest;
    private GameObject currentlySelectedObject, lastSelectedObject;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Destroying duplicate instance");
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Debug.Log("Hello World");
    }

    void Update()
    {
        currentlySelectedObject = CastForPhysicsObject();

        if (currentlySelectedObject != null)
        {
            lastSelectedObject = currentlySelectedObject;
            currentlySelectedObject.GetComponent<PhysicsObjectBehaviour>().isSelected = true;
        }
        else if (currentlySelectedObject == null && lastSelectedObject != null)
        {
            lastSelectedObject.GetComponent<PhysicsObjectBehaviour>().isSelected = false;
            lastSelectedObject = null;
        }
  
    }

    private GameObject CastForPhysicsObject()
    {
        RaycastHit hitInfo;

        Physics.Raycast(transform.position, transform.forward, out hitInfo);
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (hitInfo.transform != null && hitInfo.collider.gameObject != null && hitInfo.collider.gameObject.tag == "PhysicsObject")
        {
            return hitInfo.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
	
}
