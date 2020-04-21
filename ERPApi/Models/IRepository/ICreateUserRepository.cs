namespace ERPApi.Models.IRepository
{
    public interface ICreateUserRepository
    {
        object GetAllCreateUser();

        object GetEmployeeByCreateUser(int user_id);

        object GetEmployees();

        user GetCreateUserByID(int user_id);

        bool InsertCreateUser(user oCreateUser);

        bool UpdateCreateUser(user oCreateUser);

        bool DeleteCreateUser(int user_id);
    }
}