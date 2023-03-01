using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ActivateGrabRay : MonoBehaviour
{
    [SerializeField] private GameObject LeftGrabRay;
    [SerializeField] private GameObject RightGrabRay;

    [SerializeField] private XRDirectInteractor LeftDirectGrab;  //On Left Hand Object
    [SerializeField] private XRDirectInteractor RightDirectGrab; //On Right Hand Object

    // Update is called once per frame
    void Update()
    {
        LeftGrabRay.SetActive(LeftDirectGrab.interactablesSelected.Count == 0);
        RightGrabRay.SetActive(RightDirectGrab.interactablesSelected.Count == 0);
    }
}
