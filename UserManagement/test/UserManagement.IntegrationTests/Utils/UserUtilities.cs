using System;
using System.Collections.Generic;
using UserManagement.Common.Models;
using UserManagement.Repository.Context;

namespace UserManagement.IntegrationTests.Utils;

public static class UserUtilities
{
    public static void InitializeUsersForTests(UserManagementContext ctx)
    {
        ctx.Users.AddRange(new List<UserModel>()
        {
            new UserModel()
            {
                Id = new Guid("E2359490-072F-4DFA-9992-D7FB70424A57"),
                Name = "Bob",
                Lastname = "Sanders",
                Email = "Bob.sanders@yopmail.com",
                UserIdentifier = "bob.sanders@yopmail.com",
                Description = "I am the best",
                DateCreated = new DateTime(2022,10,10),
                IsDeleted = false
            },
            new UserModel()
            {
                Id = new Guid("C2EF2A0A-0EAF-48EC-B91F-AC26BF32CBAA"),
                Name = "Tom",
                Lastname = "Sanders",
                Email = "tom.sanders@yopmail.com",
                UserIdentifier = "tom.sanders@yopmail.com",
                Description = "I am the second best",
                DateCreated = new DateTime(2022,10,10),
                IsDeleted = false
            },
            new UserModel()
            {
                Id = new Guid("3B73EE57-B27A-4FDD-9AEB-9E1B62831429"),
                Name = "Mike",
                Lastname = "du Plooy",
                Email = "michaeldp@yopmail.com",
                UserIdentifier = "michaeldp@yopmail.com",
                Description = "I am a dev",
                DateCreated = new DateTime(2022,10,10),
                IsDeleted = false
            },
            new UserModel()
            {
                Id = new Guid("8E30D0DC-78F6-4ED6-8C3C-C07FAF142899"),
                Name = "Tim",
                Lastname = "Nook",
                Email = "tnook@yopmail.com",
                UserIdentifier = "tnook@yopmail.com",
                Description = "I am the QA",
                DateCreated = new DateTime(2022,10,10),
                IsDeleted = false
            }
        });

        ctx.SaveChanges();
    }
}