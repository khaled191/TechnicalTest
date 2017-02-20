using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheGoodTheBadAndTheUgly
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(_ =>
            {
                _.For<IApplicatoin>().Use<Application>();
                _.For<ILinkExtracctor>().Use<RegexLinkExtractor>();
            });

            var app = container.GetInstance<IApplicatoin>();

            var consoleColor = Console.ForegroundColor;

            app.Start(new Uri(args[0]), (url, text) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;      Console.Write($"text: {text} ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;        Console.Write($"url: {url}\r\n");
            });

            Console.ForegroundColor = consoleColor;

            if (!args.Any(a => a.ToLower().Contains("silent")))
            {
                Console.WriteLine("\r\nPress any key");
                Console.ReadLine();
            }
        }
    }

    #region The Application

    public interface IApplicatoin
    {
        void Start(Uri url, Action<string, string> onLinkFound);
    }

    public class Application: IApplicatoin
    {
        private readonly ILinkExtracctor _linkeExtractor;
        
        public Application(ILinkExtracctor linkExtractor)
        {
            this._linkeExtractor = linkExtractor;
        }

        public void Start(Uri URL, Action<string, string> onLinkFound)
        {
            Console.WriteLine("Obtaining all links for " + URL.ToString());

            var html = default(string);

            using (WebClient client = new WebClient()) 
            {
                html = client.DownloadString(URL);
            }

            if (html != null && html != "")
            {
                    var document = new Document(html);
                    var links = ((RegexLinkExtractor)this._linkeExtractor).GetLinks(document);

                    links.ForEach((l) =>
                    {
                        var url = l.Item1.Trim();
                        var text = l.Item2.Trim();

                        onLinkFound(url, text);
                    });
            }
        }
    }

    #endregion

    #region The Link Extractor

    public interface ILinkExtracctor
    {
        IEnumerable<Tuple<String, String>> GetLinks(IDocument Document, String filterByClass);
    }

    public class RegexLinkExtractor : ILinkExtracctor
    {
        const string _expression = "(?ismx)" +
                                        "<a[^>]*?" +
                                            "href=\"(?<href>[^\"]*?)\"[^>] * " +
                                            "(?:class=\"#{className}\")?[^>]*>" +
                                        "(?<linktext>[^<]*?)<";

        public List<Tuple<string, string>> GetLinks(IDocument document)
        {
            return GetLinks(document, ".*?").ToList();
        }

        [Obsolete]
        public IEnumerable<Tuple<String, String>> GetLinks(IDocument Document, string filterByClass)
        {
            var Expression = _expression.Replace("#{className}", filterByClass);

            var regex = new Regex(Expression);

            var a = Document.GetContent;

            try
            {
                // Convert all matches to return type
                return regex.Matches(a).OfType<Match>().Select((m) =>
                {
                    var href = m.Groups["href"].Value;
                    var text = m.Groups["linktext"].Value;

                    return new Tuple<String, String>(href, text);
                }).ToList();
            }
            catch (Exception ex)
            {
                // TODO should I do something here
                throw ex;
            }
        }
    }

    #endregion

    #region The Document Getter

    public interface DocumentGetter  {
        String getTheContent(string url);
    }

    #endregion

    #region The Document

    public interface IDocument
    {
        String GetContent { get; }
    }

    public class Document : IDocument
    {
        private String HTML;

        public Document(String html)
        {
            HTML = html;
        }

        public string GetContent 
        {
            get
            {
                return HTML;
            }
        }
    }

    #endregion

}

