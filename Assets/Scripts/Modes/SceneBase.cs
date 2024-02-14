using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    public GameObject UI;
    public abstract void ReceiveInput();
    public abstract void CleanUp();
}