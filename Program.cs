using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Customer_Management
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CRUDMETHODS cm= new CRUDMETHODS();

            Console.WriteLine("please select a no. to display ");



            Console.WriteLine(1 + ": Please insert the data into the database table ");

            Console.WriteLine(2 + ": Please update the data by entering a no. ");

            Console.WriteLine(3 + ": Please delete the data by enetring a no. ");

            Console.WriteLine(4 + ": display the data by using no. ");

            Console.WriteLine(5 + ": display all the data from table ");

            Console.WriteLine(6 + ": to enter the data from data_table to file ");

            Console.WriteLine(7 + ": delete the file created ");


            int n = Convert.ToInt32(Console.ReadLine());

            switch (n)
            {
                case 1:
                    {
                        cm.Insert();
                        break;
                    }
                case 2:
                    {
                        cm.Update();
                        break;
                    }
                case 3:
                    {
                        cm.Delete();
                        break;
                    }
                case 4:
                    {
                        cm.Get_Single();
                        break;
                    }
                case 5:
                    {
                        cm.Getting_All_Data();
                        break;
                    }
                case 6:
                    {
                        cm.SaveDataFile();
                        Console.WriteLine("data file created");
                        break;
                    }
                case 7:
                    {
                        cm.DeleteFile();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid output");
                        break;
                    }
            }

        }
    }
}
