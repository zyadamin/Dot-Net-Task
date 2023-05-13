using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using task.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace task.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<Person> register(Person person)
        {
            var exPerson = await _context.Persons.FirstOrDefaultAsync(x=>x.userName==person.userName);
            if(exPerson==null){

            person.password=hashPassword(person.password);
           // person.address=encryptString(person.address,person.password);
           // person.birthdate=encryptString(person.birthdate,person.password);
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }
        else{return null;}
        }

        public async Task<bool> resetUserPassword(Person person)
        {
            var exPerson = await _context.Persons.FirstOrDefaultAsync(x=>x.userName==person.userName);
            if(exPerson==null){
                return false;
            }
            else{
            
            string _hashedOldPassword = hashPassword(person.password);
            if(exPerson.password==_hashedOldPassword){
                string _hashedNewPassword=hashPassword(person.newPassword);
                exPerson.password=_hashedNewPassword; 
                exPerson.newPassword="";
                _context.Persons.Update(exPerson);
                await _context.SaveChangesAsync(); 
                return true;
            }
            else{
                return false;
            }

        }

        }
        public  async Task<Person> retrieveUserData(Person person)
        {   
            
            var exPerson = await _context.Persons.FirstOrDefaultAsync(x=>x.userName==person.userName);
            if(person==null){
                return null;
            }
            else{

            string _hashedPassword = hashPassword(person.password);
            if(exPerson.password==_hashedPassword){
                
               // person.address=decryptString(person.address,exPerson.password);
               // person.birthdate=decryptString(person.birthdate,exPerson.password);        
                exPerson.password="";
                return exPerson;
            }
            else{
                return null;
            }

            }
            
        }


        private string hashPassword(string password){

            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha512.ComputeHash(bytes);
        
            return Convert.ToBase64String(hash);
        }
    

        public static string encryptString(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public static string decryptString(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
                       }
                }
        }
}