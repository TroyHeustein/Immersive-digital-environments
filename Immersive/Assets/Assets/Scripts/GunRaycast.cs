using UnityEngine;

public class GunRaycast : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject bulletHolePrefab;
    public float maxDistance = 100f;

    public AudioSource audioSource;

    public void Shoot()
    {
        Debug.Log("GUN SHOOT FUNCTION CALLED!");

        // Play test sound
        if (audioSource != null)
        {
            audioSource.Play();
        }

        Ray ray = new Ray(muzzlePoint.position, muzzlePoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Debug.Log("RAYCAST HIT: " + hit.collider.gameObject.name);

            GameObject bulletHole = Instantiate(
                bulletHolePrefab,
                hit.point,
                Quaternion.LookRotation(hit.normal)
            );

            bulletHole.transform.position += hit.normal * 0.01f;
        }
        else
        {
            Debug.Log("RAYCAST MISSED");
        }
    }
}