using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    /* 
        Lista (array, no caso) de waypoints que o objeto irá seguir e
        uma variável que indica o waypoint para o qual o objeto está indo.
    */
    public GameObject[] waypoints;
    int currentWP = 0;

    /*
        Configurações relativas ao movimento.
        Deixei 'speed' e 'rotSpeed' expostas no Inspector pra facilitar testes.
    */
    [SerializeField] float speed = 1.0f;
    float accuracy = 1.0f;
    [SerializeField] float rotSpeed = 0.4f;

    void Start()
    {
        /*
            'GameObject.FindGameObjectsWithTag' busca todos os waypoints (marcados pela tag "waypoint")
            na cena e retorna esses objetos num array.
            Atribúi esse array à 'waypoints'.
        */
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");                
    }
    void LateUpdate()
    {
        /*
            Previne que o Update rode caso não haja waypoints no array e, portanto,
            previne que o código tente acessar itens inexistentes dentro desse array.
            Eu usaria if(waypoints.Length != 0) e colocaria todo o resto do Update dentro disso.
            Assim fica mais elegante, vou adotar essa estruturinha.
        */
        if (waypoints.Length == 0) return;

        /*
            Variável local que guarda a posição do waypoint que o objeto está tentando alcançar.
            Ignora a posição Y do waypoint, restringe movimento aos eixos X e Z apenas.
        */
        Vector3 lookAtGoal = new Vector3
        (
            waypoints[currentWP].transform.position.x, 
            this.transform.position.y, 
            waypoints[currentWP].transform.position.z
        );

        /*
            Cálculo da direção desse objeto pra o waypoint alvo e
            interpolação da rotação atual para a rotação alvo (dada por 'Quaternion.LookRotation').
        */
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp
        (
            this.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotSpeed
        );

        /*
            Verifica se o objeto chegou ao alvo
            (distância até o alvo é menor que 'accuracy' (funciona como uma tolerância)).
            Se chegou ao alvo atual, o próximo waypoint na lista
            (ou o primeiro, se não houver próximo) passa a ser o alvo.
        */
        if(direction.magnitude < accuracy)
        {
            // Incrementa a variável que indica o waypoint alvo atual.
            currentWP++;

            // Caso 'currentWP' ultrapassar o último item da lista, volta ao primeiro.
            if(currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }

        //Move o objeto em seu eixo local Z em dada velocidade.
        this.transform.Translate
        (
            0, 
            0, 
            speed * Time.deltaTime,
            Space.Self
        );
    }
}
