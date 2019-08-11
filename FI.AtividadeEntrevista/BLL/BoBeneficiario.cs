using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario bene = new DAL.DaoBeneficiario();
            return bene.Incluir(beneficiario);
        }

        public List<long> Incluir(List<DML.Beneficiario> beneficiarios)
        {
            List<long> Ids = new List<long>();
            foreach (DML.Beneficiario beneficiario in beneficiarios)
            {
                Ids.Add(Incluir(beneficiario));
            }
            return Ids;
        }

        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario bene = new DAL.DaoBeneficiario();
            bene.Alterar(beneficiario);
        }

        public void Alterar(List<DML.Beneficiario> beneficiarios)
        {
            foreach (var beneficiario in beneficiarios)
            {
                Alterar(beneficiario);
            }
        }

        public void Excluir(long id)
        {
            DAL.DaoBeneficiario bene = new DAL.DaoBeneficiario();
            bene.Excluir(id);
        }

        public void Excluir(List<long> ids)
        {
            foreach (var id in ids)
            {
                Excluir(id);
            }
        }

        public List<DML.Beneficiario> Listar(long Id)
        {
            DAL.DaoBeneficiario bene = new DAL.DaoBeneficiario();
            return bene.Listar(Id);
        }

        public bool VerificarExistencia(string CPF, long IdCliente)
        {
            DAL.DaoBeneficiario bene = new DAL.DaoBeneficiario();
            return bene.VerificarExistencia(CPF, IdCliente);
        }
    }
}
