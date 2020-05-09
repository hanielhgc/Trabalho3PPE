using System;
using System.Collections.Generic;
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
            double[,] matriz = new double[lineCount, colCount];
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


            List<double> mediaColunas = new List<double>();
            List<double> desvioColunas = new List<double>();




            for (int col = 0; col < colCount; col++)
            {

                double count = 0;

                for (int row = 0; row < lineCount; row++)
                {

                    count += matriz[row, col];

                }

                mediaColunas.Add(count / lineCount);
            }



            for (int col = 0; col < colCount; col++)
            {
                double resultado = 0;

                for (int row = 0; row < lineCount; row++)
                {
                    //calculando o desvio padrão
                    var result = Math.Pow(mediaColunas[col] - matriz[row, col], 2);
                    resultado = Math.Sqrt(result / lineCount);
                }

                desvioColunas.Add(resultado);
            }


            //calculando R e roh


            List<Roh> rohs = new List<Roh>();


            for (int i = 0; i < colCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {

                    var col1 = ExtrairColuna(matriz, i);
                    var col2 = ExtrairColuna(matriz, j);

                    var r = MultiplicarVetores(col1, col2).Sum();

                    Roh roh = new Roh();
                    roh.nome = "Roh" + i + "-" + j;
                    roh.i = i;
                    roh.j = j;
                    roh.valor = r / (desvioColunas[i] * desvioColunas[j]);
                    rohs.Add(roh);
                }
            }


            Xhat[] xhats = new Xhat[colCount];


            for (int i = 0; i < colCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    Xhat xhat = new Xhat();
                    xhat.coluna = new double[lineCount];

                    var col1 = ExtrairColuna(matriz, i);
                    var col2 = ExtrairColuna(matriz, j);


                    for (int z = 0; z < lineCount; z++)
                    {
                        var ro = rohs.Where(x => x.i == i && x.j == j).FirstOrDefault();
                        xhat.coluna[z] = col1.Average() + ro.valor * desvioColunas[i] * desvioColunas[j] * (col2[z] - col2.Average());

                    }

                    xhats[i] = xhat;

                }
            }


            double[,] MEAM = new double[colCount, colCount];

            double[] xHAT;
            double[] X;

            for (int row = 0; row < colCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {


                    xHAT = xhats[row].coluna;
                    X = ExtrairColuna(matriz, col);

                    double soma = 0;
                    for (int ind = 0; ind < lineCount; ind++)
                    {
                        soma = Math.Abs(xHAT[ind]-X[ind]);
                    }

                    soma = soma / lineCount;

                    MEAM[row, col] = soma;

                }

            }

            ImprimirMatriz(MEAM);


            matriz = matriz;


        }



        public static double[] MultiplicarVetores(double[] vetor1, double[] vetor2)
        {
            //se os tamanhos dos vetores forem diferentes haverá erro
            double[] vetorResultado = new double[vetor1.Length];

            for (int i = 0; i < vetor1.Length; i++)
            {
                vetorResultado[i] = vetor1[i] * vetor2[i];
            }

            return vetorResultado;

        }

        public static double ProdutorioVetor(double[] vetor)
        {
            double result = 1;

            for (int i = 0; i < vetor.Length; i++)
            {
                if (vetor[i] != 0)
                {

                    result *= vetor[i];

                }
            }

            return result;

        }


        public static double[] ExtrairColuna(double[,] matriz, int coluna)
        {


            var colCount = matriz.GetLength(1);
            var lineCount = matriz.GetLength(0);


            double[] vetor = new double[lineCount];


            for (int row = 0; row < lineCount; row++)
            {
                vetor[row] = matriz[row, coluna];
            }




            return vetor;
        }



        public static double[] MultiplicarMatriz(double[,] matriz, double[] vetor)
        {


            var colCount = matriz.GetLength(1);
            var rowCount = matriz.GetLength(0);

            double[] vetorResultado = new double[rowCount];


            if (colCount != vetor.Length)
            {
                throw new System.Exception("Tamanho do vetor (" + vetor.Length + ") diferente do número de colunas da matriz (" + colCount + ").");
            }

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = matriz[i, j] * vetor[j];
                }

            }


            for (int k = 0; k < matriz.GetLength(0); k++)
            {
                double sum = 0;
                for (int l = 0; l < matriz.GetLength(1); l++)
                {
                    sum = sum + matriz[k, l];

                }

                vetorResultado[k] = sum;

            }


            return vetorResultado;
        }


        public static double[,] MultiplicarMatriz(double[,] matriz, double numero)
        {


            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = matriz[i, j] * numero;
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

        public static void ImprimirMatriz(double[,] matriz)
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
