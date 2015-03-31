namespace DietGeeks.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DietGeeks.Data.Contracts;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser, IDeletableEntity, IAuditInfo
    {
        public ApplicationUser()
            : this(string.Empty, string.Empty)
        {
        }

        public ApplicationUser(string username, string email)
            : base(username)
        {
            this.Email = email;
            this.UserSettings = new UserInformation();
            this.UserHistory = new UserHistory();
            this.CreatedOn = DateTime.Now;
        }

        public UserInformation UserSettings { get; set; }

        public UserHistory UserHistory { get; set; }

        public Guid? ForgottenPasswordToken { get; set; }

        #region IDeletableEntity
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
        #endregion

        #region IAuditInfo
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
        #endregion

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            //// Add custom user claims here
            return userIdentity;
        }
    }
}
