using UnityEngine;
using System.Collections;

public class XAnimMove : MonoBehaviour 
{
	public float ratio = 1f;
	public Animator animator;

	public float animLength;
	public AnimationCurve xPosCurve;
	public AnimationCurve yPosCurve;
	public AnimationCurve zPosCurve;

	float lastEvaluateTime = 0f;

	float _startTime;

	float lastPos = 0;

	public void CallStart()
	{
		_startTime = Time.time;
		lastPos = 0;
	}

	public void CallUpdate () 
	{
		float time = Time.time;
		while (time > animLength)
		{
			time -= animLength;
		}

		Vector3 pos = transform.position;
		float curPos = zPosCurve.Evaluate(time);
		pos.z += curPos - lastPos;
		transform.position = pos;
		lastPos = curPos;

//		float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
//
//		while (normalizedTime > 1)
//		{			
//			normalizedTime -= 1;
//		}
//
//		float evaluateTime = normalizedTime * animLength;
//
//		if (evaluateTime - lastEvaluateTime > 0)
//		{
//			Vector3 pos = transform.position;
//			pos.z += (zPosCurve.Evaluate(evaluateTime) - zPosCurve.Evaluate(lastEvaluateTime)) * ratio;
//			transform.position = pos;
//
//			Debug.LogWarning((zPosCurve.Evaluate(evaluateTime) - zPosCurve.Evaluate(lastEvaluateTime)) * ratio);
//
//			lastEvaluateTime = evaluateTime;
//		}
//		else
//		{
//			lastEvaluateTime = 0;
//		}
	}
}
