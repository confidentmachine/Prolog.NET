﻿using System.IO;
using Prolog;
using log4net;

namespace PrologWorkbench.Core
{
    public class ProgramProvider : IProvideProgram
    {
        readonly ILog _logger = LogManager.GetLogger(typeof (ProgramProvider));

        public Program Program { get; private set; }

        public void Reset()
        {
            Program = new Program();
        }

        public bool Load(string fileName)
        {
            Program program;
            try
            {
                program = Program.Load(fileName);
            }
            catch (FileNotFoundException ex)
            {
                _logger.Error("Coult not load file: ", ex);
                //CommonExceptionHandlers.HandleException(this, ex);
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.Error("Coult not load file: ", ex);
                //CommonExceptionHandlers.HandleException(this, ex);
                return false;
            }
            catch (IOException ex)
            {
                _logger.Error("Coult not load file: ", ex);
                //CommonExceptionHandlers.HandleException(this, ex);
                return false;
            }
            Program = program;
            return true;
        }

        public bool Save(string fileName)
        {
            if (Program == null || string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (!Program.IsModified)
            {
                return true;
            }

            try
            {
                if(!fileName.Equals(Program.FileName))
                    Program.SaveAs(fileName);
                else
                    Program.Save();
            }
            catch (FileNotFoundException ex)
            {
                _logger.Error("Coult not save file: ", ex);
                //CommonExceptionHandlers.HandleException(this, ex);
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.Error("Coult not save file: ", ex);
                //CommonExceptionHandlers.HandleException(this, ex);
                return false;
            }
            catch (IOException ex)
            {
                _logger.Error("Coult not save file: ", ex);
                ////CommonExceptionHandlers.HandleException(this, ex);
                return false;
            }
            return true;
        }
    }
}
