using UnityEngine;
using TopDown.Audio;

namespace TopDown.Shooting
{
    public class GunController : MonoBehaviour
    {
        [Header("Cooldown")]
        [SerializeField] private float cooldown = 0.25f;
        private float cooldownTimer;

        [Header("References")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firepoint;
        [SerializeField] private Animator MuzzleFlashAnimator;

        [Header("Sound Effects")]
        [SerializeField] private AudioClip shotSound;


        private void Update()
        {
            cooldownTimer += Time.deltaTime;
        }

        private void Shoot()
        {
            if(cooldownTimer < cooldown)
            {
                return;
            }

            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation, null);
            bullet.GetComponent<Projectile>().ShootBullet(firepoint);

            MuzzleFlashAnimator.SetTrigger("Shoot");
            cooldownTimer = 0;
            SoundManager.Instance?.PlaySound(shotSound);
        }

        private void OnShoot()
        {
            Shoot();
        }
    }
}