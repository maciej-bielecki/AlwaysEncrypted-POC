using System;
using Microsoft.Data.SqlClient;

namespace AlwaysEncrypted_POC
{
    public class Client
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public byte Rank { get; set; }

        public string CreditCardNumber { get; set; }

        public static Client CreateWithRandomData()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            return new Client()
            {
                Id = Guid.NewGuid(),
                Name = NameGenerator.GenerateName(random.Next(5, 10)),
                CreditCardNumber = $"{random.Next(1000, 9999)} {random.Next(1000, 9999)} {random.Next(1000, 9999)} {random.Next(1000, 9999)}",
                Birthdate = new DateTime(random.Next(1945, 1995), random.Next(1, 12), random.Next(1, 28)),
                Rank = Convert.ToByte(random.Next(5))
            };
        }

        public static Client CreateFromReader(SqlDataReader reader)
        {
            return new Client()
            {

                Id = (Guid)reader["id"],
                Name = reader["name"].ToString(),
                CreditCardNumber = reader["ccn"].ToString(),
                Birthdate = (DateTime)reader["birthdate"],
                Rank = (byte)reader["rank"]
            };
        }
    }
}
