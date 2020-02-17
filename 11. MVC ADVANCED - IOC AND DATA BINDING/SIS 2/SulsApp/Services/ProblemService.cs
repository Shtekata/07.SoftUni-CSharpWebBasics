using SulsApp.Models;

namespace SulsApp.Services
{
    public class ProblemService : IProblemService
    {
        private readonly ApplicationDbContext db;

        public ProblemService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateProblem(string name, int points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points,
            };
            db.Problems.Add(problem);
            db.SaveChanges();
        }
    }
}
