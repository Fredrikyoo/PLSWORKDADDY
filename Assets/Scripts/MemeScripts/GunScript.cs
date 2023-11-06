using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform firePoint;  // Punktet der kulen vil bli skutt fra.
    public Transform casePoint;  // Punktet der kulen vil bli skutt fra.
    public GameObject bulletPrefab;  // Prefaben for kulen.
    public GameObject casePrefab;
    public float bulletSpeed = 60f;  // Hastigheten til kulen.
    public float caseSpeed = 5f;  // Hastigheten til kulen.

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  // Venstre musetast.
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject caseBody = Instantiate(casePrefab, casePoint.position, casePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Rigidbody rbcase = caseBody.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
            rbcase.velocity = firePoint.right * caseSpeed;
        }
    }
}
