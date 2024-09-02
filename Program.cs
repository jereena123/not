using Demomvc.Repository;

namespace Demomvc
{
    public class Program
    {
        public static void Main(string[] args)
        { //this initilaizes a new instance  of the WebApplicaationBuilder class,
            //which is used to configure services and the app's pipeline
            //key concepts:middleware :authentication:,routing,static files

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //Service Registration
            //This registers MVC services to the container
            //enabling support for controllers and views

            builder.Services.AddControllersWithViews();
            //register repository  defination and implementation
            //these  lines register services with dependency injection
            builder.Services.AddScoped<ILoginRepository,LoginRepository>();

            //Building the Application
            //This method builds the application
            //Creating an instance of the WebApplication class
            //The app object now contains the services and middleware
            //configured during the setup phase
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               //enables http strict Transport Security(HSTS)
               //which adds a special header
               //to enforce secure connection
                app.UseHsts();
            }
            //configuring middleware
            //Rediects all http requests to HTTPS.
            app.UseHttpsRedirection();
            //Serves static files like HTML,CSS,Javascript ansd images,
            app.UseStaticFiles();
            //Enables routing,allowing the application to define endpoint
            //for controllers and view
            
            app.UseRouting();
            //Enables authorization,checking iftheuser is allowed 
            //access a specific resource or action
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
