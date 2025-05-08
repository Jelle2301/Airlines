using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;

namespace Repositories.Interfaces
{
    public interface IZitplaatsDAO : IDAO<Zitplaat>
    {
        Task<Zitplaat?> GetAllBeschikbareZitplaatsenByVluchtAsync(int vluchtId);
        Task MaakZitplaatsBezet(int zitplaatdId);
        Task MaakZitplaatsVrij(int zitplaatdId);
        Task<int> TelAantalBeschikbareZitplaatsenVoorVlucht(int vluchtId);
    }
}
