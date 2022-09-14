using System.Collections;
using System.Collections.Generic;
using TofAr.V0;
using TofAr.V0.Tof;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DepthSceneManager : MonoBehaviour
{
    public void BackButton()
    {
        TofArTofManager.Instance.StopStream();

        Destroy(TofArManager.Instance.gameObject);
        Destroy(TofArManager.Instance.gameObject);

        SceneManager.LoadSceneAsync("Main");
    }
}
