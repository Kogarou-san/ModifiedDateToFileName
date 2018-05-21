using System;
using System.Windows.Forms;
using System.IO;

namespace ModifiedDateToFileName
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}
		/*String	directory:		full path to the file, not including the filename			Eg.		"C:\foo\bar.txt"
					fileEntries:	full path to a list of files, including the filenames		Eg.	[0]	"C:\latin\lorem.txt"
																									[1]	"C:\latin\ipsum.txt"
					file:			a single position of the array "fileEntries"				Eg.	[1]	"C:\latin\ipsum.txt"
					fileName:		the filename of "file", including extension					Eg.		"ipsum.txt"*/


		private void buttonOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Any file|*.*",
				Title = "Select a file from a folder you want to read dates and write names"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				String		directory	= Path.GetDirectoryName(openFileDialog.FileName);
				String[]	fileEntries = Directory.GetFiles(directory);
				foreach (String file in fileEntries)
				{
					String	oldName		= Path.GetFileName(file),
							extension	= Path.GetExtension(openFileDialog.FileName);

					DateTime dateTime = File.GetLastWriteTime(file);
					
					if (TimeZoneInfo.Local.IsDaylightSavingTime(dateTime))
						dateTime.AddHours(1);

					String newName = String.Concat(dateTime.ToString("yyyy-MM-dd hh.mm.ss"), extension);

					File.Move(	String.Concat(directory, '\\', oldName),
								String.Concat(directory, '\\', newName));
					
				}
			}
		}
	}
}


/*shit seria
	

	*/