namespace Visug2CommitBOTApp.Model
{
    using System.Data.Entity;

    public class Context : DbContext
    {
        
        public Context() : base("name=Context") {}
        public DbSet<Registrant> Registrants { get; set; }
        public DbSet<RegistrantBotData> RegistrantsBotData { get; set; }
    }
}