using Microsoft.OpenApi.Models;
//using PizzaStore.DB;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

var builder = WebApplication.CreateBuilder(args);

//ConnectionString
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

builder.Services.AddEndpointsApiExplorer();

//EntityFramework InMemory, restart after each run (shouldn't be used in release)
//builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));

//SQLite Db
builder.Services.AddSqlite<PizzaDb>(connectionString);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "PizzaStore API", 
        Description = "Keep track of your tasks", 
        Version = "v1" 
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/", () => "Hello World!");
 
//SYNC

//GET
//app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
//app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
//POST
//app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
//PUT
//app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
//DELETE
//app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));


//ASYNC

//GET
app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());
app.MapGet("/pizza/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id));

//POST
app.MapPost("/pizza", async (PizzaDb db, Pizza pizza) =>
{
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

//PUT
app.MapPut("/pizza/{id}", async (PizzaDb db, Pizza updatepizza, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    pizza.Name = updatepizza.Name;
    pizza.Description = updatepizza.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

//DELETE
app.MapDelete("/pizza/{id}", async (PizzaDb db, int id) =>
{
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null)
    {
        return Results.NotFound();
    }
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
