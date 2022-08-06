var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IServiceA, ServiceA>(); // new Class ServiceA ednas samo postoi
builder.Services.AddScoped<IServiceB, ServiceB>(); // eden request
builder.Services.AddTransient<IServiceC, ServiceC>(); // new ServiceC
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

app.UseAuthorization();

app.MapControllers();

app.Run();

public interface IServiceA
{

}

public class ServiceA : IServiceA
{
    private readonly IServiceB serviceB;

    public ServiceA(IServiceB serviceB)
    {
        this.serviceB = serviceB;
    }
}

interface IServiceB
{

}

//scoped
class ServiceB : IServiceB
{
    private readonly IServiceA serviceA;
    private readonly IServiceC serviceC;

    //public ServiceB(IServiceA serviceA, IServiceC serviceC)
    //{
    //    this.serviceA = serviceA;
    //    this.serviceC = serviceC;
    //}
}

interface IServiceC
{

}

//transient
class ServiceC : IServiceC
{
    private readonly IServiceA serviceA;

    public ServiceC(IServiceA serviceA, IServiceB serviceB)
    {
        this.serviceA = serviceA;
        ServiceB = serviceB;
    }

    public IServiceB ServiceB { get; }
}