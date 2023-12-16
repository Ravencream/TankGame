using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tmove : MonoBehaviour
{


    public float velocidadRotacion = 5f;

    private void Start()
    {
        {
            CursorLoqueado();
           
        }
    }

    void FixedUpdate()
    {
        // Obtener la posici�n del cursor en p�xeles
        Vector3 mousePos = Input.mousePosition;

        // Convertir la posici�n del cursor a un rayo desde la c�mara
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        // Obtener un plano que est� en la misma altura que la torreta
        Plane plane = new Plane(Vector3.up, transform.position);

        float hitDist;

        // Verificar si el rayo del cursor golpea el plano
        if (plane.Raycast(ray, out hitDist))
        {
            // Obtener el punto en el que el rayo intersecta con el plano
            Vector3 targetPoint = ray.GetPoint(hitDist);

            // Calcular la rotaci�n necesaria para apuntar al punto del cursor
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Suavizar la rotaci�n para que no sea brusca
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, velocidadRotacion * Time.deltaTime);

            /*transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, velocidadRotacion * Time.deltaTime);*/

            // Limitar la rotaci�n por marco
            float maxDeltaRotation = velocidadRotacion * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDeltaRotation);

        }

     }
        void CursorLoqueado()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
   
}

