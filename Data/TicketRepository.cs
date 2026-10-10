using Microsoft.Data.Sqlite;
using ServiceDeskPro.Models;
using System.Collections.Generic;

namespace ServiceDeskPro.Data
{
    public class TicketRepository
    {
        private readonly string _connectionString = "Data Source=servicedesk.db";

        // Constructor automatically creates the SQL table if it doesn't exist
        public TicketRepository()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Tickets (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    Priority TEXT NOT NULL,
                    Status TEXT NOT NULL
                )";
            command.ExecuteNonQuery();
        }

        // ADO.NET logic to Read from Database
        public List<Ticket> GetAllTickets()
        {
            var tickets = new List<Ticket>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Title, Description, Priority, Status FROM Tickets ORDER BY Id DESC";
            
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tickets.Add(new Ticket {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Priority = reader.GetString(3),
                    Status = reader.GetString(4)
                });
            }
            return tickets;
        }

        // ADO.NET logic to Insert into Database using Parameterized Queries
        public void AddTicket(Ticket ticket)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Tickets (Title, Description, Priority, Status) VALUES ($title, $desc, $priority, $status)";
            command.Parameters.AddWithValue("$title", ticket.Title);
            command.Parameters.AddWithValue("$desc", ticket.Description);
            command.Parameters.AddWithValue("$priority", ticket.Priority);
            command.Parameters.AddWithValue("$status", "Open");
            
            command.ExecuteNonQuery();
        }
    }
}