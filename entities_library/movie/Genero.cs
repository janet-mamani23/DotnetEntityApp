namespace entities_library.movie;

public class Genero
{
    public int Id { get; set; } // Identificador único para el género
    public TypeGenero Tipo { get; set; } // Tipo de género basado en la enumeración
    public string Descripcion { get; set; } = string.Empty; // Descripción opcional

        // Constructor básico
    public Genero(int id, TypeGenero tipo)
    {
        Id = id;
        Tipo = tipo;
    }
}
