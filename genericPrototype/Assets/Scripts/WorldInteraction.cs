using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteraction : MonoBehaviour {
	UnityEngine.AI.NavMeshAgent playerAgent;

	void Start()
	{
		playerAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}

	void Update ()
	{
		//Left mouse button.
		if (Input.GetMouseButton (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			GetInteraction ();
	}

	void GetInteraction()
	{
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo;
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity))
		{
			GameObject interactedObject = interactionInfo.collider.gameObject;
			if (interactedObject.tag == "Interactable Object")
			{
				Debug.Log ("Interactable Interacted");
			} else
			{
				playerAgent.destination = interactionInfo.point;	
			}

		}
}
}