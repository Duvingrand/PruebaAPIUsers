using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Prueba.Models;
using Prueba.Repositories.Interfaces;
using Prueba.Services;

namespace Prueba.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBHelperService _dbHelper;

        public UserRepository(DBHelperService dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<User> GetAllUsers()
    {
        var users = new List<User>();

        using var connection = _dbHelper.GetConnection();
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM Users", connection);
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                users.Add(new User
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["Name"].ToString() ?? string.Empty,
                    LastName = reader["LastName"].ToString() ?? string.Empty,
                    Address = reader["Address"].ToString() ?? string.Empty,
                    TellNumber = reader["TellNumber"].ToString() ?? string.Empty,
                    BirthDay = reader["TellNumber"].ToString() ?? string.Empty,
                    DocumentID = reader["DocumentID"].ToString() ?? string.Empty
                });
            }
        }

        return users;
    }

    public User? GetUserById(int id)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE ID = @ID", connection);
        cmd.Parameters.AddWithValue("@ID", id);

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                return new User
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["Name"].ToString() ?? string.Empty,
                    LastName = reader["LastName"].ToString() ?? string.Empty,
                    Address = reader["Address"].ToString() ?? string.Empty,
                    TellNumber = reader["TellNumber"].ToString() ?? string.Empty,
                    BirthDay = reader["TellNumber"].ToString() ?? string.Empty,
                    DocumentID = reader["DocumentID"].ToString() ?? string.Empty
                };
            }
        }

        return null;
    }

    public void CreateUser(User user)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        SqlCommand cmd = new SqlCommand(
            @"INSERT INTO Users (Name, LastName, Address, TellNumber, BirthDay, DocumentID) 
              VALUES (@Name, @LastName, @Address, @TellNumber, @BirthDay, @DocumentID)", connection);

        cmd.Parameters.AddWithValue("@Name", user.Name);
        cmd.Parameters.AddWithValue("@LastName", user.LastName);
        cmd.Parameters.AddWithValue("@Address", user.Address);
        cmd.Parameters.AddWithValue("@TellNumber", user.TellNumber);
        cmd.Parameters.AddWithValue("@BirthDay", user.BirthDay);
        cmd.Parameters.AddWithValue("@DocumentID", user.DocumentID);

        cmd.ExecuteNonQuery();
    }

    public void UpdateUser(int userId, User user)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        SqlCommand cmd = new SqlCommand(
            @"UPDATE Users 
              SET Name = @Name, LastName = @LastName, Address = @Address, 
                  TellNumber = @TellNumber, BirthDay = @BirthDay, DocumentID = @DocumentID 
              WHERE ID = @ID", connection);

        cmd.Parameters.AddWithValue("@ID", userId);
        cmd.Parameters.AddWithValue("@Name", user.Name);
        cmd.Parameters.AddWithValue("@LastName", user.LastName);
        cmd.Parameters.AddWithValue("@Address", user.Address);
        cmd.Parameters.AddWithValue("@TellNumber", user.TellNumber);
        cmd.Parameters.AddWithValue("@BirthDay", user.BirthDay);
        cmd.Parameters.AddWithValue("@DocumentID", user.DocumentID);

        cmd.ExecuteNonQuery();
    }

    public void DeleteUser(int userId)
    {
        using var connection = _dbHelper.GetConnection();
        connection.Open();

        SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE ID = @ID", connection);
        cmd.Parameters.AddWithValue("@ID", userId);

        cmd.ExecuteNonQuery();
    }

    }
}