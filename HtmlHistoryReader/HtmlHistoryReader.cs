using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Mrowa.Utils.HtmlHistoryViewer
{
	/// <summary>
	/// Html history reader.
	/// </summary>
	public class HtmlHistoryReader
	{
		#region Fields
		
		/// <summary>
		/// Gets or sets the filepath.
		/// </summary>
		/// <value>
		/// The filepath.
		/// </value>
		public string Filepath { get; protected set; }	
		
		/// <summary>
		/// Gets or sets the HTML content.
		/// </summary>
		/// <value>
		/// The HTML content.
		/// </value>
		public string Content { get; protected set; }
		
		#endregion
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Mrowa.Utils.HtmlHistoryReader.HtmlHistoryReader"/> class.
		/// </summary>
		public HtmlHistoryReader ()
		{
			Filepath = String.Empty;
			Content = String.Empty;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Mrowa.Utils.HtmlHistoryReader.HtmlHistoryReader"/> class.
		/// </summary>
		/// <param name='filepath'>
		/// Filepath to the html file.
		/// </param>
		public HtmlHistoryReader(string filepath)
		{
			ReadHistoryFile(filepath);
		}
		
		/// <summary>
		/// Reads the history file.
		/// </summary>
		/// <param name='filePath'>
		/// File path.
		/// </param>
		public void ReadHistoryFile(string filePath)
		{
			Filepath = filePath;
			
			Content = System.IO.File.ReadAllText(Filepath);
		}

		/// <summary>
		/// Parses the history file to Messages.
		/// </summary>
		/// <returns>
		/// List of messages from history file.
		/// </returns>
		public List<Message> ParseHistoryFile()
		{			
			List<Message> messages = new List<Message>();			
			HtmlDocument document = new HtmlDocument();
			List<HtmlNode> nodeList = new List<HtmlNode>();

			document.LoadHtml(Content);
			HtmlNode bodyNode = document.DocumentNode.SelectSingleNode("//body");

			foreach (HtmlNode node in bodyNode.ChildNodes)
			{
				nodeList.Add(node);
				if (node.Name == "#text" & node.InnerText == Environment.NewLine)
				{
					StringBuilder builder = new StringBuilder();

					foreach (HtmlNode messageNode in nodeList)
		         	{
						builder.Append(messageNode.InnerHtml);
					}

					messages.Add (new Message(builder.ToString()));

					nodeList.Clear();
				}
			}
			return messages;
		}

		/// <summary>
		/// Gets list of all html nodes in //body: xpath, name and inner html.
		/// </summary>
		/// <returns>
		/// The html data.
		/// </returns>
		public List<String[]> GetHtmlData()
		{
			List<String[]> htmls = new List<string[]>();
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(Content);
			HtmlNode bodyNode = document.DocumentNode.SelectSingleNode("//body");
			Stack<HtmlNode> nodes = new Stack<HtmlNode>();

			for (int i = bodyNode.ChildNodes.Count - 1; i >= 0; --i)
			{
				nodes.Push(bodyNode.ChildNodes[i]);
			}

			/*foreach (HtmlNode node in bodyNode.ChildNodes)
			{
				nodes.Push(node);
			}*/

			while (nodes.Count > 0)
			{
				HtmlNode node = nodes.Pop();

				foreach (HtmlNode childNode in node.ChildNodes)
				{
					nodes.Push (childNode);
				}

				string[] result = new String[3];
				result[0] = node.XPath;
				result[1] = node.Name;
				result[2] = "[" + node.InnerHtml + "]";

				htmls.Add (result);
			}
			
			return htmls;
		}
		
		/// <summary>
		/// Strips the HTML.
		/// </summary>
		/// <returns>
		/// Text stripped of html..
		/// </returns>
		/// <param name='htmlContent'>
		/// HTML
		/// </param>
		public static string StripHTML(string htmlContent)
		{
			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(htmlContent);
			string strippedHTML = doc.DocumentNode.InnerText;
			
			return strippedHTML;
		}
	}
}

