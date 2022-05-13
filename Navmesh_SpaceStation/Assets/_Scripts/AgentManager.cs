using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public List<AiControl> lstAgents = new List<AiControl>();

    private void Start()
    {
        /*
            Encontra os objetos na cena com o script 'AiControl'.
        */
        foreach(AiControl aiControl in FindObjectsOfType<AiControl>())
        {
            lstAgents.Add(aiControl);
        }
    }
    private void Update()
    {
        /*
            Espera o Input do jogador (botão esquerdo do mouse),
            realiza um Raycast a partir da câmera,
            define o ponto alvo da movimentação dos navMeshAgents.
        */
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            Physics.Raycast(ray, out hitInfo);

            foreach(AiControl agent in lstAgents)
            {
                agent.navMeshAgent.SetDestination(hitInfo.point);
            }
        }   
    }
}
