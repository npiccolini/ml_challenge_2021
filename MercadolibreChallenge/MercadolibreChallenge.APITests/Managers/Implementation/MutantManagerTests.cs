using Microsoft.VisualStudio.TestTools.UnitTesting;
using MercadolibreChallenge.API.Managers.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using MercadolibreChallenge.API.Exceptions;

namespace MercadolibreChallenge.API.Managers.Implementation.Tests
{
    [TestClass()]
    public class MutantManagerTests
    {
        [TestMethod()]
        public void No_Mutant_Test()
        {
            // Arrange
            var array = new string[] { "ATGCGA", "CAGTGC", "TTATTT", "AGACGG", "GCGTCA", "TCACTG" };
            var manager = new MutantManager();

            // Act
            var result = manager.IsMutant(array);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void Mutant_Test()
        {
            // Arrange
            var array = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            var manager = new MutantManager();

            // Act
            var result = manager.IsMutant(array);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Null_Array_Test()
        {
            try
            {
                // Arrange
                string[] array = null;
                var manager = new MutantManager();

                // Act
                var result = manager.IsMutant(array);

                // Assert
                Assert.IsTrue(result);
            }
            catch(InvalidDnaException ex)
            {
                Assert.AreEqual("El array ingresado es null", ex.Message);
            }
        }

        [TestMethod()]
        public void Null_Row_Test()
        {
            try
            {
                // Arrange
                string[] array = new string[] { null, "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }; ;
                var manager = new MutantManager();

                // Act
                var result = manager.IsMutant(array);

                // Assert
                Assert.IsTrue(result);
            }
            catch (InvalidDnaException ex)
            {
                Assert.AreEqual("Una línea del array es null", ex.Message);
            }
        }

        [TestMethod()]
        public void Invalid_Sequence_Test()
        {
            try
            {
                // Arrange
                string[] array = new string[] { "123456", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }; ;
                var manager = new MutantManager();

                // Act
                var result = manager.IsMutant(array);

                // Assert
                Assert.IsTrue(result);
            }
            catch (InvalidDnaException ex)
            {
                Assert.AreEqual("Sequencia inválida! (solo se admiten las siguientes letras [ACGT]", ex.Message);
            }
        }

        [TestMethod()]
        public void Min_Data_Test()
        {
            try
            {
                // Arrange
                string[] array = new string[] { "AGAAGG", "CCCCTA", "TCACTG" }; ;
                var manager = new MutantManager();

                // Act
                var result = manager.IsMutant(array);

                // Assert
                Assert.IsTrue(result);
            }
            catch (InvalidDnaException ex)
            {
                Assert.AreEqual("La matriz no tiene las dimesiones mínimas para ser evaluada. El mínimo es 4x4", ex.Message);
            }
        }
    }
}