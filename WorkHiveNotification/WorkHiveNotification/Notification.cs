using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace WorkHiveNotification
{
    public class Notification
    {
        string connectionString = "";

        public void LoadData()
        {
            try
            {
                string connectionString = ""; //add connectionstring here
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT * from jobs";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);
                      
                    }
                }
                string html = "";
                foreach (DataRow row in  dataTable.AsEnumerable())
                {

                    string jobTitle = row["Title"].ToString();
                    html += "<a href='#'>" + jobTitle + "</a><br>";
                    break;
                }
                string emailBody = "Dear User. New job/s have been posted in WorkHive<br><br>";
                    emailBody = emailBody + html;
                List<string> users = new List<string>();
                SendEmail(emailBody, users);
            }
            catch (Exception ex)
            {

            }
        }
        public static void SendEmail(string htmlString, List<string> users)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("");//add your email here
                message.Subject = "New Job Notification";
                message.IsBodyHtml = true; //to make message body as html
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("", "");//add credentials here
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
               
                foreach(var email in users)
                {
                    message.To.Add(new MailAddress(email));
                    smtp.Send(message);
                }
             
            }
            catch (Exception ex) { 
            }
        }
    }
}
