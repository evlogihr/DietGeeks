﻿namespace DietGeeks.Data.Contracts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class DeletableEntity : AuditInfo, IDeletableEntity
    {
        [Display(Name = "Deleted?")]
        [Editable(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "Date of deletion")]
        [Editable(false)]
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}
