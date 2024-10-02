using dapr_demo.Components;
using Dapr.Workflow;
using Workflows;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<OrderWorkflow>();

    options.RegisterActivity<CreateDraftOrderActivity>();
    options.RegisterActivity<CheckForDuplicateBusinessPartnerActivity>();
    options.RegisterActivity<UpdateOrderActivity>();
    options.RegisterActivity<UpdateOrderStatusActivity>();
});

if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DAPR_GRPC_PORT")))
{
    Console.WriteLine("Setting DAPR_GRPC_PORT to 50001");
    Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", "50001");
}

// Add services to the container.
builder.Services.AddDaprClient();
builder.Services.AddSingleton<DaprWorkflowClient>();
builder.Services.AddScoped<WorkflowService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
