using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; //required for DbContexts

//A DbContext is the setup/connection to 
//a database proper so that our model will
//match with database rows and columns

namespace logincsharp.Models
{
    //Declare a LoginContext class and inherit
    //from DbContext
    public class LoginContext : DbContext
    {
        //Create a constructor to load
        //Context options of type 
        //DbContextOptions object type
        //but of our class type LoginContext
        //inherit from base class options
        public LoginContext(DbContextOptions<LoginContext> options)
            : base(options)
        {
        }

        //We have one method, Users, which has
        //getters and setters in the DbSet of type
        //UserData (our model class)
        public DbSet<UserData> Users { get; set; } = null!;
    }
}