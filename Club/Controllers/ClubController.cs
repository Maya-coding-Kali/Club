using Microsoft.AspNetCore.Mvc;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using Club.Models;

namespace Club.Controllers
{
    public class ClubController : Controller
    {
        // in Program.cs you will find this 
        //    app.MapControllerRoute(
        //        name: "default",
        //pattern: "{controller=Home}/{action=Index}/{id?}");
        // change controller=Home to controller=Club

      
        public IActionResult Index()
        {
            // right click on Index() ^ above and add view
            // select option 2 Raser View -  this will do alot of the heavy lifting
            // For the first time:            
            //view name =  index
            //template = List
            //model class = ClubInfo (Club.models)
            //Do this 4 more times with Edit, Create, Details, Delete
            //change the view name To Edit, Create, Details, Delete
            // change the template name To Edit, Create, Details, Delete
            return View(GetInfo());
            // add GetInfo() ^^ as a parameter to the the return View() when you make the function;
        }
        // in the club folder go to index.cshtml change primary key to ID
        // @Html.ActionLink("Edit", "Edit", new {  /*id=item.PRIMARYKEY*/  }) |
        //@Html.ActionLink("Details", "Details", new { /* id=item.PRIMARYKEY*/  }) |
        //        @Html.ActionLink("Delete", "Delete", new {  /*id=item.PRIMARYKEY*/  })
        [HttpGet]
        // the purpose of [HTTPGET] is to get database data and pass it to the view
        public List<ClubInfo> GetInfo()
        {
            //First we need to create an empty list that will hold Club Member Objects
            //then a connection object with our connection string
            //open connection string
            // use a SQLcommand to make an sql statement
            //Use data reader to read
            //add all the member details to an object
            // add each object to a list
            //return the list 
            List<ClubInfo> listObj = new List<ClubInfo>();
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-STPU0PM\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True");
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand("Select * FROM club", conn);
            //SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            SqlDataReader sdr = sqlCommand.ExecuteReader();
            while (sdr.Read())
            {
                ClubInfo clubInfo = new ClubInfo();
                clubInfo.ID = Convert.ToInt32(sdr["ID"]);
                clubInfo.FirstName = sdr["FirstName"].ToString();
                clubInfo.LastName = sdr["LastName"].ToString();
                clubInfo.DOB = sdr["DOB"].ToString();
                clubInfo.Rank = sdr["Rank"].ToString();
                listObj.Add(clubInfo);
            }
            return listObj;
        }
        [HttpGet]
        public ActionResult Create()
        {
            // this returns our View() for create, make sure to return View()
            return View();
        }
        [HttpPost]
        public ActionResult Create(ClubInfo Memeber)
        {
            //now we are on the create page we will beable to input the member we want to add
            //Make a connection object with our connection string
            //open connection string
            // use a SQLcommand to make an sql statement
            //Execute your query
            //redirect to /club
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-STPU0PM\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True");
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand($"INSERT INTO club (ID, FirstName,LastName, DOB, Rank) VALUES ('{Memeber.ID}', '{Memeber.FirstName}','{Memeber.FirstName}', '{Memeber.DOB}', '{Memeber.Rank}')", conn);
            sqlCommand.ExecuteNonQuery();
            return Redirect("/Club");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // this is the same idea as the first create function , just a different purpose.
            // we get the edit view so we can see the page (this is done by [HTTPGET])
            // then we need to make a member object
            //then a connection object
            //then open the connection
            //now make a Command with a query to find the data of the member you want to edit
            // Now use a data reader to read the data
            // put the data into our member object
            //now return the View with your member profile
            // When you run your program you will see the input fields filled with your member data
            ClubInfo memberProfile = new ClubInfo();
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-STPU0PM\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True");
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM club WHERE ID = {id}", conn);
            SqlDataReader DataRead = sqlCommand.ExecuteReader();
            while (DataRead.Read())
            {
                memberProfile.ID = Convert.ToInt32(DataRead["ID"]);
                memberProfile.FirstName = DataRead["FirstName"].ToString();
                memberProfile.LastName = DataRead["LastName"].ToString();
                memberProfile.Rank = DataRead["Rank"].ToString();
                memberProfile.DOB = DataRead["DOB"].ToString();
            }
            return View(memberProfile);
        }
        [HttpPut]
        public ActionResult Edit([FromBody] ClubInfo Member)
        {
          
            //then a connection object
            //then open the connection
            //now make a Command with a query to find the data of the member you want to edit
            // Now use a data reader to read the data
            // Execte query
            // redirect to /Club
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-STPU0PM\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True");
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand($"UPDATE club SET FirstName = '{Member.FirstName}', LastName = '{Member.LastName}', DOB = '{Member.DOB}', Rank = '{Member.Rank}' where ID = {Member.ID}", conn);
             sqlCommand.ExecuteNonQuery();
            return Redirect("/Club");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            // this is the same idea as the first edit function, But now we just want to delete.
            // we get the Delete view so we can see the page (this is done by [HTTPGET])
            // then we need to make a member object
            //then a connection object
            //then open the connection
            //now make a Command with a query to find the data of the member you want to delete
            // Now use a data reader to read the data
            // put the data into our member object
            //now return the View with your member profile
            // When you run your program you will see the input fields filled with your member data
            ClubInfo memberProfile = new ClubInfo();
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-STPU0PM\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True");
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM club WHERE ID = {id}", conn);
            SqlDataReader DataRead = sqlCommand.ExecuteReader();
            while (DataRead.Read())
            {
                memberProfile.ID = Convert.ToInt32(DataRead["ID"]);
                memberProfile.FirstName = DataRead["FirstName"].ToString();
                memberProfile.LastName = DataRead["LastName"].ToString();
                memberProfile.Rank = DataRead["Rank"].ToString();
                memberProfile.DOB = DataRead["DOB"].ToString();
            }
            return View(memberProfile);
        }
        [HttpDelete]
        public ActionResult Delete(ClubInfo Member)
        {
            //then a connection object
            //then open the connection
            //now make a Command with a query to find the data of the member you want to Delete
            // Now use a data reader to read the data
            // Execte query
            // redirect to /Club
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-STPU0PM\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True");
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand($"DELETE FROM club WHERE ID = {Member.ID}", conn);
            sqlCommand.ExecuteNonQuery();

            return Redirect("/Club");           
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            // this is the same idea as the first edit function, But now we just want to Details.
            // we get the Details view so we can see the page (this is done by [HTTPGET])
            // then we need to make a member object
            //then a connection object
            //then open the connection
            //now make a Command with a query to find the data of the member you want to See
            // Now use a data reader to read the data
            // put the data into our member object
            //now return the View with your member profile
            // When you run your program you will see the your member data
            ClubInfo memberProfile = new ClubInfo();
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-STPU0PM\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True");
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM club WHERE ID = {id}", conn);
            SqlDataReader DataRead = sqlCommand.ExecuteReader();
            while (DataRead.Read())
            {
                memberProfile.ID = Convert.ToInt32(DataRead["ID"]);
                memberProfile.FirstName = DataRead["FirstName"].ToString();
                memberProfile.LastName = DataRead["LastName"].ToString();
                memberProfile.Rank = DataRead["Rank"].ToString();
                memberProfile.DOB = DataRead["DOB"].ToString();
            }
            return View(memberProfile);
        }
        // we only need the one function for details becuse we wont be altering the data, just looking
    }
}
