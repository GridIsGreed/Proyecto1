using System;

namespace DamasChinas
{
    // 1. Definimos los tipos de casillas y piezas usando un Enum
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
            // 2. Creamos una matriz (arreglo bidimensional) de 8x8
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
                // 3. Recorremos filas y columnas para asegurar que todo esté vacío
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

        // Lógica para validar y mover una ficha
        public bool MoverPieza(int fOrigen, int cOrigen, int fDestino, int cDestino)
        {
            Pieza piezaOrigen = casillas[fOrigen, cOrigen];
            Pieza piezaDestino = casillas[fDestino, cDestino];

            // 1. Validar que haya una pieza en el origen
            if (piezaOrigen == Pieza.Vacia)
            {
                Console.WriteLine("\n[Error] No hay ninguna ficha en la casilla seleccionada.");
                return false;
            }

            // 2. Validar que el destino esté completamente libre
            if (piezaDestino != Pieza.Vacia)
            {
                Console.WriteLine("\n[Error] La casilla de destino está ocupada.");
                return false;
            }

            // 3. Calcular desplazamientos (Math.Abs obtiene el valor absoluto/positivo)
            int difFila = fDestino - fOrigen;
            int difCol = Math.Abs(cDestino - cOrigen);

            // 4. Validar movimiento diagonal simple de 1 casilla (columna cambia en 1)
            if (difCol != 1)
            {
                Console.WriteLine("\n[Error] Los peones solo se mueven 1 casilla en diagonal.");
                return false;
            }

            // 5. Validar la dirección según el tipo de peón
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

            // 6. Si pasó todas las reglas, ejecutamos el movimiento
            casillas[fDestino, cDestino] = piezaOrigen;
            casillas[fOrigen, cOrigen] = Pieza.Vacia;
            return true;
        }
        
         class Program
        {
            static void Main(string[] args)
            {
                Tablero miTablero = new Tablero();
                while (true)
                {
                    miTablero.MostrarTablero();
                    Console.WriteLine("--- TURNO DE JUEGO ---");
                    // Aquí podrías agregar la lógica para mover piezas.
                    int fOrigen = PedirCoordenada("Fila de la ficha a mover (0-7): ");
                    int cOrigen = PedirCoordenada("Columna de la ficha a mover (0-7): ");
                        
                    int fDestino = PedirCoordenada("Fila destino (0-7): ");
                    int cDestino = PedirCoordenada("Columna destino (0-7): ");

                    // Intentamos ejecutar el movimiento
                    bool exito = miTablero.MoverPieza(fOrigen, cOrigen, fDestino, cDestino);

                    if (!exito)
                    {
                      Console.WriteLine("Presiona Enter para reintentar...");
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
                        return numero; // Entrada válida, devolvemos el valor
                    }

                    Console.WriteLine("¡Entrada inválida! Ingresa un número entero entre 0 y 7.");
                }
            }
        }
    }
}