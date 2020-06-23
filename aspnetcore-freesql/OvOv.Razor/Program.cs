using System;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
namespace OvOv.Razor
{
    class Program
    {
        static void Main(string[] args)
        {
            string template = "Hello @Model.Name, welcome to RazorEngine!";
            var result = Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });

            Console.WriteLine(result);

            string templateFilePath = "HelloWorld.cshtml";
            var templateFile = File.ReadAllText(templateFilePath);
            string templateFileResult = Engine.Razor.RunCompile(templateFile, Guid.NewGuid().ToString(), null, new
            {
                Name = "World"
            });

            Console.WriteLine(templateFileResult);

            string copyRightTemplatePath = "CopyRightTemplate.cshtml";
            var copyRightTemplate = File.ReadAllText(copyRightTemplatePath);
            string copyRightResult = Engine.Razor.RunCompile(copyRightTemplate, Guid.NewGuid().ToString(), typeof(CopyRightUserInfo), new CopyRightUserInfo
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                UserName = "IGeekFan"
            });
            Console.WriteLine(copyRightResult);

            Console.ReadKey();

        }
    }
}
