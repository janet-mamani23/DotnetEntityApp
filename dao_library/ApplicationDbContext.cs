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

    public DbSet<Comment>? Comments {get;}
    public DbSet<FileEntity> ? Files {get;}
    public DbSet<FileType>? FileTypes {get;}
    public DbSet<Person>? Persons {get;}
    public DbSet<User>? Users {get;}
    public DbSet<UserBan>? UserBans {get;}
    public DbSet<Genre>? Genres {get;}
    public DbSet<Movie>? Movies {get;} //repositorio de daos movie
    public DbSet<Qualify>? Qualifies {get;}
}
