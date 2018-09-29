using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Tools
{
    public class CryptoTools
    {
        #region Fields


        private static byte[] key = { };
        private static byte[] IV = { 38, 55, 206, 48, 28, 64, 20, 16 }; //{ 238, 52, 226, 148, 18, 64, 20, 16 }; // 
        private static string stringKey = "!1234a#KN"; // "!5663a#KN";

        #endregion

        #region Public Methods

        public static byte[] String_To_Bytes2(string strInput)
        {
            /* 
             * Asignar matriz de bytes en base a la mitad de la longitud de cadena.
             */
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];
            /*
             * En el bucle de 0 a numBytes, se recupera una subcadena de la instancia.  La subcadena comienza en la
             * posición de carácter x * 2 y tiene una longitud 2. Luego se convierte a byte en base 16 y se almacena 
             * en la posicion x del array de bytes.
             */
            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }

            // return the finished byte array of decimal values
            return bytes;
        }

        //Bytes_To_String on the other hand, is more of a mess:

        // convert the byte array back to a true string
        public static string Bytes_To_String2(byte[] bytes_Input)
        {
            /*
             * StringBuilder representa una cadena de caracteres modificable instanciada con la capacidad
             * del doble de la longitud de bytes_Input.
             * En el bucle le anexa a dicha cadena la representacion equivalente a string del byte en formato
             * "X02". (El parámetro format puede ser un estándar o una cadena de formato numérico personalizado)
             */
            StringBuilder strTemp = new StringBuilder(bytes_Input.Length * 2);
            foreach (byte b in bytes_Input)
            {
                strTemp.Append(b.ToString("X02"));
            }
            return strTemp.ToString();
        } 


        public static string Encrypt(string text)
        {
            try
            {
                /*
                 * private static string stringKey = "!1234a#KN";
                 * private static byte[] key = { };
                 * private static byte[] IV = { 38, 55, 206, 48, 28, 64, 20, 16 };
                 * 
                 * Se obtiene una codificacion para el formato UTF8 de una matriz de bytes correspondiente
                 * a la subcadena de los 8 primeros caracteres de stringKey para generar una clave.
                 * Una vez generada la clave, obtenemos acceso a la versión del proveedor de servicios 
                 * criptográficos (CSP) del algoritmo Estándar de cifrado de datos (DES).
                 */
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                Byte[] byteArray = Encoding.UTF8.GetBytes(text);

                MemoryStream memoryStream = new MemoryStream();
                /*
                 * Inicializa una nueva instancia de la clase CryptoStream con un flujo de datos de destino 
                 * (memoryStream), la transformación criptografica que se va a realizar en la secuencia 
                 * (des.CreateEncryptor(key, IV)) y el modo de la secuencia (escritura o lectura).
                 * "CreateEncryptor()" crea un objeto descifrador simétrico con la propiedad Key (clave) 
                 * y el vector de inicialización (IV) actuales. (Cualquiera de los dos parametros pasados
                 * se pueden generar de modo aleatorio con "GenerateKey" y "GenerateIV"
                 */
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                /*
                 * Escribe una secuencia de bytes (byteArray) y hace avanzar la posición actual dentro de la 
                 * secuencia en el número de bytes escritos (de 0 a la longitud del array).
                 */
                cryptoStream.Write(byteArray, 0, byteArray.Length);
                /*
                 * Actualiza el origen de datos o repositorio subyacente con el estado actual del 
                 * búfer y, posteriormente, borra el búfer. 
                 */
                cryptoStream.FlushFinalBlock();
                /*
                 * Convierte una matriz de enteros de 8 bits sin signo en su representación de cadena 
                 * equivalente, que se codifica con dígitos de base 64.  
                 */
                string step = Convert.ToBase64String(memoryStream.ToArray());

                return CryptoTools.Bytes_To_String2(Encoding.UTF8.GetBytes(step));

            }
            catch (Exception ex)
            {

                // Handle Exception Here

            }
            return string.Empty;

        }

        /*
         * Pasos contrarios a la encriptacion de un text
         */
        public static string Decrypt(string text)
        {
            try
            {
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                Byte[] nba = String_To_Bytes2(text);
                string ukey = Encoding.UTF8.GetString(nba);


                Byte[] byteArray = Convert.FromBase64String(ukey); //text);

                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    des.CreateDecryptor(key, IV), CryptoStreamMode.Write);

                cryptoStream.Write(byteArray, 0, byteArray.Length);

                cryptoStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(memoryStream.ToArray());

            }
            catch (Exception ex)
            {

                // Handle Exception Here

            }

            return string.Empty;

        }

        #endregion
    }

}