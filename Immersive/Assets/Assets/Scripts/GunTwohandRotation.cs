using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GunTwohandRotation : MonoBehaviour
{
    [Header("Grip Zones")]
    public GunGripZone foregripZone;
    public GunGripZone rearGripZone;

    [Header("Object To Rotate")]
    public Transform objectToRotate;

    [Header("Rotation")]
    public float targetXRotation = 50f;
    public float rotationSpeed = 8f;

    [Header("Trigger")]
    [Range(0f, 1f)]
    public float triggerThreshold = 0.1f;

    private void Update()
    {
        bool bothHandsPresent =
            foregripZone != null &&
            rearGripZone != null &&
            foregripZone.HasInteractor() &&
            rearGripZone.HasInteractor();

        bool triggerHeld = false;

        if (bothHandsPresent)
        {
            triggerHeld = GetRearGripTrigger();
        }

        Quaternion targetRotation = triggerHeld
            ? Quaternion.Euler(targetXRotation, 0f, 0f)
            : Quaternion.identity;

        objectToRotate.localRotation = Quaternion.Lerp(
            objectToRotate.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private bool GetRearGripTrigger()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor rearInteractor =
            rearGripZone.GetInteractor();

        if (rearInteractor == null)
            return false;

        Component component = rearInteractor as Component;

        if (component == null)
            return false;

        Transform interactorTransform = component.transform;

        InputDevice device = FindInputDevice(interactorTransform);

        if (!device.isValid)
            return false;

        if (device.TryGetFeatureValue(
            CommonUsages.trigger,
            out float triggerValue))
        {
            return triggerValue >= triggerThreshold;
        }

        return false;
    }

    private InputDevice FindInputDevice(Transform interactorTransform)
    {
        Transform current = interactorTransform;

        while (current != null)
        {
            XRController controller =
                current.GetComponent<XRController>();

            if (controller != null)
            {
                return controller.inputDevice;
            }

            current = current.parent;
        }

        return default;
    }
}
