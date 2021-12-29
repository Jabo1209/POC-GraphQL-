using GraphQLNetExample.Infoes;
using Microsoft.EntityFrameworkCore;

namespace GraphQLNetExample.EntityFramework
{
    public class UserInfoesContext : DbContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }

        public UserInfoesContext(DbContextOptions options) : base(options)
        {

        }
    }
}
