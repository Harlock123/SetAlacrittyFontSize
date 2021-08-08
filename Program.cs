using System;
using System.IO;
using System.Text;

namespace SetAlacrittyFontSize
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;  
            string line;  
            bool foundfont =  false;


            StringBuilder sb = new StringBuilder();

            System.IO.StreamReader file = 
            new System.IO.StreamReader(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) 
                + @"/alacritty/alacritty.yml");  
            
            while((line = file.ReadLine()) != null)  
            {  
                if (line.Trim().ToUpper().StartsWith("FONT:"))
                {
                    foundfont = true;
                }

                if (foundfont && line.Trim().ToUpper().StartsWith("SIZE:"))
                {
                    // here we will replace the SIZE with our passed in arg

                    string[] arr = line.Split(":");
                    sb.Append(arr[0]);
                    sb.Append(": ");
                    sb.Append(args[0]);
                    sb.Append("\n");

                    foundfont = false;
                    
                }
                else
                {
                    sb.Append(line + "\n");
                }

                //System.Console.WriteLine(line);  
                counter++;  
            } 

            file.Close(); 

            //System.Console.WriteLine("----------------------------------------------");
            //System.Console.WriteLine("-- New YML files contents after parsing     --");
            //System.Console.WriteLine("----------------------------------------------");
            //System.Console.WriteLine("");

            //System.Console.Write(sb.ToString());
            //System.Console.WriteLine("");

            // Here we will write Out or YML config file

            StreamWriter str = 
                new StreamWriter(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData) 
                + @"/alacritty/alacritty.yml",false);

            str.Write(sb.ToString());

            str.Close();
            str.Dispose();
            
            //Console.WriteLine("Hello World!");
        }
    }
}
