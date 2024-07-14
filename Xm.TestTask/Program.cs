using Serilog;
using Xm.TestTask.OperationFilters;
using Xm.TestTask.Services;
using Xm.TestTask.Services.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.ReadFrom.Configuration(hostContext.Configuration);
});

builder.Services.AddSwaggerGen(config => {
    config.OperationFilter<DataTypeOperationFilter>();
    config.OperationFilter<BodyTypeOperationFilter>();
});

// add message handlers
builder.Services.AddTransient<IMessageHandler, ActionHandler>();
builder.Services.AddTransient<IMessageHandler, AvatarHandler>();
builder.Services.AddTransient<IMessageHandler, NotificationHandler>();

builder.Services.AddTransient<IProcessMessageService, ProcessMessageService>();
builder.Services.AddControllers();

var app = builder.Build();

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();