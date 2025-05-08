using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace Prueba.Migrations
{
    public class CreateUsersTable
    {
        [Migration(1)]
        public class CreateUserTable : Migration
        {
            public override void Up()
            {
                Create.Table("Users")
                    .WithColumn("ID").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Name").AsString(100).NotNullable()
                    .WithColumn("LastName").AsString(100).NotNullable()
                    .WithColumn("Address").AsString(255).NotNullable()
                    .WithColumn("TellNumber").AsString(20).NotNullable()
                    .WithColumn("BirthDay").AsString(10).NotNullable()
                    .WithColumn("DocumentID").AsString(50).NotNullable();
            }

            public override void Down()
            {
                Delete.Table("Users");
            }
        }
    }
}