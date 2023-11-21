using Entidades;
using Entidades.metodoExtension;

namespace UnitTests
{
    [TestClass]
    public class TestUnitarios2
    {
        [TestMethod]
        /// <summary>
        /// Verifica que al invocar el método FiltrarReservasVigentes, devuelva solo aquellas reservas que están activas.
        /// </summary>
        public void Al_InvocarAlMetodoFiltrarReservasVigentes_DeberiaDevolverSoloAquellasQueEstanActivas()
        {
            // Arrange
            Cliente cliente = new Cliente("Julieta", "Ortega", 25777904, "69228640");
            Cliente cliente2 = new Cliente("Pedro", "Gonzalez", 26577620, "65881452");
            Cliente cliente3 = new Cliente("Juan", "Perez", 38157890, "36649021");

            Vehiculo vehiculo = new Vehiculo("Fiat", "Uno", 2011, "Auto", "JSP222", true);
            Vehiculo vehiculo2 = new Vehiculo("Toyota", "Hilux", 2022, "Camioneta", "AE500NM", true);
            Vehiculo vehiculo3 = new Vehiculo("Ford", "Ka", 2021, "Auto", "AC128PL", false);

            DateTime fechaInicio = new DateTime(2023, 11, 17);
            DateTime fechaFin = new DateTime(2023, 11, 20);

            List<Reserva> listaReservas = new List<Reserva>();

            Reserva reserva1 = new Reserva(cliente, 25777904, vehiculo, "JSP222", fechaInicio, fechaFin, true);
            Reserva reserva2 = new Reserva(cliente2, 26577620, vehiculo, "AE500NM", fechaInicio, fechaFin, false);
            Reserva reserva3 = new Reserva(cliente3, 38157890, vehiculo, "AC128PL", fechaInicio, fechaFin, true);

            listaReservas.Add(reserva1);
            listaReservas.Add(reserva2);
            listaReservas.Add(reserva3);

            // Act
            List<Reserva> resultado = listaReservas.FiltrarReservasVigentes();

            // Assert
            Assert.AreEqual(2, resultado.Count); // Deberían haber 2 reservas vigentes

        }

        [TestMethod]
        /// <summary>
        /// Verifica que al invocar el método FiltrarReservasVigentes con una lista vacía, devuelva cero reservas vigentes.
        /// </summary>
        public void Al_InvocarAlMetodoFiltrarReservasVigentes_SiLaListaEstaVacia_DeberiaDevolverCeroReservasVigentes()
        {
            // Arrange
            List<Reserva> listaReservas = new List<Reserva>();

            // Act
            List<Reserva> resultado = listaReservas.FiltrarReservasVigentes();

            // Assert
            Assert.AreEqual(0, resultado.Count); // Debería ser una lista vacía
        }
            
    }
}