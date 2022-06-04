using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager flockManager;
    float speed;

    Vector3 groupCentre;

    private void Start()
    {
        // Atribui à variável ´speed´ um valor aleatório entre os valores mínimo e máximo determinados no FlockManager.
        speed = Random.Range(flockManager.minSpeed, flockManager.maxSpeed);
    }
    private void Update()
    {
        ApplyRules();

        // Move o objeto no eixo Z (Local) em determinada velocidade.
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    private void ApplyRules()
    {
        GameObject[] arrayGameObjects = flockManager.allFish;

        Vector3 posFlockCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;

        float groupSpeed = 0.01f;
        float distanceToOther;
        float groupSize = 0;

        foreach (GameObject fish in arrayGameObjects)
        {
            if (fish != this.gameObject)
            {
                distanceToOther = Vector3.Distance(fish.transform.position, this.transform.position);

                if (distanceToOther <= flockManager.neighbourDistance)
                {
                    posFlockCentre += fish.transform.position;
                    groupSize++;

                    if (distanceToOther < 1f)
                    {
                        vAvoid = vAvoid + (this.transform.position - fish.transform.position);
                    }

                    Flock flockOtherFish = fish.GetComponent<Flock>();
                    groupSpeed = groupSpeed + flockOtherFish.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            posFlockCentre = posFlockCentre / groupSize;
            speed = groupSpeed / groupSize;

            Vector3 direction = (posFlockCentre + vAvoid) - this.transform.position;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp
                (
                    transform.rotation,
                    Quaternion.LookRotation(direction),
                    flockManager.rotationSpeed * Time.deltaTime
                );
            }
        }

        groupCentre = posFlockCentre;
        print(Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groupCentre, .5f);
    }
}
