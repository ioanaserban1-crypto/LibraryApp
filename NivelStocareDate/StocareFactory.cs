namespace NivelStocareDate.Administrare
{
    // Clasa Factory — decide ce implementare a IStocareDate se foloseste.
    public static class StocareFactory
    {
        public static IStocareDate GetAdministratorStocare(string formatSalvare, string folderDate)
        {
            return formatSalvare.ToLower() switch
            {
                "txt" => new AdministratorFisierText(folderDate),
                _     => new AdministratorEntitateMemorie()
            };
        }
    }
}