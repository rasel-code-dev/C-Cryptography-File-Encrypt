using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace c__new_app{
    class Program {
        static void Main(string[] args){
            string inputOption;
            bool isTrue  = true;

            while(isTrue){
                Console.WriteLine("a) Encrypt");
                Console.WriteLine("b) Decrypt");
                Console.WriteLine("c) Exit");

                inputOption = Console.ReadLine();
                if(inputOption == "c"){
                    break;
                } else{
                    if(inputOption == "a"){
                        EncryptFile();
                    } else if (inputOption == "b"){
                        DeryptFile();
                    }
                }

            }
        }

        static void EncryptFile(){
            Console.Write("Give Your File Full Path: ");
            string filePath = Console.ReadLine();
        
            while(true){
                if(filePath.Trim() == ""){
                    Console.WriteLine("File Full Path Should Not be Empty");
                    Console.Write("Give Your File Full Path: ");
                    filePath = Console.ReadLine();
                } else{
                    break;
                }
            }
            
            Console.Write("Password Should be (6 character) : ");
            string key = Console.ReadLine();
             while(true){
                if(key.Trim() == ""){
                    Console.WriteLine("Password Should not be Empty");
                    Console.Write("Password Should be (6 character) : ");
                    key = Console.ReadLine();
                } else{
                    break;
                }
            }

            bool isFileExit = File.Exists(filePath);
            
            if(!isFileExit){
                Console.WriteLine("File Not Found " + filePath);
            } else{
                byte[] plainContent = File.ReadAllBytes(filePath);
                using(var DES = new DESCryptoServiceProvider()){
                    DES.IV = Encoding.UTF8.GetBytes(key);
                    DES.Key = Encoding.UTF8.GetBytes(key);
                    DES.Mode = CipherMode.CBC;
                    DES.Padding = PaddingMode.PKCS7;

                    using (var memStream = new MemoryStream()){
                        CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(), 
                            CryptoStreamMode.Write);

                        cryptoStream.Write(plainContent, 0, plainContent.Length);
                        cryptoStream.FlushFinalBlock();
                        File.WriteAllBytes(filePath, memStream.ToArray());
                        Console.WriteLine("File Encrypted Succssfully ",  filePath);
                    }
                }
            }
        }

        static void DeryptFile(){

            Console.Write("Give Your Encrypted File Full Path: ");
            string filePath = Console.ReadLine();
        
            while(true){
                if(filePath.Trim() == ""){
                    Console.WriteLine("File Full Path Should Not be Empty");
                    Console.Write("Give Your Encrypted File Full Path: ");
                    filePath = Console.ReadLine();
                } else{
                    break;
                }
            }
            
            Console.Write("Password Should be (6 character) : ");
            string key = Console.ReadLine();
             while(true){
                if(key.Trim() == ""){
                    Console.WriteLine("Password Should not be Empty");
                    Console.Write("Password Should be (6 character) : ");
                    key = Console.ReadLine();
                } else{
                    break;
                }
            }

            bool isFileExit = File.Exists(filePath);
            
            if(!isFileExit){
                Console.WriteLine("File Not Found " + filePath);
            } else{

                byte[] encrypted = File.ReadAllBytes(filePath);
                using(var DES = new DESCryptoServiceProvider()){
                    DES.IV = Encoding.UTF8.GetBytes(key);
                    DES.Key = Encoding.UTF8.GetBytes(key);
                    DES.Mode = CipherMode.CBC;
                    DES.Padding = PaddingMode.PKCS7;

                    using (var memStream = new MemoryStream()){
                        CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor() , 
                            CryptoStreamMode.Write);

                        cryptoStream.Write(encrypted, 0, encrypted.Length);
                        cryptoStream.FlushFinalBlock();
                        File.WriteAllBytes(filePath, memStream.ToArray());
                        Console.WriteLine("File Decrypted Succssfully ",  filePath);
                    }

                }
            }
        }

    }
}




