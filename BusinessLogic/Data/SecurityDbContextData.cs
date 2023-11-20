using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SecurityDbContextData
    {
       
        public static async Task SeedUserAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    DisplayName = "dmunoz",
                    Email = "dmunozdelrio",
                    UserName = "dmunozdelrio",
                    Address = new Address
                    {
                        
                        Street = "Los madrigales",
                        City = "Nindirí",
                        State = "Masaya",
                        Zipcode = "28001",
                        UserId = "1"
                    },

                    AddressId = 1




                };

                var reponse = await userManager.CreateAsync(user, "Root8080$$%%");

                if (reponse.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");

             
                }



                else
                {
                    throw new Exception(reponse.Errors.ElementAt(0).Description);
                }


                    




          
            
        }
    }
        }
    }

