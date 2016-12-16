using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KillerApp.Models.Data_Access
{
    public class MssqlScheldwoordLogic : IScheldwoordServices
    {
        //Connectiestring met database
        //private const string Connectie =
        //    "Server=mssql.fhict.local;Database=dbi347556;User Id=dbi347556;Password=Qwerty1";

        private const string Connectie =
            "Server=MSI;Database=KillerApp;Trusted_Connection=Yes;";

        // Haalt een lijst met alle scheldwoorden op
        public List<Scheldwoord> ListScheldwoorden()
        {
            using (SqlConnection conn = new SqlConnection(Connectie))
            {
                List<Scheldwoord> scheldwoorden = new List<Scheldwoord>();

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "Select * From Scheldwoord";
                            cmd.Connection = conn;

                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                string woord = reader.GetString(1);

                                scheldwoorden.Add(new Scheldwoord(woord));
                            }
                            return scheldwoorden;
                        }
                        catch (Exception ex)
                        {

                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                return null;
            }
        }

        public void AddScheldwoord(Scheldwoord scheldwoord)
        {
            using (SqlConnection conn = new SqlConnection(Connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "INSERT INTO Scheldwoord (Woord) VALUES (@woord)";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@woord", scheldwoord.Woord);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        public void RemoveScheldwoord(string scheldwoord)
        {
            using (SqlConnection conn = new SqlConnection(Connectie))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.CommandText = "DELETE FROM Scheldwoord WHERE Woord = @woord";
                            cmd.Connection = conn;

                            cmd.Parameters.AddWithValue("@woord", scheldwoord);

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }
    }
}