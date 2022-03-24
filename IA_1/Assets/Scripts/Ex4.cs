using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ex4 : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject obj;
    [SerializeField] private Transform[] arrPoints;
    [SerializeField] private int currentPoint = 0;
    private bool movementFinished = false;
    
    void Start()
    {
        obj.transform.position = arrPoints[0].position;
        StartCoroutine(MoveToNextPoint());
    }
    void OnDrawGizmos()
    {
        // Representação visual dos pontos
        // São coloridos de vermelho até que o objeto os ultrapasse. Serão, então, verdes.
        for(int i = 0; i < arrPoints.Count(); i++)
        {
            if(i <= currentPoint)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(arrPoints[i].position, .5f);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(arrPoints[i].position, .5f);
            }
        }
    }

    private IEnumerator MoveToNextPoint()
    {
        // Interpola posição do objeto entre o ponto atual e o próximo.

        for(float lerpCt = 0; lerpCt < 1; lerpCt += Time.deltaTime * speed)
        {
            if(currentPoint == 4) // Ser estiver no ponto final, volta até o penúltimo e finaliza o movimento.
            {
                movementFinished = true;
                
                obj.transform.position = Vector3.Lerp(arrPoints[currentPoint].position, arrPoints[currentPoint - 1].position, lerpCt);
                yield return null;
            }
            else
            {
                obj.transform.position = Vector3.Lerp(arrPoints[currentPoint].position, arrPoints[currentPoint + 1].position, lerpCt);
                yield return null;
            }
        }

        // Segue repetindo essa coroutine até que o movimento esteja finalizado.

        if(movementFinished == false)
        {
            currentPoint++;
            StartCoroutine(MoveToNextPoint());
        }
        else
        {
            currentPoint--;
        }
    }
}
