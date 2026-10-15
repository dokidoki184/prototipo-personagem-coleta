using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class personagem : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;

    public float velocidadeGiro = 720f;

    [Header("Pulo Gravidade")]
    public float alturaPulo = 2f;

    public float gravidade = -20f;
    private CharacterController controller;

    private float velocidadeY; 






    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

   
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        float v= Input.GetAxis("Vertical");

        Vector3 direcao = new Vector3(h, 0f, v);
        
        if (direcao.magnitude > 1f)
        {
            direcao.Normalize();
        }

        controller.Move(direcao * velocidade * Time.deltaTime);

        if (direcao != Vector3.zero)
        {
            Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.RotateTowards(
            transform.rotation, rotacaoAlvo, velocidadeGiro * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            
            if (velocidadeY < 0f)
            {
                velocidadeY = -2f;
            }

            if (Input.GetButtonDown("Jump"))
            {
                velocidadeY = Mathf.Sqrt(alturaPulo * -2f * gravidade);
            }
     
           velocidadeY += gravidade * Time.deltaTime;
            Vector3 movimento = direcao * velocidade;
            movimento.y = velocidadeY;
            controller.Move(new Vector3(0f, velocidadeY, 0f) * Time.deltaTime);

        }
        
    }
}

