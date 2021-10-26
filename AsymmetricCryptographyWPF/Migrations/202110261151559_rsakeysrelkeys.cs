namespace AsymmetricCryptographyWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rsakeysrelkeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RsaPrivateKeys", "Key_Id", "dbo.Keys");
            DropForeignKey("dbo.RsaPublicKeys", "Key_Id", "dbo.Keys");
            DropIndex("dbo.RsaPrivateKeys", new[] { "Key_Id" });
            DropIndex("dbo.RsaPublicKeys", new[] { "Key_Id" });
            RenameColumn(table: "dbo.RsaPrivateKeys", name: "Key_Id", newName: "KeyId");
            RenameColumn(table: "dbo.RsaPublicKeys", name: "Key_Id", newName: "KeyId");
            AlterColumn("dbo.RsaPrivateKeys", "KeyId", c => c.Int(nullable: false));
            AlterColumn("dbo.RsaPublicKeys", "KeyId", c => c.Int(nullable: false));
            CreateIndex("dbo.RsaPrivateKeys", "KeyId");
            CreateIndex("dbo.RsaPublicKeys", "KeyId");
            AddForeignKey("dbo.RsaPrivateKeys", "KeyId", "dbo.Keys", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RsaPublicKeys", "KeyId", "dbo.Keys", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RsaPublicKeys", "KeyId", "dbo.Keys");
            DropForeignKey("dbo.RsaPrivateKeys", "KeyId", "dbo.Keys");
            DropIndex("dbo.RsaPublicKeys", new[] { "KeyId" });
            DropIndex("dbo.RsaPrivateKeys", new[] { "KeyId" });
            AlterColumn("dbo.RsaPublicKeys", "KeyId", c => c.Int());
            AlterColumn("dbo.RsaPrivateKeys", "KeyId", c => c.Int());
            RenameColumn(table: "dbo.RsaPublicKeys", name: "KeyId", newName: "Key_Id");
            RenameColumn(table: "dbo.RsaPrivateKeys", name: "KeyId", newName: "Key_Id");
            CreateIndex("dbo.RsaPublicKeys", "Key_Id");
            CreateIndex("dbo.RsaPrivateKeys", "Key_Id");
            AddForeignKey("dbo.RsaPublicKeys", "Key_Id", "dbo.Keys", "Id");
            AddForeignKey("dbo.RsaPrivateKeys", "Key_Id", "dbo.Keys", "Id");
        }
    }
}
