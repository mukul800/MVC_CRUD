using MVC_Crud.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Text;
using static MVC_Crud.BLL.parameters;

namespace MVC_Crud.DAL
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<StudentModel>> GetStudents()
        {
            List<StudentModel> students = new List<StudentModel>();
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                string sqlStr = "Select * from students;";
                using (SqlCommand cmd = new SqlCommand(sqlStr, sqlCon))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            students.Add(new StudentModel
                            {
                                id = reader.GetInt32(0),
                                name = reader.GetString(1),
                                age = reader.GetInt32(2)
                            });
                        }
                    }
                }
                sqlCon.Close();
            }
            return students;
        }
        public async Task<StudentModel> GetStudentById(int id)
        {
            StudentModel student = new StudentModel();
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                string sqlStr = "select * from students where id = @id;";
                sqlParam.Clear();
                sqlParam.Add(new SqlParameter("@id", id));

                using (SqlCommand cmd = new SqlCommand(sqlStr, sqlCon))
                {
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                    }
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            student.id = reader.GetInt32(0);
                            student.name = reader.GetString(1);
                            student.age = reader.GetInt32(2);
                        }
                    }
                }
                sqlCon.Close();
            }
            return student;
        }
        public int Insert(StudentModel objParams)
        {
            int res = 0;
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            { 
                sqlCon.Open();
                string sqlStr = "insert into students (name, age) values(@name,@age);";
                sqlParam.Clear();
                sqlParam.Add(new SqlParameter("@name",objParams.name));
                sqlParam.Add(new SqlParameter("@age",objParams.age));
                using (SqlCommand cmd = new SqlCommand(sqlStr, sqlCon)) 
                { 
                    foreach(SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                    res = cmd.ExecuteNonQuery();
                }
            }
            return res;
        }
        public int Edit(StudentModel objParams)
        {
            int res = 0;
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                string sqlStr = "update students set name = @name, age = @age where id = @id;";
                sqlParam.Clear();
                sqlParam.Add(new SqlParameter("@id", objParams.id));
                sqlParam.Add(new SqlParameter("@name", objParams.name));
                sqlParam.Add(new SqlParameter("@age", objParams.age));

                using (SqlCommand cmd = new SqlCommand(sqlStr, sqlCon))
                {
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                    res = cmd.ExecuteNonQuery();
                }
                sqlCon.Close();
            }
            return res;
        }
        public int Delete(int id)
        {
            int res = 0;
            List<SqlParameter> sqlParam = new List<SqlParameter>();
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                string sqlStr = "delete from students where id = @id;";
                sqlParam.Clear();
                sqlParam.Add(new SqlParameter("@id", id));

                using (SqlCommand cmd = new SqlCommand(sqlStr, sqlCon))
                {
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                    res = cmd.ExecuteNonQuery();
                }
                sqlCon.Close();
            }
            return res;
        }
        public async Task<string> register(RegisterParams objParams)
        {
            string res = "";

            using (HttpClient client = new HttpClient()) 
            {
                var requestUri = "https://localhost:7283/Register/register";
                var data = new
                {
                    firstname = objParams.firstname,
                    lastname = objParams.lastname,
                    username = objParams.username,
                    password = objParams.password,
                    age = objParams.age
                };
                string json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(requestUri, content);
                res = await response.Content.ReadAsStringAsync();
            }
            return res;
        }
        public async Task<string> GetUsers()
        {
            string res = "";
            using (HttpClient client = new HttpClient()) 
            {
                string requestUri = "https://localhost:7283/Register/getusers";
                var response = await client.GetAsync(requestUri);
                res = await response.Content.ReadAsStringAsync();
            }
            return res;
        }
    }
}
