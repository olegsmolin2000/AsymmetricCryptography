namespace AsymmetricCryptographyWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keycoladd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Keys", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Keys", "Content");
        }
    }
}
