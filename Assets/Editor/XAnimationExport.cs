using UnityEngine;
using System.Collections;
using UnityEditor;


public class XAnimationDetial : EditorWindow
{
	public AnimationClip animClip;
	public GameObject targetObj;

	public AnimationCurve xPosCurve;
	public AnimationCurve yPosCurve;
	public AnimationCurve zPosCurve;

	void OnGUI()
	{
		animClip = EditorGUILayout.ObjectField(animClip, typeof(AnimationClip), false) as AnimationClip;
		targetObj = EditorGUILayout.ObjectField(targetObj, typeof(GameObject), true) as GameObject;
		if (GUILayout.Button("Process"))
		{
			ProcessAnimClip();	
		}

		if (xPosCurve != null)
		{
			EditorGUILayout.CurveField(xPosCurve);
		}
		if (yPosCurve != null)
		{
			EditorGUILayout.CurveField(yPosCurve);
		}
		if (zPosCurve != null)
		{
			EditorGUILayout.CurveField(zPosCurve);
		}
	}

	void ProcessAnimClip()
	{
		EditorCurveBinding[] curveBindingArray = AnimationUtility.GetCurveBindings(animClip);
		XAnimMove animMove = targetObj.GetComponentInParent<XAnimMove>();
		animMove.animLength = animClip.length;
		foreach (var curveBinding in curveBindingArray)
		{
			Object animObj = AnimationUtility.GetAnimatedObject(targetObj, curveBinding);
			AnimationCurve curve = AnimationUtility.GetEditorCurve(animClip, curveBinding);
			if (animObj.name == "Bip001")
			{
				if (curveBinding.propertyName.Contains("m_LocalPosition.x"))
				{					
					animMove.xPosCurve = xPosCurve = curve;
				}
				else if (curveBinding.propertyName == "m_LocalPosition.y")
				{
					animMove.yPosCurve = yPosCurve = curve;
				}
				else if (curveBinding.propertyName == "m_LocalPosition.z")
				{
					animMove.zPosCurve = zPosCurve = curve;
				}
				Debug.LogFormat("animObjName:{0}, path:{1}, propertyName:{2}, keyFrameCount:{3}", 
				                animObj.name, 
				                curveBinding.path,
				                curveBinding.propertyName,
				                curve.keys.Length);
			}
		}
	}
}