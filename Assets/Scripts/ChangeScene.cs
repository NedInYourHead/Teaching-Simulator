using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
	[SerializeField] private int nextScene = 0;
	[SerializeField] private string key = "return";

	void Update()
	{
		if (Input.GetKeyDown(key))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
		}
	}
}
