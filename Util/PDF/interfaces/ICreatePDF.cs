using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.PDF.interfaces
{
    public interface ICreatePDF
    {
        MemoryStream CreatePDFDocumentAsync(string logoPath, Boeking boeking, Vlucht vlucht);
    }
}
