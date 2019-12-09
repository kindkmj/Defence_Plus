using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneManager : SingletonMonobehaviour<SceneManager>
{
    #region Singleton
    //public static SceneManager Instance { get; private set; }

    protected override void OnAwake()
    {
        //if (Instance == null)
        //    Instance = this;
        //else
        //{
        //    Destroy(this);
        //}

        DontDestroyOnLoad(gameObject);
    } 
    #endregion
    
    public List<string> Scenes;
	public List<bool> MoveScene = new List<bool>() {false, false, false};
	
	void Update () 
	{
		//for(int i = 0; i < MoveScene.Count; i++)
		//{
		//	if(MoveScene[i])
		//	{
		//		UnityEngine.SceneManagement.SceneManager.LoadScene(Scenes[i]);
		//		MoveScene[i] = false;
		//	}
		//}
	}
}
