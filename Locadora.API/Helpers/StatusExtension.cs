using System.Runtime.CompilerServices;

namespace Locadora.API.Helpers {
    public static class StatusExtension {
        public static string GetStatus(string forecastDateParam, string returnDateParam) {
            if (string.IsNullOrEmpty(returnDateParam)) {
                return "Pendente";
            }

            DateTime forecastDate;
            DateTime returnDate;

            if (DateTime.TryParse(forecastDateParam, out forecastDate) && DateTime.TryParse(returnDateParam, out returnDate)) {
                if (returnDate > forecastDate) {
                    return "Atrasado";
                } else {
                    return "No Prazo";
                }
            }

            return null;
        }
    }
}
