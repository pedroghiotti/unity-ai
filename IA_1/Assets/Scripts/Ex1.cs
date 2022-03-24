using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex1 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform pos1, pos2;

    private float lerpCt = 0; //Variável usada pra controlar a interpolação das posições.
    
    void Start()
    {
        this.transform.position = pos1.position;
    }
    void Update()
    {
        // Interpolação da posição do objeto entre dois pontos.

        this.transform.position = Vector3.Lerp(pos1.position, pos2.position, lerpCt);
        lerpCt += Time.deltaTime * speed;
    }

    void OnDrawGizmos()
    {
        // Representação visual dos pontos e trajetória.

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pos1.position, .5f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos2.position, .5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
