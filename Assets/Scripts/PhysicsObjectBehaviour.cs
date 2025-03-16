// PhysicsObjectBehaviour.cs
// February 2nd, 2020
// Updated by Matheus Vilano
// Assigned default values to uninitialized fields in order to remove Warnings

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObjectBehaviour : MonoBehaviour
{
    private Renderer physicsObjectRenderer = default;
    private Rigidbody physicsObjectRigidbody = default;
    [SerializeField]
    private string sObjectName = "";

    private Vector3 StartingPosition;

    public Color unselectedColour = default, selectedColour = default;

    private bool _isSelected = default;

    public bool isSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            if (value != _isSelected)
            {
                _isSelected = value;
                ChangeColourOnSelection(_isSelected);
            }
            
        }
    }

	// Use this for initialization
	void Start ()
    {
        physicsObjectRenderer = GetComponent<Renderer>();
        physicsObjectRigidbody = GetComponent<Rigidbody>();
        StartingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && _isSelected == true)
        {
            physicsObjectRigidbody.useGravity = !physicsObjectRigidbody.useGravity;
        }

        if (Input.GetMouseButtonDown(1) && _isSelected == true)
        {
            transform.position = StartingPosition;
        }
	}

    void OnCollisionEnter(Collision collision)
    {

        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * collision.relativeVelocity.magnitude, Color.red, 5f);
        }
        if (collision.relativeVelocity.magnitude > 2)
        {
            Debug.Log("BANG! at Magnitude of: " + collision.relativeVelocity.magnitude);
            AudioManager.instance.PlayPhysicsObjectImpact(sObjectName, collision.relativeVelocity.magnitude, this.gameObject);
        }
    }

    private void ChangeColourOnSelection(bool activateSelection)
    {
        if (activateSelection == true)
        {
            physicsObjectRenderer.material.color = selectedColour;
        }
        else if (activateSelection == false)
        {
            physicsObjectRenderer.material.color = unselectedColour;
        }
    }
}
