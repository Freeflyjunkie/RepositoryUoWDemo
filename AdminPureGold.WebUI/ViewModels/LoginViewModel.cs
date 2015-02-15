using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminPureGold.WebUI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
        [StringLength(25)]
        [DisplayName("User Name:")]
        [UIHint("UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(25)]
        [DisplayName("Password:")]
        [UIHint("Password")]
        public string Password { get; set; }
    }
}