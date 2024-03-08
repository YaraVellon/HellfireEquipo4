using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Durante las pruebas habiendo tanto joystick como botón, el juego hace algo extraño

// Si intentas saltar con la barra espaciadora, todo fufa, pero si atacas utilizando el
// botón, el juego pasa a asignar a la barra espaciadora el último input de la UI que
// hayas utilizado (en este caso el botón de ataque), con lo que al saltar con la barra
// espaciadora también se acciona el botón de ataque

// Si tras usar el botón de ataque tocas el joystick y saltas con el espacio, va bien otra vez
// Pero en cuanto usas otra vez el botón, el juego pasa a asignárselo al espacio

// Este script es para evitar eso; no se consigue del todo, siempre pasa que tras usar una vez
// el botón de ataque la siguiente vez que saltas con el espacio también ataca por asignarle
// el espacio al botón, pero el resto de veces lo ignora correctamente
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
            // Deseleccionar cualquier botón activo
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
