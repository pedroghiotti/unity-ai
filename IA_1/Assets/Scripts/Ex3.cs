using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex3 : MonoBehaviour
{
    [SerializeField] private float speed1, speed2;

    [SerializeField] private GameObject obj1, obj2, obj3;
    [SerializeField] private Transform pos1_1, pos1_2, pos1_3;
    
    void Start()
    {
        obj1.transform.position = pos1_1.position;
        obj2.transform.position = pos1_2.position;
        obj3.transform.position = pos1_3.position;

        StartCoroutine(MoveToRandom(obj3));
    }
    void Update()
    {
        MoveToward(obj1, obj3);
        MoveToward(obj2, obj3);
    }

    void OnDrawGizmos()
    {
        // Representação visual da trajetória dos objetos 1 e 2 até o objeto 3

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(obj1.transform.position, obj3.transform.position);
        Gizmos.DrawLine(obj2.transform.position, obj3.transform.position);
    }

    private void MoveToward(GameObject obj, GameObject target)
    {
        // Move dado objeto em direção à outro.

        Vector3 dir = (target.transform.position - obj.transform.position).normalized;
        obj.transform.Translate(dir * Time.deltaTime * speed1);
    }

    private IEnumerator MoveToRandom(GameObject obj)
    {
        // Movimenta um dado objeto para posições aleatórias dentro de um alcance limitado.
        // Interpola Posição atual -> Posição aleatória dentro de um alcance.

        Vector3 startPos = obj.transform.position;
        Vector3 destination = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        for(float n = 0; n < 1; n += Time.deltaTime * speed2)
        {
            obj.transform.position = Vector3.Lerp(startPos, destination, n);
            yield return null;
        }
        
        StartCoroutine(MoveToRandom(obj)); //Inicia o movimento novamente.
    }
}
