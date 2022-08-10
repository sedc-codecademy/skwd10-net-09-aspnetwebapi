using Loto3000.Domain.Enums;

namespace Loto3000.Domain.Models
{
    public class User
    {
        public User() { }
        public User(int id, string firstName, string lastName, string username, string pw, string email, double credits, DateOnly dateOfBirth, IList<Ticket> tickets)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = pw;
            Email = email;
            Credits = credits;
            DateOfBirth = dateOfBirth;
            Tickets = tickets;
        }
        public void CombinationGenerator(int[] nums)
        {
            IList<Combination> combs = new List<Combination>();

            for (int i = 0; i < nums.Length; i++)
            {
                Combination combination = new Combination();

                combination.Id = i;
                if (nums[i] < 1 || nums[i] > 37)
                {
                    throw new Exception("Number is not valid, please choose a number between 1 and 37");
                }
                combination.Number = nums[i];

                combs.Add(combination);
            }

            Combination = combs;
        }
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; }
        public string Password { get; set; }    
        public string Email { get; set; }
        public double Credits { get; set; }
        public IList<Combination> Combination { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public IList<Ticket> Tickets { get; set; }
    }
}