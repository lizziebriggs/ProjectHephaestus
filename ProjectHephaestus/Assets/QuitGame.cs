using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void doquit()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

}
