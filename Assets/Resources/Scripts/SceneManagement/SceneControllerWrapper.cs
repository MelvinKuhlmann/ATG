using UnityEngine;

public class SceneControllerWrapper : MonoBehaviour
{
    public void RestartZone (bool resetHealth)
    {
        SceneController.RestartZone (resetHealth);
    }

    public void TransitionToScene (TransitionPoint transitionPoint)
    {
        SceneController.TransitionToScene (transitionPoint);
    }
}