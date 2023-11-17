using Entidades;

namespace UnitTests
{
    [TestClass]
    public class TestUnitarios
    {
        [TestMethod]
        public void Al_InvocarAlMetodoCalcularCostoReserva_SiSeRecibeUnVehiculoDeTipoAuto_DeberiaDevolverElCostoSegunTarifaYDiasAlquilados()
        {
            // Arrange
            Cliente cliente = new Cliente("Pedro", "Gonzalez", 26577620, "65881452");
            Vehiculo vehiculo = new Vehiculo("Fiat", "Uno", 2011, "Auto", "JSP222", true);
            DateTime fechaInicio = new DateTime(2023, 11, 1);
            DateTime fechaFin = new DateTime(2023, 11, 6);

            Reserva reserva = new Reserva(cliente, 12345678, vehiculo, "PatenteAuto", fechaInicio, fechaFin, true);

            // Act
            float costoReserva = reserva.CalcularCostoReserva();

            // Assert
            Assert.AreEqual(Reserva.TarifaAuto * 5, costoReserva);
        }

        [TestMethod]
        public void Al_InvocarAlMetodoCalcularCostoReserva_SiSeRecibeUnVehiculoDeTipoCamioneta_DeberiaDevolverElCostoSegunTarifaYDiasAlquilados()
        {
            // Arrange
            Cliente cliente = new Cliente("Julieta", "Ortega", 25777904, "69228640");
            Vehiculo vehiculo = new Vehiculo("Toyota", "Hilux", 2022, "Camioneta", "AE500NM", true);
            DateTime fechaInicio = new DateTime(2023, 11, 17);
            DateTime fechaFin = new DateTime(2023, 11, 20);

            Reserva reserva = new Reserva(cliente, 12345678, vehiculo, "PatenteCamioneta", fechaInicio, fechaFin, true);

            // Act
            float costoReserva = reserva.CalcularCostoReserva();

            // Assert
            Assert.AreEqual(105000, costoReserva);
        }
    }
}