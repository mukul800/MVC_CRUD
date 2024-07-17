namespace MVC_Crud.BLL
{
    public class parameters
    {
        public class InsertParams
        {
            public string name;
            public int age;
        }
        public class UpdateParams
        {
            public int id;
            public string name;
            public int age;
        }
        public class RegisterParams
        {
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public int age { get; set; }
        }
    }
}
