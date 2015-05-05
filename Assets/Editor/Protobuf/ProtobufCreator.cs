using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public static class ProtobufCreator
{
	private static readonly string shell_script_path = "/Protobuf";

	[MenuItem("Protobuf/Compile Proto File")]
	private static void CompileProtoFile()
	{
		Debug.Log("=== CompileProtoFile ===");
		Debug.Log(Application.persistentDataPath);
		Debug.Log(Directory.GetCurrentDirectory());

		//var psi = new System.Diagnostics.ProcessStartInfo();
		 
		//psi.FileName = Directory.GetCurrentDirectory() + shell_script_path;

		var process = new System.Diagnostics.Process();

		string absolute_shell_script_path = Directory.GetCurrentDirectory() + shell_script_path;

		process.StartInfo.FileName = "sh";
		process.StartInfo.WorkingDirectory = absolute_shell_script_path;
		process.StartInfo.Arguments= "protobuf.sh";
		process.StartInfo.CreateNoWindow = false;
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardError=true;
		process.StartInfo.RedirectStandardOutput=true;
		//process.StartInfo.WorkingDirectory = absolute_shell_script_path;

		process.Start();

		//UnityEngine.Debug.Log( process.StandardOutput.ReadToEnd() );
		process.WaitForExit();
		process.Close();

		Debug.Log(absolute_shell_script_path);
		
		/*
		string[] filePaths = Directory.GetFiles(); 

		foreach(var path in filePaths)
		{
			Debug.Log(path);
		}
*/
	}
}
