// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");


var blog = new BlogModel
{
  ID = 1,
  Title = "Hello testing 1",
  Author = "Ko bar bar",
  Content = "Become Billioner with AI"  
};

string jsonStr = JsonConvert.SerializeObject(blog);
// string jsonStr = blog.ToJson();
 System.Console.WriteLine(jsonStr);
 Console.ReadLine();

string jsonStr2 = """{"ID":1,"Title":"Hello testing 1","Author":"Ko bar bar","Content":"Become Billioner with AI"}""";
var blog2 = JsonConvert.DeserializeObject<BlogModel>(jsonStr2);
 System.Console.WriteLine(blog2.Author);
 Console.ReadLine();
public class BlogModel
{
    public int ID {get;set;}
    public string Title {get;set;}
    public string Author {get;set;}
    public string Content {get;set;}

}

public static class Extensions //Dev Code
{
    public static string ToJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj,Formatting.Indented);
        return jsonStr;
    }
}

