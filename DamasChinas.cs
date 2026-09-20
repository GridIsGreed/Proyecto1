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
                Console.Write((int)casillas[fila, col] + " ");
            }
            Console.WriteLine();
        }
    }
    
        class Program
        {
             static void Main(string[] args)
             {
                 Tablero miTablero = new Tablero();
                 miTablero.MostrarTablero();
                 Console.ReadLine();
             }
        }
    }
}