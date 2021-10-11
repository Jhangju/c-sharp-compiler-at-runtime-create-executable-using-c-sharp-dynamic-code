using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
namespace test_form
{
    public partial class Form1 : Form
    {
        [Obsolete]
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += new System.EventHandler(this.button1_Click);
            textBox4.Text = "something.exe";
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            string Output = "something.exe";
            if (textBox4.Text == "")
            {

                textBox4.Text="something.exe";

                Output = textBox4.Text;
            }
            else
            { 
             Output = textBox4.Text;
            }
            Button ButtonObject = (Button)sender;

            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("System.Net.dll");//System.Net.HttpStatusCode
            parameters.ReferencedAssemblies.Add("System.Windows.dll");//System.Net.HttpStatusCode
            parameters.ReferencedAssemblies.Add("System.Net.Http.dll");//System.Net.HttpStatusCode
            parameters.ReferencedAssemblies.Add("System.Web.dll");//System.Net.HttpStatusCode

            var assemblies = AppDomain.CurrentDomain
                            .GetAssemblies()
                            .Where(a => !a.IsDynamic)
                            .Select(a => a.Location);

            parameters.ReferencedAssemblies.AddRange(assemblies.ToArray());
            //Make sure we generate an EXE, not a DLL
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, textBox1.Text);

            if (results.Errors.Count > 0)
            {
                textBox1.ForeColor = Color.Red;
                foreach (CompilerError CompErr in results.Errors)
                {
                    textBox2.Text = 
                                "Line number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine + Environment.NewLine;
                }
            }
            else
            {
                //Successful Compile
                textBox2.ForeColor = Color.Blue;
                textBox2.Text = "Success!";
                //If we clicked run then launch our EXE
                if (ButtonObject.Text == "Run") Process.Start(Output);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
