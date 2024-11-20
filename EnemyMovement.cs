using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace Assets.Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        public float moveSpeed = 3f;
        public Transform[] waypoints;
        private int waypointIndex = 0;
        public GameObject explosionPrefab;
        private Bounds _cameraBounds;
        private SpriteRenderer _renderer;

        public float speed = 3f;
        private Vector3 targetPosition;

        private Rigidbody2D _rigidbody;

        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>(); // Certifique-se de ter o Rigidbody2D

            var height = Camera.main.orthographicSize * 2f;
            var width = height * Camera.main.aspect;
            _cameraBounds = new Bounds(Vector3.zero, new Vector3(width, height));

            SetNewTargetPosition();
        }

        void Update()
        {
            MoveTowardsTarget();
        }

        void SetNewTargetPosition()
        {
            var spriteWidth = _renderer.sprite.bounds.extents.x;
            var spriteHeight = _renderer.sprite.bounds.extents.y;

            var newPositon = Vector3.zero;
            newPositon.x = Mathf.Clamp(Random.Range(-5f, 5f), _cameraBounds.min.x + spriteWidth, _cameraBounds.max.x - spriteWidth);
            newPositon.y = Mathf.Clamp(Random.Range(-5f, 5f), _cameraBounds.min.y + spriteHeight, _cameraBounds.max.y - spriteHeight);

            targetPosition = newPositon;
        }

        void MoveTowardsTarget()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                SetNewTargetPosition();
            }
        }

        // Detecta colisões com outros inimigos
        public class PlayerController : MonoBehaviour
        {
            public GameObject gameOverPanel;  // Painel de fim de jogo (tela de game over)
            private Vector3 targetPosition;
            private float speed;
            private object scoreText;

            public object ScoreManager { get; private set; }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.gameObject.CompareTag("Enemy"))  // Se colidir com outro inimigo
                {
                    // Calcular a direção de afastamento
                    Vector3 awayFromCollision = (transform.position - collision.transform.position).normalized;
                    targetPosition = transform.position + awayFromCollision * Random.Range(2f, 4f); // Distância aleatória

                    speed = Random.Range(2f, 5f); // Opcional: alterar a velocidade para um valor aleatório após a colisão
                }

                if (collision.gameObject.CompareTag("PointObject"))  // Se colidir com o objeto que dá ponto
                {
                    ScoreManager.Instance.AddPoint();  // Supondo que você tenha um ScoreManager responsável pelo placar
                    Destroy(collision.gameObject);  // Opcional: destruir o objeto após a coleta
                }

                if (collision.gameObject.CompareTag("NegativePointObject"))  // Se colidir com um objeto que retira pontos
                {
                    // Remove um ponto do placar
                    ScoreManager.Instance.RemovePoint();
                    Destroy(collision.gameObject);  // Opcional: destruir o objeto após a colisão
                }

                if (collision.gameObject.CompareTag("Polvo"))  // Se colidir com o objeto chamado "Polvo"
                {
                    // Pausar o jogo
                    Time.timeScale = 0f;  // Pausa o jogo

                    // Mostrar a tela de fim de jogo
                    GameOver();  // Chama a função GameOver
                }
            }

            // Função GameOver
            public void GameOver()
            {
                // Exibir o painel de fim de jogo
                gameOverPanel.SetActive(true);

                // Exibir a pontuação final
                scoreText.text = "Pontuação Final: " + ScoreManager.Instance.GetScore();
            }
        }


        public void Explode()
        {
            // Instanciar a explosão
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Destroi o inimigo
        }
    }
}
