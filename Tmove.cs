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
        // Obtener la posición del cursor en píxeles
        Vector3 mousePos = Input.mousePosition;

        // Convertir la posición del cursor a un rayo desde la cámara
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        // Obtener un plano que esté en la misma altura que la torreta
        Plane plane = new Plane(Vector3.up, transform.position);

        float hitDist;

        // Verificar si el rayo del cursor golpea el plano
        if (plane.Raycast(ray, out hitDist))
        {
            // Obtener el punto en el que el rayo intersecta con el plano
            Vector3 targetPoint = ray.GetPoint(hitDist);

            // Calcular la rotación necesaria para apuntar al punto del cursor
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Suavizar la rotación para que no sea brusca
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, velocidadRotacion * Time.deltaTime);

            /*transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, velocidadRotacion * Time.deltaTime);*/

            // Limitar la rotación por marco
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

