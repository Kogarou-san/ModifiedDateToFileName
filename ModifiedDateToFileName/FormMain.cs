using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ModifiedDateToFileName
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		String directory;
		Stopwatch stopwatch = new Stopwatch();

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Any file|*.*",
				Title = "Select a file from a folder you want to read dates and write names"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				stopwatch.Start();

				directory = Path.GetDirectoryName(openFileDialog.FileName);
				String path = Path.GetDirectoryName(openFileDialog.FileName);
				String[] fileEntries = Directory.GetFiles(directory);
				UInt64 i = 1;
				foreach (String fileName in fileEntries)
				{
					String oldName, extension;
					
					oldName = Path.GetFileName(fileName);
					extension = Path.GetExtension(openFileDialog.FileName);

					DateTime dateTime = File.GetLastWriteTime(fileName);

					File.AppendAllText("info.txt", String.Concat("DST for " + oldName + " is " + TimeZoneInfo.Local.IsDaylightSavingTime(File.GetLastWriteTime(fileName)).ToString(), '.'));
					//MessageBox.Show("DST for " + name + " is " + TimeZoneInfo.Local.IsDaylightSavingTime(File.GetLastWriteTime(fileName)).ToString(), "Timezone", MessageBoxButtons.OK);
					
					if (TimeZoneInfo.Local.IsDaylightSavingTime(File.GetLastWriteTime(fileName)))
						dateTime.AddHours(1);
					String newName = String.Concat(dateTime.ToString("yyyy-MM-dd hh.mm.ss"), extension);

					File.Move(String.Concat(path, '\\', oldName),
							String.Concat(path, '\\', newName));
					File.AppendAllText("info.txt", String.Concat(" Renamed ", oldName, " to ", newName, Environment.NewLine)); 
					i++;
				}
				stopwatch.Stop();
				String message = String.Concat("\n\n", i - 1, " out of ", fileEntries.Length, " files renamed succ-essfully in ",
												(float)stopwatch.ElapsedMilliseconds / 1000, " seconds.");
				File.AppendAllText("info.txt", message + Environment.NewLine);
				MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

			}
		}
	}
}
