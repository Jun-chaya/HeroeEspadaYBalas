using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            
            MovimientoTopDown.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            //SeguirJugador.Instance.player = GameObject.FindWithTag("Player").transform;
            UIFade.Instance.FadeToClear();

        }
    }
}
