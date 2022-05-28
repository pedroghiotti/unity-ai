using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager flockManager;
    float speed;

    private void Start()
    {
        // Atribui à variável ´speed´ um valor aleatório entre os valores mínimo e máximo determinados no FlockManager.
        speed = Random.Range(flockManager.minSpeed, flockManager.maxSpeed);
    }
    private void Update()
    {
        // Move o objeto no eixo Z em determinada velocidade.
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
