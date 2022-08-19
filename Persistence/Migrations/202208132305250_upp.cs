namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Anuncios", "infoCatalogoId", "dbo.InfoImagens");
            DropForeignKey("dbo.Anuncios", "infoImagenId", "dbo.InfoImagens");
            DropForeignKey("dbo.PayMentMethods", "extra_id", "dbo.extras");
            DropForeignKey("dbo.data", "payment_method_id", "dbo.PayMentMethods");
            DropForeignKey("dbo.WompiPayments", "data_id", "dbo.data");
            DropForeignKey("dbo.Anuncios", "pago_id", "dbo.WompiPayments");
            DropForeignKey("dbo.AnuncioTrabajoes", "Empresa_id", "dbo.Empresas");
            DropForeignKey("dbo.Empresas", "rolId", "dbo.Roles");
            DropIndex("dbo.Anuncios", new[] { "infoImagenId" });
            DropIndex("dbo.Anuncios", new[] { "infoCatalogoId" });
            DropIndex("dbo.Anuncios", new[] { "pago_id" });
            DropIndex("dbo.WompiPayments", new[] { "data_id" });
            DropIndex("dbo.data", new[] { "payment_method_id" });
            DropIndex("dbo.PayMentMethods", new[] { "extra_id" });
            DropIndex("dbo.AnuncioTrabajoes", new[] { "Empresa_id" });
            DropIndex("dbo.Empresas", new[] { "rolId" });
            RenameColumn(table: "dbo.Anuncios", name: "catalogoId", newName: "catalogo_id");
            RenameIndex(table: "dbo.Anuncios", name: "IX_catalogoId", newName: "IX_catalogo_id");
            AddColumn("dbo.Anuncios", "imagen", c => c.Binary());
            DropColumn("dbo.Anuncios", "actDiseno");
            DropColumn("dbo.Anuncios", "infoImagenId");
            DropColumn("dbo.Anuncios", "infoCatalogoId");
            DropColumn("dbo.Anuncios", "referenciaCobro");
            DropColumn("dbo.Anuncios", "referenciaPago");
            DropColumn("dbo.Anuncios", "path");
            DropColumn("dbo.Anuncios", "pago_id");
            DropColumn("dbo.AnuncioTrabajoes", "Empresa_id");
            DropTable("dbo.InfoImagens");
            DropTable("dbo.WompiPayments");
            DropTable("dbo.data");
            DropTable("dbo.PayMentMethods");
            DropTable("dbo.extras");
            DropTable("dbo.Empresas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                        HomeTown = c.String(),
                        LastName = c.String(),
                        Name = c.String(),
                        BirthDate = c.DateTime(),
                        phoneNumber = c.String(),
                        Nit = c.String(),
                        NombreEmpresa = c.String(),
                        SectorComercial = c.String(),
                        rolId = c.Int(),
                        estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.extras",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        brand = c.String(),
                        last_four = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PayMentMethods",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        installments = c.String(),
                        type = c.String(),
                        extra_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.data",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        created_at = c.String(),
                        amount_in_cents = c.String(),
                        reference = c.String(),
                        currency = c.String(),
                        payment_method_type = c.String(),
                        extra = c.String(),
                        brand = c.String(),
                        installments = c.String(),
                        redirect_url = c.String(),
                        status = c.String(),
                        status_message = c.String(),
                        merchant = c.String(),
                        legal_name = c.String(),
                        contact_name = c.String(),
                        logo_url = c.String(),
                        legal_id_type = c.String(),
                        email = c.String(),
                        legal_id = c.String(),
                        meta = c.String(),
                        payment_method_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.WompiPayments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        data_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.InfoImagens",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        infoNegocio = c.String(),
                        textoImagen = c.String(),
                        color = c.String(),
                        path = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.AnuncioTrabajoes", "Empresa_id", c => c.Int());
            AddColumn("dbo.Anuncios", "pago_id", c => c.Int());
            AddColumn("dbo.Anuncios", "path", c => c.String());
            AddColumn("dbo.Anuncios", "referenciaPago", c => c.String());
            AddColumn("dbo.Anuncios", "referenciaCobro", c => c.String());
            AddColumn("dbo.Anuncios", "infoCatalogoId", c => c.Int());
            AddColumn("dbo.Anuncios", "infoImagenId", c => c.Int());
            AddColumn("dbo.Anuncios", "actDiseno", c => c.Boolean(nullable: false));
            DropColumn("dbo.Anuncios", "imagen");
            RenameIndex(table: "dbo.Anuncios", name: "IX_catalogo_id", newName: "IX_catalogoId");
            RenameColumn(table: "dbo.Anuncios", name: "catalogo_id", newName: "catalogoId");
            CreateIndex("dbo.Empresas", "rolId");
            CreateIndex("dbo.AnuncioTrabajoes", "Empresa_id");
            CreateIndex("dbo.PayMentMethods", "extra_id");
            CreateIndex("dbo.data", "payment_method_id");
            CreateIndex("dbo.WompiPayments", "data_id");
            CreateIndex("dbo.Anuncios", "pago_id");
            CreateIndex("dbo.Anuncios", "infoCatalogoId");
            CreateIndex("dbo.Anuncios", "infoImagenId");
            AddForeignKey("dbo.Empresas", "rolId", "dbo.Roles", "id");
            AddForeignKey("dbo.AnuncioTrabajoes", "Empresa_id", "dbo.Empresas", "id");
            AddForeignKey("dbo.Anuncios", "pago_id", "dbo.WompiPayments", "id");
            AddForeignKey("dbo.WompiPayments", "data_id", "dbo.data", "id");
            AddForeignKey("dbo.data", "payment_method_id", "dbo.PayMentMethods", "id");
            AddForeignKey("dbo.PayMentMethods", "extra_id", "dbo.extras", "id");
            AddForeignKey("dbo.Anuncios", "infoImagenId", "dbo.InfoImagens", "id");
            AddForeignKey("dbo.Anuncios", "infoCatalogoId", "dbo.InfoImagens", "id");
        }
    }
}
