using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void Switch(int sceneBuildIndex, float switchWaitTime)
    {
        StartCoroutine(LetsDoThis(sceneBuildIndex, switchWaitTime));
    }

    IEnumerator LetsDoThis(int buildIndex, float switchWaitTime)
    {
        yield return new WaitForSeconds(switchWaitTime);
        SceneManager.LoadScene(sceneBuildIndex: buildIndex);
    }
}