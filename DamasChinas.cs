using System;

namespace DamasChinas
{
    // Definimos los tipos de casillas y piezas usando un Enum
    public enum Pieza
    {
        Vacia = 0,
        PeonBlanco = 1,
        PeonNegro = 2,
        ReinaBlanca = 3,
        ReinaNegra = 4
    }

    class Tablero
    {
            // Creamos una matriz (arreglo bidimensional) de 8x8
            private Pieza[,] casillas;

            public Tablero()
            {
                // Inicializamos el tamaño de la matriz
                casillas = new Pieza[8, 8];
                LimpiarTablero();
                // Llamamos a la colocación inicial
                InicializarFichas();
            }

            private void LimpiarTablero()
            {
                // Recorremos filas y columnas para asegurar que todo esté vacío
                for (int fila = 0; fila < 8; fila++)
                {
                    for (int col = 0; col < 8; col++)
                    {
                        casillas[fila, col] = Pieza.Vacia;
                    }
                }
            }

        // Colocar las 12 piezas de cada jugador en las posiciones iniciales
        private void InicializarFichas()
        {
            // Colocamos las piezas negras en las primeras 3 filas
            for (int fila = 0; fila < 3; fila++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((fila + col) % 2 == 1) // Solo en casillas negras
                    {
                        casillas[fila, col] = Pieza.PeonNegro;
                    }
                }
            }

