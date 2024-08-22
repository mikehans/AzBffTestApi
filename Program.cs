var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var products = new List<Product>
{
    new Product(1, "Hat"),
    new Product(2, "Scarf")
};

app.MapGet("/", () => "Hello catalogue");

app.MapGet("/env", () => app.Environment.IsDevelopment());


// get the data set
app.MapGet("/catalogue", () => TypedResults.Ok(products));

app.MapGet("/catalogue/{id}", (int id) => Results.Ok(products.Where(p => p.Id == id)));

app.MapPost("/catalogue", () => TypedResults.Created());

app.Run();

record Product(int Id, string Name);