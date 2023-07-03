namespace project1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RolesUsuario")]
    public partial class RolesUsuario
    {
        public int Id { get; set; }

        public int IdRole { get; set; }

        public int IdUser { get; set; }
    }
}
