using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace PhotoImpression
{
    class SQLiteDatabase
    {
        private string _database = "Data/data.sqlite";
        private SQLiteConnection connecction;

        private string GetSHA1HashData(byte[] data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(data);

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }

        public SQLiteDatabase()
        {
            connecction = new SQLiteConnection("Data Source=Data/data.sqlite;Version=3;");
            // check for Data existence
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }
            // check for database
            if (!File.Exists(this._database))
            {
                Console.WriteLine("Creating new database file...");
                SQLiteConnection.CreateFile(_database);
                string sql = "CREATE TABLE photo (id INTEGER PRIMARY KEY, title VARCHAR(200), data BOLB, sha1 VARCHAR(100))";
                connecction.Open();
                SQLiteCommand command = new SQLiteCommand(sql, connecction);
                command.ExecuteNonQuery();
                connecction.Close();
            }
        }

        public Boolean CheckImageHash(string hashedValue)
        {
            connecction.Open();
            string sql = String.Format("SELECT count(id) FROM photo where sha1 = '{0}'", hashedValue);
            Console.WriteLine(sql);
            SQLiteCommand cmd = new SQLiteCommand(sql, connecction);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            Console.WriteLine(count);
            connecction.Close();

            if (count > 0)
                return true;
            return false;
        }

        public void saveImageData(string title, byte[] binaryData) 
        {
            // get hash value
            string hashedValue = this.GetSHA1HashData(binaryData);
            if (this.CheckImageHash(hashedValue))
            {
                MessageBox.Show("Image already exists in database");
                return;
            }
            connecction.Open();
            SQLiteCommand cmd = connecction.CreateCommand();
            cmd.CommandText = String.Format("INSERT INTO photo" +
                                            "(title, data, sha1) VALUES" +
                                            "(@0, @1, @2)");
            SQLiteParameter pTitle = new SQLiteParameter("@0", System.Data.DbType.String);
            pTitle.Value = title;
            cmd.Parameters.Add(pTitle);

            SQLiteParameter pBinary = new SQLiteParameter("@1", System.Data.DbType.Binary);
            pBinary.Value = binaryData;
            cmd.Parameters.Add(pBinary);

            SQLiteParameter pHash = new SQLiteParameter("@2", System.Data.DbType.String);
            pHash.Value = hashedValue;
            cmd.Parameters.Add(pHash);

            cmd.ExecuteNonQuery();
            connecction.Close();

            Console.WriteLine("Saved");
        }
    }
}
