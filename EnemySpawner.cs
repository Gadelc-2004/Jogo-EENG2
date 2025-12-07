using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints; // Array com os 4 pontos de spawn
    
    [Header("Enemy Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemies = 10; // Máximo de inimigos na cena
    
    [Header("Spawn Timing")]
    [SerializeField] private float initialSpawnDelay = 2f; // Tempo antes do primeiro spawn
    [SerializeField] private float spawnInterval = 3f; // Intervalo entre spawns
    
    private List<GameObject> activeEnemies = new List<GameObject>();
    
    private void Start()
    {
        // Garantir que temos exatamente 4 spawn points
        if (spawnPoints.Length != 4)
        {
            Debug.LogWarning($"Você precisa configurar exatamente 4 spawn points. Atualmente: {spawnPoints.Length}");
        }
        
        // Iniciar spawning
        StartCoroutine(SpawnEnemiesRoutine());
    }
    
    private IEnumerator SpawnEnemiesRoutine()
    {
        // Aguarda o delay inicial
        yield return new WaitForSeconds(initialSpawnDelay);
        
        // Loop infinito de spawning
        while (true)
        {
            // Só spawna se não atingiu o limite máximo
            if (activeEnemies.Count < maxEnemies)
            {
                SpawnEnemyAtRandomPoint();
            }
            
            // Aguarda o intervalo antes do próximo spawn
            yield return new WaitForSeconds(spawnInterval);
            
            // Limpa inimigos destruídos da lista
            CleanEnemyList();
        }
    }
    
    private void SpawnEnemyAtRandomPoint()
    {
        // Escolhe um ponto de spawn aleatório (0 a 3)
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        
        // Instancia o inimigo
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        
        // Adiciona à lista de inimigos ativos
        activeEnemies.Add(newEnemy);
        
        Debug.Log($"Inimigo spawnado no ponto {randomIndex + 1} - Posição: {spawnPoint.position}");
    }
    
    private void CleanEnemyList()
    {
        // Remove inimigos nulos (que foram destruídos) da lista
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
            }
        }
    }
    
    // Método opcional para spawnar em um ponto específico
    public void SpawnEnemyAtPoint(int pointIndex)
    {
        if (pointIndex >= 0 && pointIndex < spawnPoints.Length && activeEnemies.Count < maxEnemies)
        {
            GameObject newEnemy = Instantiate(
                enemyPrefab, 
                spawnPoints[pointIndex].position, 
                Quaternion.identity
            );
            activeEnemies.Add(newEnemy);
        }
    }
    
    // Método para debug: ver spawn points na cena
    private void OnDrawGizmos()
    {
        if (spawnPoints != null)
        {
            Gizmos.color = Color.red;
            foreach (Transform point in spawnPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawSphere(point.position, 0.5f);
                    Gizmos.DrawWireCube(point.position, Vector3.one);
                }
            }
        }
    }
}