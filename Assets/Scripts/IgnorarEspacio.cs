using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Durante las pruebas habiendo tanto joystick como bot�n, el juego hace algo extra�o

// Si intentas saltar con la barra espaciadora, todo fufa, pero si atacas utilizando el
// bot�n, el juego pasa a asignar a la barra espaciadora el �ltimo input de la UI que
// hayas utilizado (en este caso el bot�n de ataque), con lo que al saltar con la barra
// espaciadora tambi�n se acciona el bot�n de ataque

// Si tras usar el bot�n de ataque tocas el joystick y saltas con el espacio, va bien otra vez
// Pero en cuanto usas otra vez el bot�n, el juego pasa a asign�rselo al espacio

// Este script es para evitar eso; no se consigue del todo, siempre pasa que tras usar una vez
// el bot�n de ataque la siguiente vez que saltas con el espacio tambi�n ataca por asignarle
// el espacio al bot�n, pero el resto de veces lo ignora correctamente
// No he conseguido una forma mejor de hacerlo

public class IgnorarEspacio : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        // Obtener el componente Button del objeto
        button = GetComponent<Button>();
    }

    private void Update()
    {
        // Verificar si se presiona la tecla espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Deseleccionar cualquier bot�n activo
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
