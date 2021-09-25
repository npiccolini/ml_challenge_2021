using MercadolibreChallenge.API.Exceptions;
using MercadolibreChallenge.API.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MercadolibreChallenge.API.Managers.Implementation
{
    public class MutantManager
    {
        private readonly int MAX_MATCHES = 2;
        private readonly int MIN_VALUES = 4;
        private readonly int PATTERN_MATCHES = 4;

        private readonly List<char> LETTERS = new List<char> { 'A', 'T', 'C', 'G' };
        public bool IsMutant(string[] dna)
        {
            var matches = 0;
            ValidateSecuence(dna);

            //convierto el array en una matriz
            var matrixDna = ConvertToMatrix(dna);

            //recorro la secuencia horizontalmente
            matches = CheckArrayDna(Matrix2dToArrayHorizontal(matrixDna), matches);
            if (matches < MAX_MATCHES)
            {
                //recorro la secuencia verticalmente
                matches = CheckArrayDna(Matrix2dToArrayVertical(matrixDna), matches);
                if (matches < MAX_MATCHES)
                {
                    //recorro la secuencia de forma oblicua
                    matches = CheckArrayDna(Matrix2dToArrayDiagonal_SWNE(matrixDna), matches);
                    if (matches < MAX_MATCHES)
                    {
                        matches = CheckArrayDna(Matrix2dToArrayDiagonal_SENW(matrixDna), matches);
                    }
                }
            }

            return matches >= MAX_MATCHES;
        }

        public void SaveData(string[] dna, bool isMutant)
        {
            new HumanRepository().Save(new Repositories.Entities.Human { Dna = JsonConvert.SerializeObject(dna), Mutant = isMutant });
        }
        /// <summary>
        /// valido la sequencia ingresada para que cumpla con los requisitos minimos
        /// </summary>
        /// <param name="dna"></param>
        private void ValidateSecuence(string[] dna)
        {
            if (dna == null)
                throw new InvalidDnaException("El array ingresado es null");
            foreach (var row in dna)
            {
                if (row == null)
                    throw new InvalidDnaException("Una línea del array es null");

                var regex = new Regex(@"^[ACGT]+$", RegexOptions.None);
                var result = regex.IsMatch(row, 0);
                if (!result)
                    throw new InvalidDnaException("Sequencia inválida! (solo se admiten las siguientes letras [ACGT]");
            }
        }

        /// <summary>
        /// convierte el array en una matriz de caracteres
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private char[,] ConvertToMatrix(string[] array)
        {
            if (array == null || array[0].Length < MIN_VALUES || array.Length < MIN_VALUES)
                throw new InvalidDnaException("La matriz no tiene las dimesiones mínimas para ser evaluada. El mínimo es 4x4");

            var matrix = new char[array.Length, array[0].Length];
            var index = 0;
            foreach (var row in array)
            {
                var chars = row.ToCharArray();
                var length = 0;
                foreach (var c in chars)
                {
                    matrix[index, length] = c;
                    length++;
                }
                index++;
            }
            return matrix;
        }

        /// <summary>
        /// Convierte la matriz en un array recorrido verticalmente
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private string[] Matrix2dToArrayVertical(char[,] matrix)
        {
            string[] arrayV = new string[matrix.GetLength(1)];
            for (int k = 0; k <= matrix.GetLength(1) - 1; k++)
            {
                var m = 0;
                var n = k;
                var row = string.Empty;
                while (m <= matrix.GetLength(0) - 1)
                {
                    row += matrix[m, n];
                    m++;
                }
                arrayV[k] = row;
            }
            return arrayV;
        }

        private string[] Matrix2dToArrayHorizontal(char[,] matrix)
        {
            string[] arrayH = new string[matrix.GetLength(0)];
            for (int k = 0; k <= matrix.GetLength(0) - 1; k++)
            {
                var m = k;
                var n = 0;
                var row = string.Empty;
                while (n <= matrix.GetLength(1) - 1)
                {
                    row += matrix[m, n];
                    n++;
                }
                arrayH[k] = row;
            }
            return arrayH;
        }

        /// <summary>
        /// Convierte la matriz en un array recorrido diagonal de SurOeste a NorEste
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private string[] Matrix2dToArrayDiagonal_SWNE(char[,] matrix)
        {
            string[] arrayhalf1 = new string[matrix.GetLength(0)];
            string[] arrayhalf2 = new string[matrix.GetLength(1) - 1];

            for (int k = 0; k <= matrix.GetLength(0) - 1; k++)
            {
                var i = k;
                var j = 0;
                var row = string.Empty;
                while (i >= 0 && j <= matrix.GetLength(1) - 1)
                {
                    row += matrix[i, j];
                    i--;
                    j++;
                }
                arrayhalf1[k] = row;
            }
            for (int k = 1; k <= matrix.GetLength(1) - 1; k++)
            {
                var i = matrix.GetLength(0) - 1;
                var j = k;
                var row = string.Empty;
                while (i >= 0 && j <= matrix.GetLength(1) - 1)
                {
                    row += matrix[i, j];
                    i--;
                    j++;
                }
                arrayhalf2[k - 1] = row;
            }
            var unionArr = arrayhalf1.Union(arrayhalf2).ToArray();
            return unionArr;
        }

        /// <summary>
        /// Convierte la matriz en un array recorrido diagonal de SurEste a NorOeste
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private string[] Matrix2dToArrayDiagonal_SENW(char[,] matrix)
        {

            string[] arrayhalf1 = new string[matrix.GetLength(1) - 1];
            string[] arrayhalf2 = new string[matrix.GetLength(0)];

            for (int k = 0; k < matrix.GetLength(1) - 1; k++)
            {
                var m = matrix.GetLength(0) - 1;
                var n = k;
                var row = string.Empty;
                while (n >= 0 && m >= 0)
                {
                    row += matrix[m, n];
                    m--;
                    n--;
                }
                arrayhalf1[k] = row;
            }
            var index = 0;
            for (int k = matrix.GetLength(0) - 1; k >= 0; k--)
            {

                var m = k;
                var n = matrix.GetLength(1) - 1;
                var row = string.Empty;
                while (n >= 0 && m >= 0)
                {
                    row += matrix[m, n];
                    m--;
                    n--;
                }
                arrayhalf2[index] = row;
                index++;
            }
            var unionArr = arrayhalf1.Union(arrayhalf2).ToArray();
            return unionArr;
        }

        /// <summary>
        /// Busca coincidencias de letras en una secuencia determinada
        /// </summary>
        /// <param name="dnaRow"></param>
        /// <returns></returns>
        private bool CheckRowDna(string dnaRow)
        {
            var count = 0;
            char prevLetter = 'x';
            foreach (var letter in dnaRow)
            {
                if (letter == prevLetter)
                    count++;
                else
                {
                    prevLetter = letter;
                    count = 1;
                }
                if (count >= PATTERN_MATCHES)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Revisa el array buscando coincidencia de letras
        /// </summary>
        /// <param name="arrayDna"></param>
        /// <param name="matches"></param>
        /// <returns></returns>
        private int CheckArrayDna(string[] arrayDna, int matches)
        {
            foreach (var dnaRow in arrayDna)
            {
                if (dnaRow.Length < MIN_VALUES)
                    continue;

                if (CheckRowDna(dnaRow))
                    matches++;

                if (matches >= MAX_MATCHES)
                    return matches;
            }
            return matches;
        }
    }
}
