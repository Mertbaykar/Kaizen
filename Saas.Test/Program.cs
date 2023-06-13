using Saas.Helpers;
using Saas.Models;
using System.Configuration;
using System.Text.Json;

string ticketUrl = ConfigurationManager.AppSettings["TicketUrl"];
string json = File.ReadAllText(ticketUrl);
var tickets = JSONHelper.Parse<List<Ticket>>(json);
Console.WriteLine(tickets.FirstOrDefault()?.Description);