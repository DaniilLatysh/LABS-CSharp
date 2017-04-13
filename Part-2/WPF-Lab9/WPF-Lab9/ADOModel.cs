namespace WPF_Lab9
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ADOModel : DbContext
    {
        public ADOModel()
            : base("name=ADOModel")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<WH> WHs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.WHs)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("Entity").MapLeftKey("ID_Category").MapRightKey("ID_Part"));
        }
    }
}
