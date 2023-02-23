using Microsoft.EntityFrameworkCore;

namespace Sdc.Data
{
    public class SdcContext : DbContext
    {
        
        public SdcContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<MAT_Leg> MAT_Legs { get; set; }
        public DbSet<MAT_Match> MAT_Matchs { get; set; }
        public DbSet<MAT_Round> MAT_Rounds { get; set; }
        public DbSet<MAT_Set> MAT_Sets { get; set; }
        public DbSet<MAT_Shot> MAT_Shots { get; set; }
        public DbSet<MAT_Shot_x_Round> MAT_Shot_x_Round { get; set; }
        public DbSet<PLA_EloHistory> PLA_EloHistories { get; set; }
        public DbSet<PLA_Player> PLA_Players { get; set; }
        public DbSet<TUR_Tournament> TUR_Tournaments { get; set; }

    }
}
