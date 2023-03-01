using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class TeleRayActivator : MonoBehaviour
{
    public InputActionProperty R3Click;
    public GameObject TeleRayObject;
    // Update is called once per frame
    void Update()
    {
        TeleRayObject.SetActive(R3Click.action.ReadValue<float>() == 1);
    }
}
