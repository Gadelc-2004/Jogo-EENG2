using UnityEngine;

namespace TopDown.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [Header("Movement Stats")]
        [SerializeField] private float speed; 
        [SerializeField] private float lifetime;
        private Rigidbody2D body;
        private float lifeTimer;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }
    
        public void ShootBullet(Transform shootPoint)
        {
            lifeTimer = 0;
            body.linearVelocity = Vector2.zero;
            transform.position = shootPoint.position;
            transform.rotation = shootPoint.rotation;
            gameObject.SetActive(true);

            Vector2 rotatedDirection = Quaternion.Euler(0, 0, 88) * -transform.up;
            body.AddForce(rotatedDirection * speed, ForceMode2D.Impulse);
        }

        private void Update()
        {
            lifeTimer += Time.deltaTime;
            if(lifeTimer >= lifetime)
            { 
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
                Destroy(gameObject);
            }
        }

      }
}