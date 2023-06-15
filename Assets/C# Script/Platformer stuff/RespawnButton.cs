using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnButton : MonoBehaviour
{
    public void ButtonClicked()
    {
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }
}
