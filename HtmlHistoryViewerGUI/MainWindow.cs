using System;
using Gtk;
using System.Collections.Generic;
using Mrowa.Utils.HtmlHistoryViewer;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnRunMessagesButtonClicked (object sender, EventArgs e)
	{
		HtmlHistoryReader historyReader = new HtmlHistoryReader();
		
		historyReader.ReadHistoryFile(FileChooserButton.Filename);
		originalTextView.Buffer.Text = historyReader.Content;
		
		List<Message> messages = new List<Message>();
		messages = historyReader.ParseHistoryFile();
		
		Gtk.ListStore HtmlListStore = new Gtk.ListStore (typeof (string), typeof (string), typeof(string));
		
		TreeView.AppendColumn ("Node", new Gtk.CellRendererText (), "text", 0);
		TreeView.AppendColumn ("Type", new Gtk.CellRendererText (), "text", 1);
		TreeView.AppendColumn ("Innertext", new Gtk.CellRendererText (), "text", 2);
		
		foreach (Message message in messages)
		{
			HtmlListStore.AppendValues("", "", message.HTMLContent);
		}
		
		TreeView.Model = HtmlListStore;
	}

	protected void OnRunHTMLDebugButtonClicked (object sender, EventArgs e)
	{
		HtmlHistoryReader historyReader = new HtmlHistoryReader();
		
		historyReader.ReadHistoryFile(FileChooserButton.Filename);
		originalTextView.Buffer.Text = historyReader.Content;
		
		List<string[]> htmls = new List<String[]>();
		htmls = historyReader.GetHtmlData();
		
		Gtk.ListStore HtmlListStore = new Gtk.ListStore (typeof (string), typeof (string), typeof(string));
		
		TreeView.AppendColumn ("Node", new Gtk.CellRendererText (), "text", 0);
		TreeView.AppendColumn ("Type", new Gtk.CellRendererText (), "text", 1);
		TreeView.AppendColumn ("Innertext", new Gtk.CellRendererText (), "text", 2);
		
		foreach (string[] strings in htmls)
		{
			HtmlListStore.AppendValues(strings[0], strings[1], strings[2]);
		}
		
		TreeView.Model = HtmlListStore;
	}
}
