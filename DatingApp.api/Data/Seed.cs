using System.Collections.Generic;
using System.Linq;
using DatingApp.api.Models;
using Newtonsoft.Json;

namespace DatingApp.api.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context){
            if (!context.Users.Any()){
                var userData=System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users=JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users){
                    byte[] passwordHash,passwordSalt;
                    CreateHashedPassword("password",out passwordSalt,out passwordHash);

                    user.PasswordHash=passwordHash;
                    user.PasswordSalt=passwordSalt;
                    user.UserName=user.UserName.ToLower();
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }

        private static void CreateHashedPassword(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512()){
                passwordSalt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}