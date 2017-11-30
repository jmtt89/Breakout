using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	void Start(){

		iTween.PunchScale(gameObject, iTween.Hash("x", .15, "easeType", "easeInOutElastic", "loopType", "loop", "delay", .5));
	}
}

