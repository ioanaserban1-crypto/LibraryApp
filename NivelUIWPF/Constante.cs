namespace NivelUIWPF
{
    public static class Constante
    {
        // Limite pentru titlul cartii
        public const int TITLU_LUNGIME_MIN = 2;
        public const int TITLU_LUNGIME_MAX = 100;

        // Limite pentru numarul de exemplare
        public const int NR_EXEMPLARE_MIN = 1;
        public const int NR_EXEMPLARE_MAX = 50;

        // Limite pentru numele autorului
        public const int AUTOR_NUME_LUNGIME_MIN = 3;
        public const int AUTOR_NUME_LUNGIME_MAX = 80;

        // Limite pentru an (folosit la DatePicker daca e nevoie)
        public const int AN_MIN = 1900;
        public const int AN_MAX = 2025;
    }
}