using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevPreload : MonoBehaviour
{
	void Awake()
	{
		GameObject check = GameObject.Find("_app");
		if (check == null)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("_preload", LoadSceneMode.Additive);
		}
	}
}
