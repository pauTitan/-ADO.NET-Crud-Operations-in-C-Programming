using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_Customer_Management
{
    internal class CRUDMETHODS
    {

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter ad;

        string str = "Server=EMBERPC-126;Database=ABC_Bank;Integrated Security=SSPI";



    public CRUDMETHODS()
        {
            try
            {

                conn = new SqlConnection(str);
                conn.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Insert()
        {
            Console.WriteLine("welcome to the bank to insert");

            Console.WriteLine("enter your name to insert");
            string name = Convert.ToString(Console.ReadLine());

            Console.WriteLine("enter your acc_id to insert");
            string acc_id = Convert.ToString(Console.ReadLine());

            Console.WriteLine("enter your branch_id to insert");
            string branch_id = Convert.ToString(Console.ReadLine());

            Console.WriteLine("enter your aadh_id to insert");
            int aadh_id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter your balance to insert");
            int bal = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter your country to insert");
            string country = Convert.ToString(Console.ReadLine());

            try
            {
                cmd = new SqlCommand("USP_INSERT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@acc_id", acc_id);
                cmd.Parameters.AddWithValue("@branch_id", branch_id);
                cmd.Parameters.AddWithValue("@adhar_card", aadh_id);
                cmd.Parameters.AddWithValue("@balance", bal);
                cmd.Parameters.AddWithValue("@country", country);

                int x = cmd.ExecuteNonQuery();
                MessageBox.Show(x + ": data inserted");
                conn.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public void Update()
        {
            Console.WriteLine("welcome to the bank to update");

            Console.WriteLine("enter your aadhar to update");
            int aadhar = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter your bal to update");
            float bal = Convert.ToSingle(Console.ReadLine());

            try
            {
                cmd = new SqlCommand("USP_UPDATE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@adhar_no", aadhar);
                cmd.Parameters.AddWithValue("balance", bal);

                int x = cmd.ExecuteNonQuery();
                MessageBox.Show(x + ": data updated");
                conn.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void Get_Single()
        {
                Console.WriteLine("welcome to the bank to get single data");

                Console.WriteLine("enter your aadhar to get data");
                int aadhar = Convert.ToInt32(Console.ReadLine());

            try
            {

                ad = new SqlDataAdapter("GET_SINGLE", str);
                ad.SelectCommand.CommandType = CommandType.StoredProcedure;

                ad.SelectCommand.Parameters.AddWithValue("@adhar_no", aadhar);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine("Customer Details = " + dr[0]);
                    Console.WriteLine(dr[1]);
                    Console.WriteLine(dr[2]);
                    Console.WriteLine(dr[3]);
                    Console.WriteLine(dr[4]);
                    Console.WriteLine(dr[5]);
                    Console.WriteLine(dr[6]);
                    Console.WriteLine(dr[7]);
                    Console.WriteLine("--------------------------------------------------");
                }
                MessageBox.Show("single data fetched");
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void Delete()
        {
            Console.WriteLine("welcome to the bank to delete details");

            Console.WriteLine("enter your aadhar to delete");
            int aadhar = Convert.ToInt32(Console.ReadLine());

            try
            {
                cmd = new SqlCommand("USP_DELETE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@adhar_no", aadhar);

                int x = cmd.ExecuteNonQuery();
                MessageBox.Show(x + ": date deleted");
                conn.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Getting_All_Data()
        {
            Console.WriteLine("welcome to the bank to get all details");

            try
            {
                ad = new SqlDataAdapter("USP_DETAILS", conn);
                ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                ad.Fill(ds, "CUSTOMER_INFO");

                foreach (DataRow dr in ds.Tables["CUSTOMER_INFO"].Rows)
                {
                    Console.WriteLine("CUSTOMER DETAILS = " + dr[0]);
                    Console.WriteLine(dr[1]);
                    Console.WriteLine(dr[2]);
                    Console.WriteLine(dr[3]);
                    Console.WriteLine(dr[4]);
                    Console.WriteLine(dr[5]);
                    Console.WriteLine(dr[6]);
                    Console.WriteLine(dr[7]);
                    Console.WriteLine("--------------------------------------------------");
                }
                MessageBox.Show("all data fetched");
                conn.Close();
            }
            catch(SqlException ex) 
            { 
               MessageBox.Show(ex.Message);
            }
        }

        public void SaveDataFile()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd");
            string LogFolder = @"C:\DataFile\";

            try
            {

                string FileNamePart = "DataFile";
                string DestinationFolder = @"C:\Files\";
                string TableName = "dbo.CUSTOMER_DETAILS1";
                string FileDelimiter = " | "; 
                string FileExtension = ".txt";


            
                SqlConnection SQLConnection = new SqlConnection();
                SQLConnection.ConnectionString = "Server=EMBERPC-126;Database=ABC_Bank;Integrated Security=SSPI";

         
                string query = "Select * From " + TableName;
                SqlCommand cmd = new SqlCommand(query, SQLConnection);
                SQLConnection.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                SQLConnection.Close();

     
                string FileFullPath = DestinationFolder + "\\" + FileNamePart + "_" + datetime + FileExtension;

                StreamWriter sw = null;
                sw = new StreamWriter(FileFullPath, false);

         
                int ColumnCount = dt.Columns.Count;
                for (int ic = 0; ic < ColumnCount; ic++)
                {
                    sw.Write(dt.Columns[ic]);
                    if (ic < ColumnCount - 1)
                    {
                        sw.Write(FileDelimiter);
                    }
                }
                sw.Write(sw.NewLine);

        
                foreach (DataRow dr in dt.Rows)
                {
                    for (int ir = 0; ir < ColumnCount; ir++)
                    {
                        if (!Convert.IsDBNull(dr[ir]))
                        {
                            sw.Write(dr[ir].ToString());
                        }
                        if (ir < ColumnCount - 1)
                        {
                            sw.Write(FileDelimiter);
                        }
                    }
                    sw.Write(sw.NewLine);

                }

                sw.Close();

            }
            catch (Exception exception)
            {
     
                using (StreamWriter sw = File.CreateText(LogFolder
                    + "\\" + "ErrorLog_" + datetime + ".log"))
                {
                    sw.WriteLine(exception.ToString());

                }

            }
        }

        public void DeleteFile()
        {
            String myfile = @"C:\Files\DataFile_2023-03-08-16-47.txt";

            
            File.Delete(myfile);

     
            Console.WriteLine("Specified file has been deleted");
        }


    }

}

