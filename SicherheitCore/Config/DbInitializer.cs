﻿using SicherheitCore.Models;
using SicherheitCore.Models.Concret;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SicherheitCore.Repository.SqlConcret
{
    public static class DbInitializer
    {
        public async static void Initialize(SicherheitCoreContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            var users = new List<User>();
            if(!context.Users.Any())
            {
                for (int i = 1; i <= 5; i++)
                {
                    User user = new User
                    {
                        EmailAddress = $"user{i}@testowy.pl",
                        Password = "111111111",
                        Name = $"Testowy user {i}"
                    };
                    users.Add(user);
                }
            }

            var r = new Random();
            var tasks = new List<PlannedTask>();
            if (!context.Tasks.Any())
            for (int i=1; i<=15; i++)
            {
                    var rand = r.Next(0, 4);
                    PlannedTask task = new PlannedTask
                    {
                        Title = $"Task {i}",
                        Priority = TaskPriority.Medium,
                        Deadline = new DateTime(2018, 03, i),
                        State = TaskState.Active,
                        UserId = users.ElementAt(rand).Id
                    };
                    tasks.Add(task);
            }
            await context.Users.AddRangeAsync(users);
            await context.Tasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();


        }
    }
}
