using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cameraObject;
    
    public void SceneMove(string anim)
    {
        cameraObject.GetComponent<Animator>().Play(anim);
    }
}
