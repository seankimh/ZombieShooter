namespace Game.Enemies
{
    using UnityEngine;
    using UnityEngine.AI;

    public class Enemy : MonoBehaviour
    {
        [Header("AI Settings")]
        private NavMeshAgent agent;
        private Transform target;

        [Header("Health")]
        public float health = 100f;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            FPSController player = FindObjectOfType<FPSController>();
            if (player != null)
            {
                target = player.transform;
            }
            if (!agent.isOnNavMesh)
            {
                Debug.LogWarning(name + " is floating — not on NavMesh!");
            }

        }

        private void Update()
        {
            if (target != null)
            {
                if (agent.isOnNavMesh)
                {
                    agent.SetDestination(target.position);
                }
                else
                {
                    Debug.LogWarning(name + " lost the NavMesh and is drifting.");
                }
            }
            if (Time.frameCount % 60 == 0)
            {
                var col = GetComponent<Collider>();
                Debug.Log($"{name} position: {transform.position} | Collider bounds: {col.bounds.center}, {col.bounds.size}");
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Enemy collided with the player — GAME OVER");

                GameManager.Instance.GameOver();
            }
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
            Debug.Log("Enemy took damage: " + amount + " | Remaining health: " + health);

            if (health <= 0)
            {
                Die();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var col = GetComponent<Collider>();
            if (col != null)
            {
                Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
            }
        }


        void Die()
        {
            Debug.Log("Enemy died!");
            GameManager.Instance.AddScore(10);
            Destroy(gameObject);
        }
    }
}
