using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using compundEye;
using CommandLine;
using CommandLine.Text;
using System.Drawing;

namespace toBeeView
{
    class Options
    {
        [Option('i', "image", Required = true, HelpText = "Input image file name.")]
        public string InputFile { get; set; }

        [Option('d', Required = true, HelpText = "Distance from fovea to Image plane in millimetres. (Must be an integer value)")]
        public int Distance { get; set; }

        [Option('h', Required = true, HelpText = "Image Height in millimetres. (Must be an integer value)")]
        public int Height { get; set; }

        [Option('r', "rho", DefaultValue = 0.8, HelpText = "Acceptance angle, in degrees. <Default: 0.8>")]
        public double rho { get; set; }

        [Option('p', "phi", DefaultValue = 0.8, HelpText = "Angle between receptors, in degrees. <Default: 0.8>")]
        public double phi { get; set; }

        [Option('o', HelpText = "Output image file name. If not supplied, a default value is assigned.")]
        public string OutputFile { get; set; }
        
        [Option('e',"verbose", HelpText = "Print details during execution.")]
        public bool Verbose { get; set; }

        //[ParserState]
        //public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("toBeeView", " v.0.1"),
                Copyright = new CopyrightInfo("EEZA - CSIC ", 2015),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };            
            help.AddPreOptionsLine("XXXXX is a command line software that simulates the image sampled by the retina of an animal viewing the scene chosen by the user at a specific distance. The user must provide the distance at which the animal views the scene, and two parameters describing its visual system: the angle between neighbouring photoreceptors, phi, and the acceptance angle of each photoreceptor, rho");
            help.AddPreOptionsLine(" ");
            help.AddPreOptionsLine("usage: toBeeView -i <file> -d <value> -h <value> [-a <value>] [-v <value>] [-o <file>] [-verbose] [--help]");
           
            help.AddOptions(this);
            return help;
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Options options = new Options();


            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
            if (options.Verbose)
            {
                //  Console.WriteLine(options.InputFile);
                // Console.WriteLine(options.Distance);
                //Console.WriteLine(options.Keywords.ToString());
                Console.WriteLine("Verbose activado. Generando imagen extendida");                
            }
            else
            {
                Console.WriteLine("Working ...");
            }

            // do the job
            ojoCompuesto bee = ojoCompuesto.Instance();
            try
            {
                Image image = Bitmap.FromFile(options.InputFile);
                bee.InicialiceOjo(image, (float)options.phi, (float)options.rho, (float)options.Distance/10f, (float)options.Height/10f);
                Console.WriteLine("nº hexagons:"+bee.nHexagonos);

                bee.calccolors();
                if (options.Verbose) { 
                    image = bee.Paint(true, true, true, 2); 

                }
                else { image = bee.Paint(false, false, true, 2); }

                image.Save(Path.GetFileNameWithoutExtension(options.InputFile) + "-BEE-d" + options.Distance + "-h" + options.Height + "-r" + options.rho + "-p" + options.phi + ".png");
                //Path.GetExtension(options.InputFile)
            }
            catch
            {
                Console.WriteLine("Error opening input file... Check if file exist and it can be read.");
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
        }
    }
}
