using System.ComponentModel.DataAnnotations;

namespace GraphQLNetExample.Infoes;

public class UserInfo
{
    public int ID { get; set; }
    [Required]
    public string IuserAccount { get; set; }
    [Required]
    public string IuserPassword { get; set; }
}