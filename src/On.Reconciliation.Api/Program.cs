using System.Data;
using System.Data.SqlClient;
using On.Reconciliation.Core.Commands;
using On.Reconciliation.Core.Queries;
using On.Reconciliation.Core.Services;
using Serilog;
using Thon.Hotels.FishBus;
using Thon.Hotels.FishBus.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

builder.Configuration.AddJsonFile("appSettings.json");
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDbConnection>(db => new SqlConnection(builder.Configuration.GetConnectionString("Default")));

// queries
builder.Services.AddTransient<IRuleQueries, RuleQueries>();
builder.Services.AddTransient<IStatementQueries, StatementQueries>();
builder.Services.AddTransient<IAccountingClientQueries, AccountingClientQueries>();
builder.Services.AddTransient<IGeneralLedgerQueries, GeneralLedgerQueries>();
builder.Services.AddTransient<IVatQueries, VatQueries>();

// commands
builder.Services.AddTransient<IReconciliationCommands, ReconciliationCommands>();
builder.Services.AddTransient<IRuleCommands, RuleCommands>();

// services
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<IMatchingService, MatchingService>();
builder.Services.AddTransient<IRuleService, RuleService>();

builder.Services.ConfigureMessaging().Configure<MessageSources>(builder.Configuration.GetSection("MessageSources"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();