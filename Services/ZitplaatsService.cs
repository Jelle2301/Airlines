using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class ZitplaatsService :IZitplaatsService
    {
        private IZitplaatsDAO _zitplaatsDAO;
        public ZitplaatsService(IZitplaatsDAO zitplaatsDAO)
        {
            _zitplaatsDAO = zitplaatsDAO;
        }
        public async Task<IEnumerable<Zitplaat>?> GetAllAsync()
        {
            try
            {
                return await _zitplaatsDAO.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(ZitplaatsService) in GetAllAsync");
                throw;
            }
        }

        public async Task<Zitplaat?> GetAllBeschikbareZitplaatsenByVluchtAsync(int vluchtId)
        {
            try
            {
                return await _zitplaatsDAO.GetAllBeschikbareZitplaatsenByVluchtAsync(vluchtId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(ZitplaatsService) in GetAllBeschikbareZitplaatsenByVluchtAsync");
                throw;
            }
        }

        public async Task MaakZitplaatsBezet(int zitplaatdId)
        {
            try
            {
                await _zitplaatsDAO.MaakZitplaatsBezet(zitplaatdId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(ZitplaatsService) in MaakZitplaatsBezet");
                throw;
            }
        }

        public async Task MaakZitplaatsVrij(int zitplaatdId)
        {
            try
            {
                await _zitplaatsDAO.MaakZitplaatsVrij(zitplaatdId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Service(ZitplaatsService) in MaakZitplaatsVrij");
                throw;
            }
        }
    }
}
