using EFCoreLearn.MyContext;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreLearn.Services
{
    public class StudentService
    {
        private MyDbContext _dbContext;
        public StudentService([FromServices] MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public StudentService()
        {

        }



        public bool Add(Student student)
        {
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            return true;
        }


    }
}
