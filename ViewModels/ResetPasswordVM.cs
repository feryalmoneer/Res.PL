using System.ComponentModel.DataAnnotations;

namespace Re.PL.ViewModels
{
    public class ResetPasswordVM
    {
       
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }





    }
}
