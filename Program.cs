using System.Security.Authentication.ExtendedProtection;


//Most of what is nere is boilerplate
//installed by the webapi template.
//I have noted additional code added to 
//actually make things work, like CORS

//A lightbulb will pop up offering to use
//"old fashioned" main program terminology.
//USE IT!

internal class Program
{
    private static void Main(string[] args)
    {

        //Create app container
        var builder = WebApplication.CreateBuilder(args);

        // Add services, as needed
        // to the container.

        //A service runner is needed for MVC controllers
        builder.Services.AddControllers();
        
        //API endpoints
        //Swagger
        //CORS policy
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Add a Cors service runner,
        //Pass it one parameter, which are its options
        //options are a lambda (anonymous) function
        //since the parameter probably only takes
        //callback functions
        builder.Services.AddCors(options => {
            //options is an object/class
            //with methods
            //use the default policy method
            //for convenience
            options.AddDefaultPolicy(

                //default policy takes a
                //callback lambda (anonymous) function
                policy => {
                    //policy is an object/class
                    //we use chained methods,
                    //hence the dots connecting each
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                
                }

            );
        
        });

        //now, build the app
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //Tell the app to use Cors policy
        //this call must come early in this
        //section
        app.UseCors();
        //Now, we enable CORS at our
        //controller calls with the 
        //[EnableCors] attribute
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}