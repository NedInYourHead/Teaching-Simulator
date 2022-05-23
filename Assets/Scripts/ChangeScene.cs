using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown("return"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}
