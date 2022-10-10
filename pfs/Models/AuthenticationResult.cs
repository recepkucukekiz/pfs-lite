using System.ComponentModel.DataAnnotations;

namespace pfs.Models
{
    public class AuthenticationResult
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        public List<FileModel> Files { get; set; } = new List<FileModel>();
    }
}
