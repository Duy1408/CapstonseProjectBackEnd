using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RealEstateProjectSale.Mapper;
using RealEstateProjectSaleBusinessObject.Model;
using RealEstateProjectSaleRepository.IRepository;
using RealEstateProjectSaleRepository.Repository;
using RealEstateProjectSaleServices.IServices;
using RealEstateProjectSaleServices.Services;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mapper
builder.Services.AddAutoMapper(typeof(ApplicationMapper));

//Add Scoped
builder.Services.AddScoped<IStaffRepo, StaffRepo>();
builder.Services.AddScoped<IStaffServices, StaffServices>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IProjectRepo, ProjectRepo>();
builder.Services.AddScoped<IProjectServices, ProjectServices>();
builder.Services.AddScoped<IPropertyServices, PropertyServices>();
builder.Services.AddScoped<IPropertyRepo, PropertyRepo>();
builder.Services.AddScoped<ICommentRepo, CommentRepo>();
builder.Services.AddScoped<ICommentServices, CommentServices>();
builder.Services.AddScoped<IPromotionRepo, PromotionRepo>();
builder.Services.AddScoped<IPromotionServices, PromotionServices>();
builder.Services.AddScoped<IOpeningForSaleRepo, OpeningForSaleRepo>();
builder.Services.AddScoped<IOpeningForSaleServices, OpeningForSaleServices>();
builder.Services.AddScoped<IOpenForSaleDetailRepo, OpenForSaleDetailRepo>();
builder.Services.AddScoped<IOpenForSaleDetailServices, OpenForSaleDetailServices>();
builder.Services.AddScoped<ISalespolicyServices, SalespolicyServices>();
builder.Services.AddScoped<ISalespolicyRepo, SalespolicyRepo>();
builder.Services.AddScoped<IPropertyTypeServices, PropertyTypeServices>();
builder.Services.AddScoped<IPropertyTypeRepo, PropertyTypeRepo>();
builder.Services.AddScoped<IPaymentProcessRepo, PaymentProcessRepo>();
builder.Services.AddScoped<IPaymentProcessServices, PaymentProcessServices>();
builder.Services.AddScoped<IContractRepo, ContractRepo>();
builder.Services.AddScoped<IContractServices, ContractServices>();
builder.Services.AddScoped<IPaymentProcessDetailRepo, PaymentProcessDetailRepo>();
builder.Services.AddScoped<IPaymentProcessDetailServices, PaymentProcessDetailServices>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<IBookingServices, BookingServices>();
builder.Services.AddScoped<IPaymentTypeRepo, PaymentTypeRepo>();
builder.Services.AddScoped<IPaymentTypeServices, PaymentTypeServices>();
builder.Services.AddScoped<IPromotionDetailRepo, PromotionDetailRepo>();
builder.Services.AddScoped<IPromotionDetailServices, PromotionDetailServices>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<IContractPaymentDetailRepo, ContractPaymentDetailRepo>();
builder.Services.AddScoped<IContractPaymentDetailServices, ContractPaymentDetailServices>();
builder.Services.AddScoped<IBlockRepo, BlockRepo>();
builder.Services.AddScoped<IBlockService, BlockService>();
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<IZoneRepo, ZoneRepo>();
builder.Services.AddScoped<IZoneService, ZoneService>();
builder.Services.AddScoped<IFloorRepo, FloorRepo>();
builder.Services.AddScoped<IFloorService, FloorService>();
builder.Services.AddScoped<IPropertyCategoryRepo, PropertyCategoryRepo>();
builder.Services.AddScoped<IPropertyCategoryServices, PropertyCategoryServices>();


//Azure Blob Storage
builder.Services.AddScoped(_ => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));

//Stripe Payment
builder.Services.Configure<PaymentResponseModel>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

//Add Storage
builder.Services.AddMemoryCache();

//Paging
builder.Services.AddScoped<IPagingServices, PagingServices>();

//Jwt
builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(Options =>
{
    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    Options.SaveToken = true;
    Options.RequireHttpsMetadata = false;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Real Estate Project Sale", Version = "v1" });
    c.EnableAnnotations(); //Description trong API Swagger 
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});

//Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Customer", policy => policy.RequireRole("Customer"));
    options.AddPolicy("Staff", policy => policy.RequireRole("Staff"));
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});



//builder.Services.AddAutoMapper
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            //you can configure your custom policy
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });

});


var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate Project Sale API V1");
    c.RoutePrefix = string.Empty; // Set the root path for Swagger UI
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
