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
            arguments:new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "account" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" }
            ),
            resolve: context =>
            {
                var userinfoesContext = context.RequestServices.GetRequiredService<UserInfoesContext>();
                var info = new UserInfo
                {
                    IuserAccount = context.GetArgument<string>("account"),
                    IuserPassword = context.GetArgument<string>("password")
                };

                if (Helper.IsNotValidEmail(info.IuserAccount))
                {
                    info.IuserAccount = "Not a valid Email";
                    info.IuserPassword = "Error";
                    return info;
                }

                if (string.IsNullOrWhiteSpace(info.IuserPassword))
                {
                    info.IuserAccount = "Error";
                    info.IuserPassword = "Please Enter Password";
                    return info;
                }

                var accounts = from a in userinfoesContext.UserInfo
                               select a;
                var passwords = from p in userinfoesContext.UserInfo
                                where p.IuserAccount == info.IuserAccount
                                select p;
                accounts = accounts.Where(s => s.IuserAccount.Contains(info.IuserAccount));
                passwords = passwords.Where(p => p.IuserPassword.Contains(info.IuserPassword));
                if (accounts.Any() && passwords.Any())
                {
                    info.IuserAccount = "Login Successfully";
                    info.IuserPassword = "Login Successfully";
                    return info;
                }
                else if (accounts.Any() && !passwords.Any())
                {
                    info.IuserAccount = "Error";
                    info.IuserPassword = "Password Is Incorrect";
                    return info;
                }
                else
                {
                    info.IuserAccount = "Not Found Email";
                    info.IuserPassword = "Error";
                    return info;
                }
            });
    }

}