using System;
using System.Data.SqlClient;

namespace SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("USAGE: sqlClient.exe <sql.server.com> <database name>");
            }



            String sqlServer = args[0];
            String database = args[1];

            String conString = "Server = " + sqlServer + "; Database = " + database + "; Integrated Security = True;";
            SqlConnection con = new SqlConnection(conString);

            try
            {
                con.Open();
                Console.WriteLine("Auth success!");
            }
            catch
            {
                Console.WriteLine("Auth failed");
                Environment.Exit(0);
            }

            String query = "";
            SqlCommand command;
            SqlDataReader reader;

            while (query != "exit")
            {
                Console.Write("mssql>");
                query = Console.ReadLine();
               
                try
                {
                    command = new SqlCommand(query, con);
                    reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.FieldCount > 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader[i].ToString() + ",");
                        }
                    }
                    else
                    {
                        Console.Write("No data returned");
                    }
                    reader.Close();
                }
                catch
                {
                    Console.WriteLine("Query triggered an error!");
                }
                Console.Write("\n");
                

            }

            

            con.Close();
        }
    }
}