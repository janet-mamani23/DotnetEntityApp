using entities_library.comment;
using entities_library.file_system;
using entities_library.login;
using entities_library.movie;
using entities_library.Qualify;
using Microsoft.EntityFrameworkCore;

namespace dao_library;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
    : base (options)
    {}

    public DbSet<Comment>? Comments {get; set;}
    public DbSet<FileEntity> ? Files {get; set;}
    public DbSet<FileType>? FileTypes {get; set;}
    public DbSet<Person>? Persons {get; set;}
    public DbSet<User>? Users {get; set;}
    public DbSet<UserBan>? UserBans {get; set;}
    public DbSet<Genre>? Genres {get; set;}
    public DbSet<Movie>? Movies {get; set;} //repositorio de daos movie
    public DbSet<Qualify>? Qualifies {get; set;}
}
