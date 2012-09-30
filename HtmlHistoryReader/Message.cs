using System;
using HtmlAgilityPack;

namespace Mrowa.Utils.HtmlHistoryViewer
{
	/// <summary>
	/// Html message.
	/// </summary>
	public class Message
	{
		#region Message fields

		/// <summary>
		/// Gets or sets the sender ID.
		/// </summary>
		/// <value>
		/// The sender ID.
		/// </value>
		public string SenderID { get; set; }
		/// <summary>
		/// Gets or sets the name of the sender.
		/// </summary>
		/// <value>
		/// The name of the sender.
		/// </value>
		public string SenderName { get; set; }
		/// <summary>
		/// Gets or sets the recipient ID.
		/// </summary>
		/// <value>
		/// The recipient ID.
		/// </value>
		public string RecipientID { get; set; }
		/// <summary>
		/// Gets or sets the name of the recipient.
		/// </summary>
		/// <value>
		/// The name of the recipient.
		/// </value>
		public string RecipientName { get; set; }
		/// <summary>
		/// Gets or sets the content of the messages' HTML.
		/// </summary>
		/// <value>
		/// The content of the HTML.
		/// </value>
		public string HTMLContent { get; set; }
		/// <summary>
		/// Gets or sets the time the message was sent
		/// </summary>
		/// <value>
		/// The time.
		/// </value>
		public DateTime Time { get; set; }		
		
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Mrowa.Utils.HtmlHistoryReader.Message"/> class.
		/// </summary>
		public Message()
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Mrowa.Utils.HtmlHistoryReader.Message"/> class.
		/// </summary>
		/// <param name='htmlContent'>
		/// Html content.
		/// </param>
		public Message(HtmlNode node)
		{
			Initialize();
			
			HTMLContent = node.InnerHtml;
			ParseContent();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Mrowa.Utils.HtmlHistoryReader.Message"/> class.
		/// </summary>
		/// <param name='html'>
		/// Html content.
		/// </param>
		public Message(string html)
		{
			Initialize();

			HTMLContent = html;
			ParseContent();
		}

		/// <summary>
		/// Initialize this instance with empty values.
		/// </summary>
		protected void Initialize()
		{
			SenderID = String.Empty;
			SenderName = String.Empty;
			RecipientID = String.Empty;
			RecipientName = String.Empty;
			HTMLContent = String.Empty;
			Time = new DateTime(0);
		}

		/// <summary>
		/// Parses the html content and sets proper fields.
		/// </summary>
		protected void ParseContent()
		{
			HtmlNode node = HtmlNode.CreateNode(HTMLContent);
			// parsing...
		}
	}
}

