namespace OnlineUserToDo.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserToDo")]
    public partial class UserToDo
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public int? Status { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
