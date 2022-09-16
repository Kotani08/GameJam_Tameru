using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    AudioClip TapSE;

    public void Change()
    {
        soundManager.PlayBgm(TapSE);
        Invoke("ChangeEnd",0.01f);
    }
    private void ChangeEnd()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
