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

                     Console.WriteLine($"\nIntentando mover de ({fOrigen}, {cOrigen}) a ({fDestino}, {cDestino})...");
                     Console.WriteLine("Presiona Enter para continuar...");
                     Console.ReadLine();
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