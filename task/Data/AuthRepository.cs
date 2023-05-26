using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using task.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace task.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<Response> register(Person person)
        {

            Response response = new Response();
            try{    

            var exPerson = await checkExistingUser(person.userName);

            if (exPerson == null)
            { 
                if(checkValidation(person.password)){

                person.password = hashPassword(person.password);
                person.address = encryptString(person.address, person.password.Substring(0, 32));
                person.birthdate = encryptString(person.birthdate, person.password.Substring(0, 32));
                await _context.Persons.AddAsync(person);
                await _context.SaveChangesAsync();
                response.success=true;
                response.message="user registered successful";
               
            }
            else {

                response.message= "Password must contain a minimum criteria of 8 characters at least 1 Capital, 1 Small and 1 digit";
                
             }
            }
            else { 
                response.message="User Already Exists";
            }


            }
            catch (Exception e)
            {
                response.message= "System failure";
            }

             return response;
        }

        public async Task<Response> resetUserPassword(passwordReset person )
        {   
            Response response = new Response();
            try{

                
            //check user Existing
            var PersonfromDataBase = await checkExistingUser(person.userName);
            if (PersonfromDataBase == null)
            {
                response.message="User Not Exists";
            }
            else
            {
                //check new password validation
                if (checkValidation(person.newPassword))
                {
                    string _hashedOldPassword = hashPassword(person.oldPassword);
                    if (PersonfromDataBase.password == _hashedOldPassword)
                    {
                        //encrypt data with new password 
                        string _hashedNewPassword = hashPassword(person.newPassword);

                        string addresNewEncreption=decryptString(PersonfromDataBase.address,PersonfromDataBase.password.Substring(0,32));
                        string birthDateNewEncreption=decryptString(PersonfromDataBase.birthdate,PersonfromDataBase.password.Substring(0,32));

                    
                        PersonfromDataBase.address=encryptString(addresNewEncreption,_hashedNewPassword.Substring(0,32));
                        PersonfromDataBase.birthdate=encryptString(birthDateNewEncreption,_hashedNewPassword.Substring(0,32));

                        PersonfromDataBase.password=_hashedNewPassword;
                        _context.Persons.Update(PersonfromDataBase);
                        await _context.SaveChangesAsync();

                        response.success=true;
                        response.message="Password Reseted";
                    }
                    else
                    {
                        response.message="Wrong Password";
                    }


                }
                else
                {
                response.message= "Password must contain a minimum criteria of 8 characters at least 1 Capital, 1 Small and 1 digit";
                }


                
            }
            }
            catch (Exception e)
            {
                response.message=response.message= "System failure";
            }


            return response;
        }
        public async Task<Response> retrieveUserData(userLogin user)
        {
            Response response= new Response();

            try{

                
            var PersonfromDataBase = await checkExistingUser(user.userName);
            
            if (PersonfromDataBase == null)
            {
                response.message="User Not Exist ";
            }
            else
            {

                string _hashedPassword = hashPassword(user.password);
                if (PersonfromDataBase.password == _hashedPassword)
                {   //encryption data 
                   // AES supports 128, 192, and 256 bits key sizes 
                    PersonfromDataBase.address=decryptString(PersonfromDataBase.address,PersonfromDataBase.password.Substring(0, 32));
                    PersonfromDataBase.birthdate=decryptString(PersonfromDataBase.birthdate,PersonfromDataBase.password.Substring(0, 32));        
                    PersonfromDataBase.password = "";

                    response.success=true;
                    response.data=PersonfromDataBase;
                    response.message="Login sucssful";
                }
                else
                {
                    response.message="Wrong Password";
                }

            }
            }catch (Exception e)
            {
                response.message=response.message= e.ToString();
            }

            return response;
        }


        //handel hashing password 
        private string hashPassword(string password)
        {

            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha512.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

            // making encreption using password as a key 
          //AES supports 128, 192, and 256 bits key sizes
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
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
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

        public bool checkValidation(string password){
            if(password.Length>=8){
                if (password.Any(char.IsUpper)){
                    if (password.Any(char.IsLower)){
                        if (password.Any(char.IsDigit)){
                          return  true;
                        }
                    }
                }
            }

            return false;

        }

        public async Task<Person> checkExistingUser(string userName){
            return await _context.Persons.FirstOrDefaultAsync(x => x.userName == userName);
        }
    }
}