using Avesta.Language.Globalization;
using Avesta.Language.Globalization.Enum;
using Avesta.Language.Globalization.Extension;
using Avesta.Language.Globalization.Model;
using Avesta.Language.Globalization.Provider;
using test;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();





builder.Services.AddWordContext<AvestaApplicationWordContext, LangFileProvider>();




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


namespace test
{
    public class AvestaApplicationWordContext : WordContext
    {
        public AvestaApplicationWordContext(LangFileProvider provider) : base(provider)
        {
        }


        public GlobalWord Word = new GlobalWord("HELLO", "say hello!"
            , new Word(LanguageShortName.IR_FA, AnnotationContentType.Display)
            , new Word(LanguageShortName.US_EN, AnnotationContentType.Display));

    }
}

