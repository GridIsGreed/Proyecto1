## Documentación Técnica: Juego de Damas (Consola C#)

Este proyecto es una implementación en C# del juego clásico de damas tradicionales, diseñado para ejecutarse en la terminal o línea de comandos. 
El sistema gestiona los turnos, valida la legalidad de los movimientos, maneja la captura de piezas, la coronación de reinas y determina el final de la partida.

### 1. Requisitos de Ejecución

* **Entorno:** Cualquier IDE que soporte C# (Visual Studio, Visual Studio Code) o el compilador de la línea de comandos de .NET SDK.
* **Lenguaje:** C# (Compatible con versiones de .NET Core 3.1 en adelante).
* **Interfaz:** Consola del sistema operativo.

### 2. Estructura del Código

El programa se divide en tres componentes principales dentro del espacio de nombres `DamasChinas`:

#### `enum Pieza`

Define los estados posibles de cualquier casilla dentro del tablero.

* `Vacia` (0)
* `PeonBlanco` (1)
* `PeonNegro` (2)
* `ReinaBlanca` (3)
* `ReinaNegra` (4)

#### `class Tablero`

Es el núcleo lógico del juego. Mantiene el estado de la partida a través de una matriz bidimensional `casillas` de $8 \times 8$.

| Método | Descripción de su función |
| --- | --- |
| `Tablero()` | Constructor. Inicializa la matriz, la limpia y coloca las 24 fichas iniciales llamando a métodos privados auxiliares. |
| `LimpiarTablero()` | Asigna el valor `Pieza.Vacia` a las 64 coordenadas de la matriz. |
| `InicializarFichas()` | Coloca 12 peones negros en las 3 filas superiores y 12 peones blancos en las 3 inferiores, estrictamente en las casillas oscuras. |
| `MostrarTablero()` | Limpia la consola (`Console.Clear()`) y renderiza el estado actual de la matriz utilizando caracteres de texto (`B`, `N`, `RB`, `RN`, `.`). |
| `ContarPiezas(out int, out int)` | Recorre la matriz y devuelve la cantidad de fichas activas (peones y reinas) para las Blancas y las Negras utilizando parámetros de salida. |
| `MoverPieza(...)` | Recibe las coordenadas de origen y destino. Valida reglas geométricas, restricciones de turno y sentido, ejecuta capturas, corona peones y actualiza la matriz. Retorna `true` si el movimiento fue legal. |

#### `class Program`

Gestiona el bucle de juego principal (Game Loop) y la interacción directa con el jugador.

| Método | Descripción de su función |
| --- | --- |
| `Main()` | Alterna los turnos lógicos, muestra el marcador, invoca a `MoverPieza` e interrumpe el ciclo cuando el método `ContarPiezas` reporta que un jugador llegó a 0 fichas. |
| `PedirCoordenada(string)` | Captura la entrada del teclado, valida que sea un número entero y asegura que esté dentro del rango legal del tablero (0 al 7). |

### 3. Guía de Uso y Sistema de Coordenadas

Al ejecutar el programa, el jugador verá un tablero de $8 \times 8$. El sistema de coordenadas se basa en **índices basados en cero**, igual que los arreglos en C#.

* **Fila 0:** Borde superior (donde inician las Negras).
* **Fila 7:** Borde inferior (donde inician las Blancas).
* **Columnas:** De izquierda (0) a derecha (7).

**Cómo ingresar un movimiento:**
El sistema pedirá cuatro datos secuenciales por turno:

1. **Fila origen:** Coordenada vertical de la pieza que deseas mover.
2. **Columna origen:** Coordenada horizontal de la pieza.
3. **Fila destino:** Coordenada vertical a donde deseas llegar.
4. **Columna destino:** Coordenada horizontal a donde deseas llegar.

Si el usuario intenta realizar un movimiento ilegal (como avanzar un peón hacia atrás, saltar a una casilla ocupada o mover una pieza que no le pertenece), 
la consola arrojará un mensaje de error específico y exigirá presionar `Enter` para reiniciar el turno sin pasar el control al rival.
