using System.ComponentModel.DataAnnotations;

namespace GraphQLNetExample.Infoes;

public class UserInfo
{
    public int ID { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}