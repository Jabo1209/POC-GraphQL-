using GraphQL;
using GraphQL.Types;
using GraphQLNetExample.Commons;
using GraphQLNetExample.EntityFramework;

namespace GraphQLNetExample.Infoes;

public class UserInfoesQuery : ObjectGraphType
{
    public UserInfoesQuery()
    {
        Field<UserInfoType>(
            "login",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "account" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" }
            ),
            resolve: context =>
            {
                var userinfoesContext = context.RequestServices.GetRequiredService<UserInfoesContext>();
                var info = new UserInfo
                {
                    Email = context.GetArgument<string>("account"),
                    Password = context.GetArgument<string>("password")
                };

                if (Helper.IsNotValidEmail(info.Email))
                {
                    info.Email = "Not a valid Email";
                    info.Password = "Error";
                    return info;
                }

                if (string.IsNullOrWhiteSpace(info.Password))
                {
                    info.Email = "Error";
                    info.Password = "Please Enter Password";
                    return info;
                }

                var accounts = from a in userinfoesContext.UserInfo
                               select a;
                var passwords = from p in userinfoesContext.UserInfo
                                where p.Email == info.Email
                                select p;
                accounts = accounts.Where(s => s.Email.Contains(info.Email));
                passwords = passwords.Where(p => p.Password.Contains(info.Password));
                if (accounts.Any() && passwords.Any())
                {
                    info.Email = "Login Successfully";
                    info.Password = "Login Successfully";
                    return info;
                }
                else if (accounts.Any() && !passwords.Any())
                {
                    info.Email = "Error";
                    info.Password = "Password Is Incorrect";
                    return info;
                }
                else
                {
                    info.Email = "Not Found Email";
                    info.Password = "Error";
                    return info;
                }
            });

        Field<UserInfoType>(
                "search",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "account" }
                ),
                resolve: context =>
                {
                    var userinfoesContext = context.RequestServices.GetRequiredService<UserInfoesContext>();

                    var info = new UserInfo
                    {
                        Email = context.GetArgument<string>("account"),
                    };

                    var accounts = from a in userinfoesContext.UserInfo
                                    select a;

                    var passwords = from p in userinfoesContext.UserInfo
                                    where p.Email == info.Email
                                    select p;
                    accounts = accounts.Where(s => s.Email.Contains(info.Email));
         
                    if (accounts.Any())
                    {
                        info.ID = passwords.FirstOrDefault().ID;
                        info.Password= passwords.FirstOrDefault().Password;
                        return info;
                    }
                    else
                    {
                        info.Email = "Not Found Email";
                        info.Password= "Error";
                        return info;
                    }
              });
    }

}