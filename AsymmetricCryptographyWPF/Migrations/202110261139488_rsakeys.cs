namespace AsymmetricCryptographyWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rsakeys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RsaPrivateKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Modulus = c.String(),
                        PrivateExponent = c.String(),
                        Key_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Keys", t => t.Key_Id)
                .Index(t => t.Key_Id);
            
            CreateTable(
                "dbo.RsaPublicKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Modulus = c.String(),
                        PublicExponent = c.String(),
                        Key_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Keys", t => t.Key_Id)
                .Index(t => t.Key_Id);
            
            AddColumn("dbo.Keys", "Permission", c => c.String());
            AddColumn("dbo.Keys", "BinarySize", c => c.Int(nullable: false));
            DropColumn("dbo.Keys", "Permittion");
            DropColumn("dbo.Keys", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Keys", "Content", c => c.String());
            AddColumn("dbo.Keys", "Permittion", c => c.String());
            DropForeignKey("dbo.RsaPublicKeys", "Key_Id", "dbo.Keys");
            DropForeignKey("dbo.RsaPrivateKeys", "Key_Id", "dbo.Keys");
            DropIndex("dbo.RsaPublicKeys", new[] { "Key_Id" });
            DropIndex("dbo.RsaPrivateKeys", new[] { "Key_Id" });
            DropColumn("dbo.Keys", "BinarySize");
            DropColumn("dbo.Keys", "Permission");
            DropTable("dbo.RsaPublicKeys");
            DropTable("dbo.RsaPrivateKeys");
        }
    }
}
