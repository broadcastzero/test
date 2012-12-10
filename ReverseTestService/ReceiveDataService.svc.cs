using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;

namespace ReverseTestService
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
	public class ReceiveDataService : IReceiveDataService
	{
		private static ConnectionStringSettings connString;

		/// <summary>
		/// Static constructor
		/// </summary>
		static ReceiveDataService()
		{
			// Get connection string for SQLServer
			Configuration rootWebConfig = WebConfigurationManager.OpenWebConfiguration("/");

			if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
			{
				connString =
					rootWebConfig.ConnectionStrings.ConnectionStrings["ReverseTestServiceStorage"];
				if (connString != null)
				{
					Debug.WriteLine(connString.ConnectionString);
				}
				else
				{
					Debug.WriteLine("No Northwind connection string");
				}
			}
		}

		public void StoreData(string data)
		{
			// save to database
			using (SqlConnection sqlConnection = new SqlConnection(ReceiveDataService.connString.ToString()))
			{
				try
				{
					string sql = "INSERT INTO ContactData (contactList) values (@data);";
					SqlCommand cmd = new SqlCommand(sql, sqlConnection);
					cmd.Parameters.Add("data", data);

					sqlConnection.Open();
					cmd.ExecuteNonQuery();

					sqlConnection.Close();
				}
				catch (SqlException e)
				{
					Debug.WriteLine(e.ToString());
				}
			}

		}

		public string HelloWorld(string id)
		{
			return "Hello World - you have entered " + id;
		}
	}
}

