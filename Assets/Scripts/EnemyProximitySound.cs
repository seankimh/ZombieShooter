using UnityEngine;

namespace Game.Enemies
{
    public class EnemyProximitySound : MonoBehaviour
    {
        public float triggerDistance = 8f;
        public float repeatDelay = 3f;
        private Transform player;
        private AudioSource audioSource;
        private float nextPlayTime = 0f;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (player == null || audioSource == null) return;

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= triggerDistance && Time.time >= nextPlayTime)
            {
                audioSource.Play();
                nextPlayTime = Time.time + repeatDelay;
            }
        }
    }
}
