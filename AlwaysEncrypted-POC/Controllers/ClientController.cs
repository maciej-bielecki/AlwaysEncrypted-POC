using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlwaysEncrypted_POC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ClientContext clientContext;

        public ClientController(IConfiguration configuration, ClientContext clientContext)
        {
            this.configuration = configuration;
            this.clientContext = clientContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClientAsync()
        {
            var client = Client.CreateWithRandomData();

            clientContext.Clients.Add(client);

            await clientContext.SaveChangesAsync();

            return CreatedAtRoute("GetClient", new { id = client.Id }, new { client.Id });
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetClient")]
        public async Task<IActionResult> GetClientAsync(Guid id)
        {
            var client = await clientContext.Clients.Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpGet]
        public async Task<IActionResult> GetClientAsync([FromQuery]string ccn)
        {
            #region using dbcontext
            /* 
             *var clientsQuery = clientContext.Clients.AsQueryable();

             if (!string.IsNullOrWhiteSpace(ccn))
             {
                 clientsQuery = clientsQuery.Where(x => x.CreditCardNumber == ccn);
             }
             var clients = await clientsQuery.ToListAsync();
             */
            #endregion

            #region using raw SqlCommand 

            var clients = new List<Client>();
            string cmdText = @"SELECT * FROM Client";
            using (var conn = new SqlConnection(configuration.GetConnectionString("Local")))
            {
                conn.Open();
                using SqlCommand sqlCmd = new SqlCommand(cmdText, conn);

                if (!string.IsNullOrWhiteSpace(ccn))
                {
                    cmdText += " WHERE CCN = @CCN";
                    sqlCmd.CommandText = cmdText; 
                    sqlCmd.Parameters.Add(new SqlParameter("@CCN", ccn));
                }

                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        clients.Add(Client.CreateFromReader(reader));
                    }
                }
            }

            #endregion

            return Ok(clients);
        }

    }
}
