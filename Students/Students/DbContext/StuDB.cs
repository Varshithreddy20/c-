using Microsoft.EntityFrameworkCore;


namespace Students.DbContext
{
    public class StuDB : DbContext
    {
        public StuDB(DbContextOptions<StuDB> options) : base(options) { }

    }
}
