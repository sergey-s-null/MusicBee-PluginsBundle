namespace Module.MusicSourcesStorage.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false),
                        Size = c.Long(nullable: false),
                        SourceId = c.Int(nullable: false),
                        IsCover = c.Boolean(),
                        Data = c.Binary(),
                        IsListened = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MusicSourceModels", t => t.SourceId, cascadeDelete: true)
                .Index(t => t.SourceId);
            
            CreateTable(
                "dbo.MusicSourceModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdditionalInfo_Name = c.String(nullable: false),
                        AdditionalInfo_TargetFilesDirectory = c.String(nullable: false),
                        TorrentFile = c.Binary(),
                        Post_Id = c.Long(),
                        Post_OwnerId = c.Long(),
                        Document_Id = c.Long(),
                        Document_OwnerId = c.Long(),
                        Document_Name = c.String(),
                        Document_Uri = c.String(),
                        Document_Size = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileModels", "SourceId", "dbo.MusicSourceModels");
            DropIndex("dbo.FileModels", new[] { "SourceId" });
            DropTable("dbo.MusicSourceModels");
            DropTable("dbo.FileModels");
        }
    }
}
