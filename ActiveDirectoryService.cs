using Microsoft.AspNetCore.Authentication;
using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

public class ActiveDirectoryService
{
    private readonly string _domain;

    public ActiveDirectoryService(string domain)
    {
        _domain = domain;
    }

    public bool ValidateUser(string username, string password)
    {
        try
        {
            using (var context = new PrincipalContext(ContextType.Domain, _domain))
            {
                return context.ValidateCredentials(username, password);
            }
        }
        catch (Exception ex)
        {
            // Handle exception (log it, rethrow it, etc.)
            return false;
        }
    }

    public UserPrincipal FindUser(string username)
    {
        using (var context = new PrincipalContext(ContextType.Domain, _domain))
        using (var searcher = new PrincipalSearcher(new UserPrincipal(context) { SamAccountName = username }))
        {
            return searcher.FindOne() as UserPrincipal;
        }
    }
}
