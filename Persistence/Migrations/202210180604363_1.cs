namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NombreAnunciosCaracteristicas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        NombreAnuncioId = c.Int(nullable: false),
                        CaracteristicaId = c.Int(nullable: false),
                        estado = c.Boolean(nullable: false),
                        NombreAnuncios_id = c.Int(),
                        NombreAnuncios_id1 = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Caracteristicas", t => t.CaracteristicaId, cascadeDelete: true)
                .ForeignKey("dbo.NombreAnuncios", t => t.NombreAnuncioId, cascadeDelete: true)
                .Index(t => t.NombreAnuncioId)
                .Index(t => t.CaracteristicaId)
                .Index(t => t.NombreAnuncios_id)
                .Index(t => t.NombreAnuncios_id1);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Caracteristicas", "NombreAnuncios_id1", c => c.Int());
            AddColumn("dbo.Caracteristicas", "NombreAnuncios_id", c => c.Int());
            DropForeignKey("dbo.NombreAnunciosCaracteristicas", "NombreAnuncioId", "dbo.NombreAnuncios");
            DropForeignKey("dbo.NombreAnunciosCaracteristicas", "CaracteristicaId", "dbo.Caracteristicas");
            DropIndex("dbo.NombreAnunciosCaracteristicas", new[] { "NombreAnuncios_id1" });
            DropIndex("dbo.NombreAnunciosCaracteristicas", new[] { "NombreAnuncios_id" });
            DropIndex("dbo.NombreAnunciosCaracteristicas", new[] { "CaracteristicaId" });
            DropIndex("dbo.NombreAnunciosCaracteristicas", new[] { "NombreAnuncioId" });
            DropTable("dbo.NombreAnunciosCaracteristicas");
            CreateIndex("dbo.Caracteristicas", "NombreAnuncios_id1");
            CreateIndex("dbo.Caracteristicas", "NombreAnuncios_id");
        }
    }
}
