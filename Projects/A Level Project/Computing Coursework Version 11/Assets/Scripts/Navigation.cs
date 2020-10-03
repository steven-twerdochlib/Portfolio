using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{

    public void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

}
