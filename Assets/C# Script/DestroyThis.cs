using UnityEngine;
using System.Collections;
public class DestroyThis : MonoBehaviour 
{ 
    public void ObjectDestroy() { Destroy(gameObject); } 
    void Update()
    {
        if (PlayerPrefs.GetInt("TriggerDestruction") == 1)
        {
            StartCoroutine(KillObject());
        }
    }

    IEnumerator KillObject()
    {
        yield return new WaitForSeconds(0.05f);
        PlayerPrefs.SetInt("TriggerDestruction", 0);
        PlayerPrefs.Save();
        Destroy(gameObject);
    }
}