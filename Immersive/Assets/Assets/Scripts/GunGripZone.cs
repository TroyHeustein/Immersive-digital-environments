using UnityEngine;


public class GunGripZone : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor currentInteractor;

    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor =
            other.GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor>();

        if (interactor != null)
        {
            currentInteractor = interactor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor =
            other.GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor>();

        if (interactor != null && interactor == currentInteractor)
        {
            currentInteractor = null;
        }
    }

    public bool HasInteractor()
    {
        return currentInteractor != null;
    }

    public UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor GetInteractor()
    {
        return currentInteractor;
    }
}
