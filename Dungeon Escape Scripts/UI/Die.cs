using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    private float _delay = 3f;

    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuAfterDelay());
    }

    private IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene("MainMenu");
    }
}
