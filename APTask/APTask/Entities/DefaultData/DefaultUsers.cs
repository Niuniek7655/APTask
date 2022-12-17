using System.Collections.Generic;
using System;

namespace APTask.Entities.DefaultData
{
    internal static class DefaultUsers
    {
        internal static readonly List<User> Users = new List<User>()
        {
            new User()
            {
                Id = new Guid("5704a778-34ef-43c2-8582-ca6a841af3c3"),
                Name = "User1"
            },

            new User()
            {
                Id = new Guid("7f04e832-6561-4e32-80ee-9a941d819dcf"),
                Name = "User2"
            },

            new User()
            {
                Id = new Guid("b47c8732-3148-4b35-8dab-fc60188ef082"),
                Name = "User3"
            },

            new User()
            {
                Id = new Guid("525add4f-d2ab-4990-8282-59cc12b39708"),
                Name = "User4"
            }
        };
    }
}
