namespace entities_library.movie
{
    public class Genero
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public TypeGenero TipoGenero { get; set; } // Enum TipoGenero
    }
}
