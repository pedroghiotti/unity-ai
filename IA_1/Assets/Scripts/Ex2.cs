using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex2 : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject obj1, obj2;
    [SerializeField] private Transform pos1_1, pos1_2, pos2;

    private float lerpCt = 0;
    
    void Start()
    {
        obj1.transform.position = pos1_1.position;
        obj2.transform.position = pos1_2.position;
    }
    void Update()
    {
        // Interpolação da posição dos objeto entre seus pontos iniciais e um outro em comum.

        obj1.transform.position = Vector3.Lerp(pos1_1.position, pos2.position, lerpCt);
        obj2.transform.position = Vector3.Lerp(pos1_2.position, pos2.position, lerpCt);
        lerpCt += Time.deltaTime * speed;
    }

    void OnDrawGizmos()
    {
        // Representação visual dos pontos e trajetórias.

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pos1_1.position, .5f);
        Gizmos.DrawSphere(pos1_2.position, .5f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos2.position, .5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pos1_1.position, pos2.position);
        Gizmos.DrawLine(pos1_2.position, pos2.position);
    }
}
