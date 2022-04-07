using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BestBuyBestPractices
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        // Field or local variable for making queries to database
        private readonly IDbConnection _connection;

        public DapperDepartmentRepository(IDbConnection connection)  //Constructor 
        {
            _connection = connection;
        }

       public IEnumerable<Department> GetAllDepartments()
        {
            var depos = _connection.Query<Department>("SELECT * FROM departments;").ToList();

            return depos;

            // return _connection.Query<Department>("SELECT * FROM departments;").ToList();
        }

        
        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);", new { departmentName = newDepartmentName });
        }

       
    }
}
