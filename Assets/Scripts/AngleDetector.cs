using System.Collections;
//using System.Collections.Generics;
using UnityEngine;
using UnityEngine.Events;

public class AngleDetector : MonoBehaviour
{
	[SerializeField]
	UnityEvent isUpEvent;
	[SerializeField]
	UnityEvent isDownEvent;
	[SerializeField]
	float maxAngle = 30.0f;

	bool wasUp = true;

	private void Update()
	{
		bool isUp = Vector3.Angle(Vector3.up, transform.up) < maxAngle;
		if (isUp && !wasUp) isUpEvent.Invoke();
		if (!isUp && wasUp) isDownEvent.Invoke();
		wasUp = isUp;
	}
}