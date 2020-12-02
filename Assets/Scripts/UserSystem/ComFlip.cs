using UnityEngine;
using Cinemachine;
public class ComFlip : MonoBehaviour
{
	//xを反転させる
    [SerializeField] bool inputFlip = true;
	CinemachineFreeLook freeLook;
	void Start()
	{
		CinemachineCore.GetInputAxis = GetAxisCustom;

	}

	public float GetAxisCustom(string axisName)
	{
		if (axisName == "Mouse X")
		{
			return Input.GetAxis(axisName) * (inputFlip ? -1f : 1f);
		}

		return 0;
	}
}
