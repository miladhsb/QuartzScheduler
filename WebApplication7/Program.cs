using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using QuartzSample.Jobs;
using QuartzSample.Lissener;
using QuartzSample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IJobManager, JobManager>();

#region Quartz

builder.Services.AddScoped<LogJob>();

builder.Services.AddQuartz(q =>
{
    q.SchedulerName = "MyScheduler";
    q.UseInMemoryStore();


    //inject job servce AddTransient
    //q.UseJobFactory<MyJobFactory>();
    //q.UseMicrosoftDependencyInjectionJobFactory();

    //q.AddTriggerListener<TriggerListener>();
    //q.AddJobListener<JobListener>();
    //q.AddSchedulerListener<SchedulerListener>();

    

    q.AddJob<LogJob>(p=>p.WithIdentity("mainjob").StoreDurably());

    var triggerKey = new TriggerKey("maintriger");
    q.AddTrigger(t=>t.WithIdentity(triggerKey)
    .ForJob(new JobKey("mainjob"))
    .WithSimpleSchedule(s=>s.WithIntervalInSeconds(2).RepeatForever()).StartNow()
    );
   
   
});
builder.Services.AddQuartzHostedService(q =>
{
    q.WaitForJobsToComplete=true;
    q.AwaitApplicationStarted = true;

});



//add JobFactory for dependency injection scoped in job 
  builder.Services.AddScoped<MyJobFactory>();
  var ISchedulerFactory= builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetService<ISchedulerFactory>();
  var Scheduler = ISchedulerFactory.GetScheduler().Result;
  Scheduler.JobFactory = new MyJobFactory(builder.Services.BuildServiceProvider());

//or
//ISchedulerFactory.GetScheduler().Result.JobFactory =  builder.Services.BuildServiceProvider().CreateScope().ServiceProvider.GetService<MyJobFactory>();


//add Listener
//روش دوم اضافه کردن لسینر
//Scheduler.ListenerManager.AddJobListener(new JobListener());
//Scheduler.ListenerManager.AddTriggerListener(new TriggerListener());
//Scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