            // Colocamos las piezas blancas en las últimas 3 filas
            for (int fila = 5; fila < 8; fila++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((fila + col) % 2 == 1) // Solo en casillas negras
                    {
                        casillas[fila, col] = Pieza.PeonBlanco;
                    }
                }
            }
        }

        // Método para mostrar el tablero en consola
        public void MostrarTablero()
        {
            Console.Clear();
            for (int fila = 0; fila < 8; fila++)
            {
                for (int col = 0; col < 8; col++)
                {
                    switch (casillas[fila, col])
                        {
                            case Pieza.Vacia:
                                // Usamos '.' para casillas oscuras y ' ' para casillas claras
                                Console.Write((fila + col) % 2 == 1 ? ". " : "  ");
                                break;
                            case Pieza.PeonBlanco:
                                Console.Write("B ");
                                break;
                            case Pieza.PeonNegro:
                                Console.Write("N ");
                                break;
                            case Pieza.ReinaBlanca:
                                Console.Write("RB");
                                break;
                            case Pieza.ReinaNegra:
                                Console.Write("RN");
                                break;
                        }
                }
                Console.WriteLine();
            }
        }

        // Valida a cuál jugador le pertenece la ficha
        public bool MoverPieza(int fOrigen, int cOrigen, int fDestino, int cDestino, bool turnoBlanco)
        {
            Pieza piezaOrigen = casillas[fOrigen, cOrigen];
            Pieza piezaDestino = casillas[fDestino, cDestino];

            if (piezaOrigen == Pieza.Vacia)
            {
                Console.WriteLine("\n[Error] No hay ninguna ficha en la casilla seleccionada.");
                return false;
            }

            // Validar propiedad de la pieza según el turno
            if (turnoBlanco && (piezaOrigen != Pieza.PeonBlanco && piezaOrigen != Pieza.ReinaBlanca))
            {
                Console.WriteLine("\n[Error] Es el turno de las Blancas. Debes elegir una ficha blanca.");
                return false;
            }

            if (!turnoBlanco && (piezaOrigen != Pieza.PeonNegro && piezaOrigen != Pieza.ReinaNegra))
            {
                Console.WriteLine("\n[Error] Es el turno de las Negras. Debes elegir una ficha negra.");
                return false;
            }

            if (piezaDestino != Pieza.Vacia)
            {
                Console.WriteLine("\n[Error] La casilla de destino está ocupada.");
                return false;
            }

            int difFila = fDestino - fOrigen;
            int difCol = Math.Abs(cDestino - cOrigen);

            // Validar desplazamiento horizontal diagonal
            if (difCol != 1 && difCol != 2)
            {
                Console.WriteLine("\n[Error] Movimiento diagonal inválido (solo puedes mover 1 casilla o saltar 2).");
                return false;
            }

            // CASO 1: Movimiento simple de 1 casilla
            if (difCol == 1)
            {
                if (piezaOrigen == Pieza.PeonNegro && difFila != 1)
                {
                    Console.WriteLine("\n[Error] Los peones negros solo pueden avanzar hacia abajo (+1 fila).");
                    return false;
                }

                if (piezaOrigen == Pieza.PeonBlanco && difFila != -1)
                {
                    Console.WriteLine("\n[Error] Los peones blancos solo pueden avanzar hacia arriba (-1 fila).");
                    return false;
                }

                // Movimiento simple válido
                casillas[fDestino, cDestino] = piezaOrigen;
                casillas[fOrigen, cOrigen] = Pieza.Vacia;
                return true;
            }

            // CASO 2: Intento de captura / salto (2 casillas)
            if (difCol == 2)
            {
                // Validar dirección del salto para peones
                if (piezaOrigen == Pieza.PeonNegro && difFila != 2)
                {
                    Console.WriteLine("\n[Error] Los peones negros solo pueden capturar hacia abajo (+2 filas).");
                    return false;
                }

                if (piezaOrigen == Pieza.PeonBlanco && difFila != -2)
                {
                    Console.WriteLine("\n[Error] Los peones blancos solo pueden capturar hacia arriba (-2 filas).");
                    return false;
                }

                // Calcular las coordenadas de la casilla intermedia
                int filaIntermedia = (fOrigen + fDestino) / 2;
                int colIntermedia = (cOrigen + cDestino) / 2;
                Pieza piezaIntermedia = casillas[filaIntermedia, colIntermedia];

                // Verificar si hay una ficha enemiga en medio
                bool esEnemigo = turnoBlanco ? 
                    (piezaIntermedia == Pieza.PeonNegro || piezaIntermedia == Pieza.ReinaNegra) : 
                    (piezaIntermedia == Pieza.PeonBlanco || piezaIntermedia == Pieza.ReinaBlanca);

                if (!esEnemigo)
                {
                    Console.WriteLine("\n[Error] No hay ninguna ficha enemiga para capturar en ese salto.");
                    return false;
                }

                // Realizar la captura: Mover pieza, vaciar casilla origen y vaciar casilla intermedia
                casillas[fDestino, cDestino] = piezaOrigen;
                casillas[fOrigen, cOrigen] = Pieza.Vacia;
                casillas[filaIntermedia, colIntermedia] = Pieza.Vacia; // ¡Ficha comida!

                Console.WriteLine("\n¡Ficha capturada con éxito!");
                return true;
            }

            return false;
        }        
         class Program
        {
            static void Main(string[] args)
            {
                Tablero miTablero = new Tablero();

                // Variable para controlar de quién es el turno (inician las Blancas por regla estándar)
                bool turnoBlanco = true;

                while (true)
                {
                    miTablero.MostrarTablero();

                   // Mostramos en pantalla de quién es el turno
                   string jugadorActual = turnoBlanco ? "BLANCAS (B)" : "NEGRAS (N)";
                   Console.WriteLine($"--- Turno de las {jugadorActual} ---");

                    int fOrigen = PedirCoordenada("Fila de la ficha a mover (0-7): ");
                    int cOrigen = PedirCoordenada("Columna de la ficha a mover (0-7): ");
                        
                    int fDestino = PedirCoordenada("Fila destino (0-7): ");
                    int cDestino = PedirCoordenada("Columna destino (0-7): ");

                    // Intentamos ejecutar el movimiento
                    bool exito = miTablero.MoverPieza(fOrigen, cOrigen, fDestino, cDestino, turnoBlanco);

                    if (!exito)
                    {
                      // Si el movimiento fue válido, cambiamos de turno
                      turnoBlanco = !turnoBlanco;
                    }
                    else
                    {
                      Console.WriteLine("Presiona Enter para reintentar tu turno...");
                      Console.ReadLine();
                    }
                }
            }
            // Función auxiliar para capturar números válidos del teclado
            static int PedirCoordenada(string mensaje)
            {
                int numero;
                while (true)
                {
                    Console.Write(mensaje);
                    string entrada = Console.ReadLine();

                    // Intentamos convertir el texto ingresado a un número de 0 a 7
                    if (int.TryParse(entrada, out numero) && numero >= 0 && numero <= 7)
                    {
                        return numero; 
                    }

                    Console.WriteLine("¡Entrada inválida! Ingresa un número entero entre 0 y 7.");
                }
            }
        }
    }
}