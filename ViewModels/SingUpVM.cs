using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Re.PL.ViewModels
{
    public class SingUpVM
    {
        public string FullName { get; set; }
        public string Email{ get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public string City { get; set; }

    }
}
