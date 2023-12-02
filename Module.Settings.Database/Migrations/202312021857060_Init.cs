namespace Module.Settings.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SettingEntries",
                c => new
                    {
                        Area = c.String(nullable: false, maxLength: 128),
                        Id = c.String(nullable: false, maxLength: 128),
                        Value_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Area, t.Id })
                .ForeignKey("dbo.SettingValues", t => t.Value_Id, cascadeDelete: true)
                .Index(t => t.Value_Id);
            
            CreateTable(
                "dbo.SettingValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(),
                        Value1 = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Key_Id = c.Int(),
                        Value_Id = c.Int(),
                        DictionarySettingValue_Id = c.Int(),
                        ListSettingValue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SettingValues", t => t.Key_Id)
                .ForeignKey("dbo.SettingValues", t => t.Value_Id)
                .ForeignKey("dbo.SettingValues", t => t.DictionarySettingValue_Id)
                .ForeignKey("dbo.SettingValues", t => t.ListSettingValue_Id)
                .Index(t => t.Key_Id)
                .Index(t => t.Value_Id)
                .Index(t => t.DictionarySettingValue_Id)
                .Index(t => t.ListSettingValue_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SettingEntries", "Value_Id", "dbo.SettingValues");
            DropForeignKey("dbo.SettingValues", "ListSettingValue_Id", "dbo.SettingValues");
            DropForeignKey("dbo.SettingValues", "DictionarySettingValue_Id", "dbo.SettingValues");
            DropForeignKey("dbo.SettingValues", "Value_Id", "dbo.SettingValues");
            DropForeignKey("dbo.SettingValues", "Key_Id", "dbo.SettingValues");
            DropIndex("dbo.SettingValues", new[] { "ListSettingValue_Id" });
            DropIndex("dbo.SettingValues", new[] { "DictionarySettingValue_Id" });
            DropIndex("dbo.SettingValues", new[] { "Value_Id" });
            DropIndex("dbo.SettingValues", new[] { "Key_Id" });
            DropIndex("dbo.SettingEntries", new[] { "Value_Id" });
            DropTable("dbo.SettingValues");
            DropTable("dbo.SettingEntries");
        }
    }
}
