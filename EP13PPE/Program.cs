using System;
using System.IO;
using System.Linq;

namespace EP13PPE
{
    class Program
    {
        static void Main(string[] args)
        {

            string inputPath = @"C:\Users\Haniel\source\repos\Trabalho3PPE\EP13PPE\XdadosTeste.txt";






            // String input = File.ReadAllText(@"C:\Users\Haniel\Source\Repos\AlgoritmoMenorCaminho\HillClimbing\entrada.txt");
            string probabilidades = File.ReadAllText(inputPath).Replace(".", ",");

            var lineCount = File.ReadLines(inputPath).Count();

            var colCount = probabilidades.Split('\n')[0].Split(' ').Length;


            int k = 0, l = 0;
            double?[,] matriz = new double?[lineCount, colCount];
            foreach (var row in probabilidades.Split('\n'))
            {
                l = 0;
                foreach (var col in row.Trim().Split(' '))
                {
                    if (col.Trim() != "")
                    {

                        matriz[k, l] = Convert.ToDouble(col.Trim());
                        l++;

                    }
                }
                k++;
            }


            matriz = matriz;


        }


        public static double? ProdutorioVetor(double?[] vetor)
        {
            double? result = 1;

            for (int i = 0; i < vetor.Length; i++)
            {
                if (vetor[i] != 0)
                {

                    result *= vetor[i];

                }
            }

            return result;

        }



        public static double?[] MultiplicarMatriz(double?[,] matriz, double?[] vetor)
        {


            var colCount = matriz.GetLength(1);
            var rowCount = matriz.GetLength(0);

            double?[] vetorResultado = new double?[rowCount];


            if (colCount != vetor.Length)
            {
                throw new System.Exception("Tamanho do vetor (" + vetor.Length + ") diferente do número de colunas da matriz (" + colCount + ").");
            }

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = matriz[i, j].GetValueOrDefault() * vetor[j];
                }

            }


            for (int k = 0; k < matriz.GetLength(0); k++)
            {
                double? sum = 0;
                for (int l = 0; l < matriz.GetLength(1); l++)
                {
                    sum = sum + matriz[k, l];

                }

                vetorResultado[k] = sum;

            }


            return vetorResultado;
        }


        public static double?[,] MultiplicarMatriz(double?[,] matriz, double numero)
        {


            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = matriz[i, j].GetValueOrDefault() * numero;
                }

            }


            return matriz;
        }



        public static int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public static void ImprimirMatriz(double?[,] matriz)
        {
            int valor;

            var rowCount = matriz.GetLength(0);
            var colCount = matriz.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)

                    if (matriz[row, col] == null)
                    {
                        Console.Write("null \t");
                    }
                    else
                    {
                        Console.Write(String.Format("{0}\t", matriz[row, col]));
                    }

                Console.WriteLine();
            }
        }

        public static void ImprimirMatriz(int?[,] matriz)
        {
            int valor;

            var rowCount = matriz.GetLength(0);
            var colCount = matriz.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)

                    if (matriz[row, col] == null)
                    {
                        Console.Write("null \t");
                    }
                    else
                    {
                        Console.Write(String.Format("{0}\t", matriz[row, col]));
                    }

                Console.WriteLine();
            }
        }




    }
}
